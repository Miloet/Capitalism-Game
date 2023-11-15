using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using TMPro;

public class Asset : CardBehavior
{
    public CardBehavior owner;

    public Stock self;
    public float value = 100f;
    public float multiplier;
    

    TextMeshPro assetName;
    TextMeshPro price;
    TextMeshPro upperPrice;
    TextMeshPro lowerPrice;

    SpriteRenderer Background;
    SpriteRenderer graph;

    LineRenderer graphLine;

    Vector2 graphBounds = new Vector2(0.9f, 0.9f);

    void Awake()
    {
        Free();

        assetName = transform.Find("Asset/Name").GetComponent<TextMeshPro>();
        price = transform.Find("Asset/Price").GetComponent<TextMeshPro>();
        upperPrice = transform.Find("Asset/Upper").GetComponent<TextMeshPro>();
        lowerPrice = transform.Find("Asset/Lower").GetComponent<TextMeshPro>();

        Background = transform.Find("Asset").GetComponent<SpriteRenderer>();
        graph = transform.Find("Asset/Graph").GetComponent<SpriteRenderer>();

        assetName.text = "Asset";
        price.text = "-";
        move = true;

        graphLine = graph.GetComponent<LineRenderer>();

        StartCoroutine(Sorting());
    }



    private IEnumerator<WaitForSeconds> Sorting()
    {
        while (true)
        {
            if (MouseInput.selected == gameObject) UpdateOrder(100);
            else UpdateOrder((int) Mathf.Floor(transform.position.x * 50f));
            yield return new WaitForSeconds(Time.deltaTime + 0.1f);
        }
    }
   
    public void UpdateOrder(int baseID)
    {
        Background.sortingOrder = baseID;

        assetName.sortingOrder = baseID + 1;
        price.sortingOrder = baseID + 1;
        upperPrice.sortingOrder = baseID + 2;
        lowerPrice.sortingOrder = baseID + 2;
        graph.sortingOrder = baseID + 1;
    }

    #region

    public void Assign(string symbol)
    {
        self = new Stock();
        self.CreateStock(symbol, "1y", "1d", 100);
    }
    public void Assign(Stock newAsset)
    {
        self = newAsset;
        StartCoroutine(UpdateStock());
        StartCoroutine(UpdateText());
    }
    public void Free(bool ignoreAdd = false)
    {
        if (owner != null)
        {
            owner.currentAsset.move = true;
            owner.currentAsset = null;
            owner = null;
            if(!ignoreAdd) Player.AddUnique(gameObject);
        }
    }

    public IEnumerator<WaitUntil> UpdateText()
    {
        yield return new WaitUntil(() => !self.loading);
        assetName.text = self.stockSymbol;
    }
    public IEnumerator<WaitUntil> UpdateStock()
    {
        yield return new WaitUntil(() => !self.loading);
        value = self.getValue() * self.amount;
        price.text = value.ToString("N2")+"$";
        //Update value and growth each time a month passes.

        //Update graph

        float highest = float.MinValue;
        float lowest = float.MaxValue;

        List<Vector3> positions = new List<Vector3>();

        float[] prices = self.price.ToArray();

        int max = Mathf.Min(Event.time, 60);
        Gradient gradient = new Gradient();

        for(int i = Event.time - max; i < Event.time; i++)
        {
            if (prices[i] > highest)
            {
                highest = prices[i];
            }
            if (prices[i] < lowest)
            {
                lowest = prices[i];
            }
        }

        for(int i = Event.time - max; i < Event.time; i++)
        {
            positions.Add(new Vector3(
                (float)(i - Event.time + max) / (float)max * graphBounds.x,  
                graphBounds.y* FindYPosition(prices[i], highest, lowest) , -.01f
                ) - (Vector3)graphBounds / 2f);
        }
        graphLine.positionCount = positions.Count;
        graphLine.SetPositions(positions.ToArray());

        upperPrice.text = highest.ToString("N1") +"$";
        lowerPrice.text = lowest.ToString("N1") + "$";
    }

    private float FindYPosition(float value, float upper, float lower)
    {
        return (value - lower) / (upper - lower);
    }

    #endregion
}


public class Stock
{
    public int amount;
    public string stockSymbol { get; private set; }
    public List<float> price { get; private set; }
    public DateTime creation { get; private set; }

    public bool loading;

    public static string url = "https://query1.finance.yahoo.com/v8/finance/chart/";

    public async void CreateStock(string symbol, string range = "1y", string interval = "1d", int Amount = 0)
    {
        loading = true;
        stockSymbol = symbol;
        range = "5y";
        await getStockHistoricalPrices(range, interval);

        Player.loadingStock = true;
        loading = false;

        //creation = getTimeFromRange(range);

        amount = Amount;
    }
    public bool VaildDate(DateTime date)
    {
        return date >= creation && date <= DateTime.Today;
    }
    public float getValue()
    {
        if (price != null)
            if (price[Event.time] != -1) return price[Event.time];

        Console.WriteLine($"No valid prices not found for {stockSymbol}");
        return -1;
    }

    public async Task getStockHistoricalPrices(string range, string interval)
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
            Debug.LogError($"Error: {ex.Message}");
        }
        //price = ReverseOrder(prices);
        price = prices;
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

