using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardCompiler : MonoBehaviour
{
    public static Vector2 bounds = new Vector2(5,3.5f/2f);

    public static void Compile()
    {
        CardBehavior[] cards = GameObject.FindObjectsOfType<CardBehavior>();
        List<CardBehavior> cardsInPlay = new List<CardBehavior>();
        
        foreach(CardBehavior card in cards)
        {
            if(inBounds(card.transform)) cardsInPlay.Add(card);
        }

        cards = SortArray(cardsInPlay.ToArray());

        bool valid = true;

        foreach(CardBehavior c in cards)
        {
            //if(!c.checkStuff()) 
            {
                valid = false;
                break;
            }


        }

        if (!valid) return; //Send error message to player

        foreach(CardBehavior c in cards)
        {
            //c.Effect();
        }

    }

    public static bool inBounds(Transform t)
    {
        if (t.position.x > -bounds.x && t.position.x < bounds.x
            && t.position.y > -bounds.y && t.position.y < bounds.y) return true;
        return false;
    }
    public static CardBehavior[] SortArray(CardBehavior[] array)
    {
        return array.OrderBy(go => go.transform.position.x).ToArray();
    }
}
