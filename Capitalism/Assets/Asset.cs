using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asset : MonoBehaviour
{
    public CardBehavior owner;

    public string symbol;
    public float value;
    public int ammount;
    public float multiplier;

    
    void Start()
    {
        owner = null;
    }
    public void Free()
    {
        if (owner != null)
        {
            owner.currentAsset = null;
            owner = null;
        }
    }

    public float GetValue()
    {
        return ammount * value;
    }
    public void UpdateStock()
    {
        //Update value and growth each time a month passes.
    }
}


public class Stock
{
    public int ammount;
    public string stockSymbol { get; private set; }
    public List<Decimal> price { get; private set; }
    public DateTime creation { get; private set; }

    public static string url = "https://query1.finance.yahoo.com/v8/finance/chart/";

    public Stock(string symbol, string range, string interval, int Ammount = 0)
    {
        stockSymbol = symbol;

        getStockHistoricalPrices(range, interval).Wait();

        creation = getTimeFromRange(range);

        ammount = Ammount;
    }

    public decimal Buy(int Ammount, DateTime time, decimal money)
    {
        if (Ammount > 0)
        {
            decimal price = getPrice(time);
            if (money >= Ammount * price)
            {
                ammount = Ammount;
                return price * Ammount;
            }
            else return -1;
        }
        else return -1;
    }
    public decimal Sell(int Ammount, DateTime time, decimal money)
    {
        if (Ammount > 0)
        {
            decimal price = getPrice(time);

            ammount -= Ammount;
            return price * Ammount;

        }
        else return -1;
    }

    public bool VaildDate(DateTime date)
    {
        return date >= creation && date <= DateTime.Today;
    }

    #region Gather Data

    public decimal getCurrentPrice()
    {
        return getPrice(Clock.Time);
    }

    public decimal getPrice(DateTime time)
    {
        int index = (time - creation).Days;

        if (time >= creation) return price[price.Count - index];
        Console.WriteLine($"Price not found for {stockSymbol} at {time}");
        return -1;
    }
    public decimal getAveragePrice(DateTime date, int days)
    {
        decimal t = 0;
        for (int i = 0; i < days; i++)
        {
            t += getPrice(date.AddDays(-i));
        }
        return t / (decimal)days;
    }


    public static int Convert(DateTime first, DateTime second)
    {
        return (first.Date - second.Date).Days;
    }

    public async Task<decimal[]> getStockHistoricalPrices(string range, string interval)
    {
        List<decimal> prices = new List<decimal>();

        string endpoint = $"{stockSymbol}?range={range}&interval={interval}"; // Adjust the range and interval as per your requirements

        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url + endpoint);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read the response content
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response
                YahooFinanceApiResponse apiResponse = JsonConvert.DeserializeObject<YahooFinanceApiResponse>(responseContent);

                // Extract historical prices from the response
                List<decimal> historicalPrices = new List<decimal>();

                if (apiResponse?.Chart?.Result?.Length > 0)
                {
                    var quote = apiResponse.Chart.Result[0]?.Indicators?.Quote;
                    if (quote != null && quote.Length > 0)
                    {
                        decimal? previousPrice = null;

                        foreach (var closePrice in quote[0]?.Close)
                        {
                            decimal price = closePrice ?? previousPrice ?? 0;
                            historicalPrices.Add(price);
                            previousPrice = price;
                        }
                    }
                }

                if (historicalPrices.Count > 0)
                {
                    foreach (decimal? price in historicalPrices)
                    {
                        if (price.HasValue)
                        {
                            prices.Add(Math.Round(price.Value, 2));
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Historical stock prices not found.");
                }
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"Error: {ex.Message}");
        }
        price = ReverseOrder(prices);



        return prices.ToArray();
    }

    public async Task getCurrentStockPrice()
    {
        string stockUrl = url + stockSymbol;

        Console.WriteLine(stockUrl);

        try
        {
            // Send an HTTP GET request to the website
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(stockUrl);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read the HTML content
                string htmlContent = await response.Content.ReadAsStringAsync();

                // Extract stock information using regular expressions
                string pattern = $@"<fin-streamer[^>]+class=""[^""]*\bFw\(b\)[^""]*""[^>]+data-symbol=""{stockSymbol}""[^>]+data-test=""qsp-price""[^>]+value=""(\d+(?:\.\d+)?)""[^>]*>";
                Match match = Regex.Match(htmlContent, pattern);


                // Store stock prices in an array
                price = new List<decimal>();

                if (match.Success)
                {

                    if (decimal.TryParse(match.Groups[1].Value.Replace('.', ','), out decimal stockPrice))
                    {
                        price.Add(stockPrice);
                    }
                    else
                    {
                        Console.WriteLine("Unable to parse stock price.");
                        Console.WriteLine($"The returned value was: {match.Groups[1].Value}");
                    }
                }
                else
                {
                    Console.WriteLine("Stock information not found.");
                }


                // Display the stock prices
                Console.Write($"Stock prices for {stockSymbol}:");
                Console.Write(string.Join(" - ", price) + "$\n");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static List<decimal> ReverseOrder(List<decimal> prices)
    {
        List<decimal> reversedPrices = new List<decimal>(prices.Count);
        for (int i = prices.Count - 1; i >= 0; i--)
        {
            reversedPrices.Add(prices[i]);
        }
        return reversedPrices;
    }

    private DateTime getTimeFromRange(string timeRange, string interval = "1d")
    {
        DateTime currentDate = DateTime.Today;
        DateTime startDate = currentDate;

        startDate = maxTime(interval);

        return startDate;
    }

    private DateTime maxTime(string interval)
    {
        TimeSpan time = new TimeSpan();

        DateTime currentDate = DateTime.Today;

        switch (interval)
        {
            case "1d":
                time = new TimeSpan(-price.Count, 0, 0, 0);
                break;

            case "5d":
                time = new TimeSpan(-5 * price.Count, 0, 0, 0);
                break;
        }
        return currentDate + time;
    }

    #endregion

}


#region Yahoo API


public class YahooFinanceApiResponse
{
    public YahooFinanceChart Chart { get; set; }
}

public class YahooFinanceChart
{
    public YahooFinanceChartResult[] Result { get; set; }
}

public class YahooFinanceChartResult
{
    public YahooFinanceIndicators Indicators { get; set; }
}

public class YahooFinanceIndicators
{
    public YahooFinanceQuote[] Quote { get; set; }
}

public class YahooFinanceQuote
{
    public decimal?[] Close { get; set; }
}



#endregion


public class Files
{
    string path = ""; //debug folder
    string filename = "";

    public Files(string file)
    {
        filename = Path.Combine(path, file);
    }

    public void WriteFile<T>(T text, bool replace = false)
    {
        if (!File.Exists(filename)) CreateFile(filename);

        using (StreamWriter writer = new StreamWriter(filename, !replace))
        {
            writer.Write(text);
            writer.Close();
        }
    }

    public void WriteFileArray<T>(T[] text, bool replace = false)
    {
        if (!File.Exists(filename)) CreateFile(filename);

        using (StreamWriter writer = new StreamWriter(filename, !replace))
        {
            foreach (T s in text)
            {
                writer.WriteLine(s);
            }
            writer.Close();
        }
    }

    public void CreateFile(string name)
    {
        File.Create(filename).Close();
    }

    public string[] ReadFile()
    {
        return File.ReadLines(filename).ToArray();
    }


    public string[] ReadStockSymbols()
    {
        List<string> symbols = new List<string>();

        string[] fileResult = ReadFile();

        for (int i = 0; i < fileResult.Length; i++)
        {
            string s = "";
            for (int c = 0; c < fileResult[i].Length; c++)
            {
                s += fileResult[i][c];
            }
            symbols.Add(s);
        }

        /*for (int i = 0; i < fileResult.Length; i++)
        {
            Console.WriteLine("Line:  "+ i);
            string s = "";
            for(int c = 0; fileResult[i][c] != '	'; c++)
            {
                Console.WriteLine("Char  "+ c);
                s += fileResult[i][c];
            }
            symbols.Add(s);
        }

        /*
        foreach (string text in fileResult)
        {
            string t = text.Replace(" ", string.Empty);
            symbols.AddRange(t.Split(':'));
        }*/

        return symbols.ToArray();
    }
}
