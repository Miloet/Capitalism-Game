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
        name = transform.Find("Symbol").GetComponent<TextMeshProUGUI>();
        price = transform.Find("Price").GetComponent<TextMeshProUGUI>();
        amount = transform.Find("Number").GetComponent<TextMeshProUGUI>();

        StartCoroutine(UpdateText());
    }

    public IEnumerator<WaitUntil> UpdateText()
    {
        yield return new WaitUntil(() => !stock.loading);
        name.text = stock.stockSymbol;
        price.text = stock.getValue().ToString() + "$";
        amount.text = stock.amount.ToString();
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
        if (Player.money >= stock.getValue() && add > 0 || stock.amount > 0 && add < 0)
        {
            List<Stock> assets = Player.assets.ToList<Stock>();
            StartCoroutine(UpdateText());
            if (!assets.Contains(stock)) 
                assets.Add(stock);
            
            if (stock.amount - add < 0)
                assets.Remove(stock);

            Player.assets = assets.ToArray();
            stock.amount += add;
            Player.money -= stock.getValue() * add;
        }
    }
}
