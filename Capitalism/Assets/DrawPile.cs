using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPile : MonoBehaviour
{
    bool showing = false;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void DisplayDrawPile()
    {
        showing = !showing;
        gameObject.SetActive(showing);

    }

}
