using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_Lawyer : MonoEvent
{
    public override void Start()
    {
        name = "Saul... Goodman?";

        monolog = new string[] {"Hey, you handsome corprate bigshot! " +
            "Did you know you have rights? Constitution says you do!"

            ,"I see that look on your face and I understand you're a busy woman so " +
            "Ill get straight to the point. I want to be your lawyer. This is a tough " +
            " buissness you're in and if a lowlife like me can change that tide then thats all I am asking for."

            ,"A 500$ retainer and 200$ flat monthly fee and I am yours through day, night and storm!"};
        responses = new string[] { "Take the deal (Pay 500$ and lose 200$ each month)", "Ask him to leave" };

        eventImage = GetImage("SaulGoodman");

        base.Start();
    }

    public override void Respond(int n)
    {
        switch (n)
        {
            case 0:
                Player.money -= 499.99f;
                Player.income -= 199.99f;

                text.text = "<wave>Glad to make business with you!</wave>";

                break;
            case 1:
                
                break;
        }

        base.Respond(n);
    }
}
