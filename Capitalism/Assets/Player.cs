using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float money = 0;
    public static int stress = 0;

    public static SkillBase[] skills;
    public static Asset[] assets;

    public static float assetValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        updateValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void updateValue()
    {
        float value = 0;
        foreach(Asset a in assets)
        {
            value += a.GetValue();
        }
        assetValue = value;
    }
}
