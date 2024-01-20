using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_Victory : MonoEvent
{

    public string[] cards;
    public char[] cardChar;
    public int CardsRewarded = 4;

    public static bool hasLawyer = false;
    public override void Start()
    {
        SkillWeightList skillPool = Resources.Load<SkillWeightList>("VictorySkillPool");

        name = "Financial Domination";

        cards = new string[CardsRewarded];
        cardChar = new char[CardsRewarded];
        
        monolog = new string[] {"<i>Through your actions of financial domination you have created new value out of nothing.",
            "<i>You may now add a new card to your portfolio to continue your hard work here at CONTROL. INC.",
            "<i>Good luck and may the sun smile at you with good fortune."};
        responses = new string[CardsRewarded];

        for(int i = 0; i < cards.Length; i++)
        {
            cardChar[i] = skillPool.GetRandomSkill();
            cards[i] = $"{cardChar[i]} - {Player.getSkillName(cardChar[i])}"; 
            responses[i] = $"Take the card {cards[i]}";
        }

        eventImage = GetImage("Victory");

        base.Start();
    }

    public override void Respond(int n)
    {
        Player.AddCard(cardChar[n]);

        text.text = $"<i>You took the {cards[n]} card. Truely good fortune lies ahead of you.";

        base.Respond(n);
    }
}
