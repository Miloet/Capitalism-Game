using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asset : MonoBehaviour
{
    public CardBehavior owner;
    void Start()
    {
        owner = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Free()
    {
        if (owner != null)
        {
            owner.currentAsset = null;
            owner = null;
        }
    }
}
