using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPile : MonoBehaviour
{
    public GameObject endTurn;
    public GameObject effectText;
    public GameObject boopTheSnoot;
    public static bool showing = false;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void DisplayDrawPile()
    {
        showing = !showing;
        gameObject.SetActive(showing);

        endTurn.SetActive(!showing);
        effectText.SetActive(!showing);
        boopTheSnoot.SetActive(!showing);
    }

}
