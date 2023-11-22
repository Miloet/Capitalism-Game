using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPile : MonoBehaviour
{
    //Show out of menu
    //In battle
    public GameObject endTurn;
    public GameObject effectText;
    public GameObject boopTheSnoot;
    //In events


    //Show in menu
    public GameObject cards;
    public GameObject stocks;

    public bool inEvent = false;
    public static bool showing = true;

    private void Start()
    {
        DisplayDrawPile();
    }
    public void DisplayDrawPile()
    {
        showing = !showing;
        cards.SetActive(showing);
        stocks.SetActive(showing);

        if(!inEvent)
        {
            endTurn.SetActive(!showing);
            effectText.SetActive(!showing);
            boopTheSnoot.SetActive(!showing);
        }
        else
        {
            
        }
    }
}
