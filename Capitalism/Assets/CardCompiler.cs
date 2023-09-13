using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardCompiler : MonoBehaviour
{
    public static Vector2 bounds = new Vector2(5,3.5f/2f);

    public static void Compile()
    {
        SkillBase[] cards = GameObject.FindObjectsOfType<SkillBase>();
        List<SkillBase> cardsInPlay = new List<SkillBase>();
        
        foreach(SkillBase card in cards)
        {
            if(inBounds(card.transform)) cardsInPlay.Add(card);
        }

        cards = SortArray(cardsInPlay.ToArray());

        bool valid = true;
        string reason = ""; 

        foreach(SkillBase c in cards)
        {
            if(!c.Validate()) 
            {
                reason += c.ValidateReason();
                valid = false;
            }


        }


        if (!valid)
        {
            print(reason);
            return;
        }

        foreach(SkillBase c in cards)
        {
            c.Effect();
        }
    }

    public static bool inBounds(Transform t)
    {
        if (t.position.x > -bounds.x && t.position.x < bounds.x
            && t.position.y > -bounds.y && t.position.y < bounds.y) return true;
        return false;
    }
    public static SkillBase[] SortArray(SkillBase[] array)
    {
        return array.OrderBy(go => go.transform.position.x).ToArray();
    }
}
