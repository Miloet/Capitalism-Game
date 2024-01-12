using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class StockBuy : MonoBehaviour
{
    public Stock stock;

    TextMeshProUGUI name;
    TextMeshProUGUI price;
    TextMeshProUGUI amount;

    // Start is called before the first frame update
    void Start()
    {
        FindUI();

        StartCoroutine(UpdateText());
    }

    public void FindUI()
    {
        name = transform.Find("Symbol").GetComponent<TextMeshProUGUI>();
        price = transform.Find("Price").GetComponent<TextMeshProUGUI>();
        amount = transform.Find("Number").GetComponent<TextMeshProUGUI>();
    }

    public IEnumerator<WaitUntil> UpdateText()
    {
        yield return new WaitUntil(() => !stock.loading);

        name.text = stock.stockSymbol;
        price.text = stock.getValue().ToString("N2")+"$";
        amount.text = stock.amount.ToString();
    }
    public void DirectUpdateText()
    {
        if (name == null) FindUI();
        if (stock != null && !stock.loading)
        {
            name.text = stock.stockSymbol;
            price.text = stock.getValue().ToString("N2") + "$";
            amount.text = stock.amount.ToString();
        }
    }

    public static void UpdateAllText()
    {
        Player.updateValue();

        StockBuy[] s = FindObjectsOfType<StockBuy>(true);
        foreach(StockBuy stockWindow in s)
        {
            stockWindow.DirectUpdateText();
        }
    }

    public void Add1()
    {
        Add(1);
    }
    public void Add5()
    {
        Add(5);
    }
    public void Remove1()
    {
        Add(-1);
    }
    public void Remove5()
    {
        Add(-5);
    }

    public void Add(int add)
    {
        if (Player.money >= stock.getValue() * add && add > 0 || stock.amount >= Mathf.Abs(add) && add < 0)
        {
            List<Stock> assets = Player.assets.ToList<Stock>();
            
            if (!Find(stock.stockSymbol, assets))
                assets.Add(stock);
            else
            {
                if (stock.amount + add <= 0) assets = Remove(stock.stockSymbol, assets);
            }

            Player.assets = assets.ToArray();
            stock.amount += add;
            Player.money -= stock.getValue() * add;
            StartCoroutine(UpdateText());
            Player.updateValue();
        }
    }
    private bool Find(string symbol, List<Stock> list)
    {
        foreach(Stock stock in list)
        {
            if(stock.stockSymbol.ToUpper() == symbol.ToUpper()) return true;
        }
        return false;
    }
    private List<Stock> Remove(string symbol, List<Stock> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            if (list[i].stockSymbol.ToUpper() == symbol.ToUpper())
            {
                list.RemoveAt(i);
                break;
            }
        }
        return list;
    }
}
