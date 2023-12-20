using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class AddStock : MonoBehaviour
{
    public TMP_InputField input;

    public void Add()
    {
        if(input.text != "")
        {
            List<string> s = StockList.Symbols.ToList();
            s.Add(input.text);
            StockList.Symbols = s.ToArray();
        }
    }
}
