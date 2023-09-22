using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class CardCompiler : MonoBehaviour
{
    public static Vector2 bounds = new Vector2(5,3.5f/2f);
    private static TextMeshPro abilityText;
    private static TextMeshProUGUI effectText;
    public static float multiplier = 1f; 

    private void Start()
    {
        abilityText = transform.Find("Text").GetComponent<TextMeshPro>();
        effectText = GameObject.Find("EffectCompileText").GetComponent<TextMeshProUGUI>();
    }

    public static void Compile()
    {
        SkillBase[] cards = FindObjectsOfType<SkillBase>();
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

        multiplier = 1f;

        if (!valid)
        {
            print(reason);
        }
        else
            foreach(SkillBase c in cards)
            {
                c.Effect(multiplier);
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

    public static void UpdateText()
    {
        SkillBase[] cards = FindObjectsOfType<SkillBase>();
        List<SkillBase> cardsInPlay = new List<SkillBase>();

        foreach (SkillBase card in cards)
        {
            if (inBounds(card.transform)) cardsInPlay.Add(card);
        }

        cards = SortArray(cardsInPlay.ToArray());


        string text = "";

        foreach (SkillBase card in cards)
        {
            text += card.letter;
            if (card.requireAsset)
            {
                string asset = "";
                if (card.currentAsset != null) asset = card.currentAsset.self.stockSymbol;
                else asset = "!";
                text += $"({asset})";
            }
        }
        abilityText.text = text;

        string effects="";
        foreach(SkillBase card in cards)
        {
            effects += card.writeEffect() + "\n";
        }
        effectText.text = effects;


    }
}