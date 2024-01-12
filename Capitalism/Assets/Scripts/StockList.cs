using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockList : MonoBehaviour
{
    GameObject stockTrader;
    Stock[] availableStocks;
    public static string[] Symbols = { "AAPL", "MSFT", "GOOGL", "AMZN", "TSLA", "JPM", "WMT", "NVDA", "DIS", "TSE", "KO", "NFLX", "BAC", "V", "PG", "INTC", "CSCO", "XOM", "GM", "U" };

    void Start()
    {
        stockTrader = Resources.Load<GameObject>("Stock");

        List<Stock> stocks = new List<Stock>();
        Stock stock;

        Player player = GameObject.Find("Mouse").GetComponent<Player>();

        foreach (string s in Symbols)
        {
            bool unique = true;
            stock = new Stock();
            for (int i = 0; i < Player.assets.Length; i++)
            {
                if (s == Player.assets[i].stockSymbol)
                {
                    unique = false;
                    stock = Player.assets[i];
                    break;
                }
            }
            if (unique) player.StartCoroutine(player.assignStock(stock, s));
            
            stocks.Add(stock);
        }
        availableStocks = stocks.ToArray();

        RectTransform sourceRect = GetComponent<RectTransform>();
        sourceRect.sizeDelta = new Vector2(sourceRect.rect.x, availableStocks.Length * 80f - 390f);
        Vector2 upperBound = new Vector2(sourceRect.anchoredPosition.x, sourceRect.anchoredPosition.y + sourceRect.rect.height / 2f);
        for(int i = 0; i < availableStocks.Length; i++)
        {
            RectTransform rtrans = Instantiate(stockTrader, transform).GetComponent<RectTransform>();
            rtrans.anchoredPosition = new Vector2(upperBound.x, upperBound.y - rtrans.rect.height/2 - i * 80f + 190f);

            rtrans.offsetMin = new Vector2(10, rtrans.offsetMin.y);
            rtrans.offsetMax = new Vector2(0, rtrans.offsetMax.y);

            rtrans.GetComponent<StockBuy>().stock = availableStocks[i];
        }
        print("Stocks are made");
    }
}
