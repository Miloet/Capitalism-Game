using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asset : MonoBehaviour
{
    public CardBehavior owner;

    public string symbol;
    public float value;
    public int ammount;

    
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
