using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using TMPro;

public class Asset : MonoBehaviour
{
    public CardBehavior owner;

    public Stock self;
    public float value = 100f;
    public float multiplier;


    TextMeshPro assetName;
    TextMeshPro price;


    void Start()
    {
        //value = self.getPrice();
        Free();

        assetName = transform.Find("Name").GetComponent<TextMeshPro>();
        price = transform.Find("Price").GetComponent<TextMeshPro>();

        price.text = "10$";
    }
    public void Assign(string symbol)
    {
        self = new Stock(symbol,"1y","1m", 100);
    }
    public void Assign(Stock newAsset)
    {
        self = newAsset;
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
        return value;
    }

    public void UpdateText()
    {
        assetName.text = self.stockSymbol;
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
    public List<float> price { get; private set; }
    public DateTime creation { get; private set; }

    public static string url = "https://query1.finance.yahoo.com/v8/finance/chart/";

    public Stock(string symbol, string range = "10y", string interval = "1m", int Ammount = 0)
    {
        stockSymbol = symbol;

        getStockHistoricalPrices(range, interval).Wait();

        creation = getTimeFromRange(range);

        ammount = Ammount;
    }
    public bool VaildDate(DateTime date)
    {
        return date >= creation && date <= DateTime.Today;
    }
    public float getValue(int time)
    {
        if (time >= price.Count) return price[price.Count - time];
        Console.WriteLine($"Price not found for {stockSymbol} at {time}");
        return -1;
    }

    public async Task<float[]> getStockHistoricalPrices(string range, string interval)
    {
        List<float> prices = new List<float>();

        string endpoint = $"{stockSymbol}?range={range}&interval={interval}";

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
                List<float> historicalPrices = new List<float>();

                if (apiResponse?.Chart?.Result?.Length > 0)
                {
                    var quote = apiResponse.Chart.Result[0]?.Indicators?.Quote;
                    if (quote != null && quote.Length > 0)
                    {
                        float? previousPrice = null;

                        foreach (var closePrice in quote[0]?.Close)
                        {
                            float price = closePrice ?? previousPrice ?? 0;
                            historicalPrices.Add(price);
                            previousPrice = price;
                        }
                    }
                }

                if (historicalPrices.Count > 0)
                {
                    foreach (float? price in historicalPrices)
                    {
                        if (price.HasValue)
                        {
                            prices.Add((float)Math.Round(price.Value, 2));
                        }
                    }
                }
                else
                {
                    Debug.LogError("Historical stock prices not found.");
                }
            }
        }
        catch (Exception ex)
        {
            //Debug.LogError($"Error: {ex.Message}");
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
                price = new List<float>();

                if (match.Success)
                {

                    if (float.TryParse(match.Groups[1].Value.Replace('.', ','), out float stockPrice))
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

    private static List<float> ReverseOrder(List<float> prices)
    {
        List<float> reversedPrices = new List<float>(prices.Count);
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
    public float?[] Close { get; set; }
}



#endregion

