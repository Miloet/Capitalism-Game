using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float money = 1000;
    public static int stress = 0;

    public static char[] skills = {'A','A','B','B','C'};
    public static string[] assets;

    public static float assetValue = 0;

    // Start is called before the first frame update
    void Start()
    {

        //updateValue();

        foreach(char skill in skills)
        {

        }
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
