using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_Lawyer : MonoEvent
{

    public static bool hasLawyer = false;
    public override void Start()
    {
        name = "Saul... Goodman?";

        monolog = new string[] {
    "Hey there, you dashing corporate bigshot! " +
    "Let me drop some legal wisdom on you – did you know you've got rights? " +
    "Constitution's got your back, my friend!",

    "Look, I get it. You're a busy lady, and time is money. " +
    "So, let's cut to the chase. I want to be the legal maestro in your orchestra.",

    "This business of yours is a wild ride, and if a charming rascal like me can help steer the ship, that's all I'm asking for.",

    "For a mere 500 bucks upfront and a sweet 200 smackers each month, " +
    "I'm your legal guardian angel. Day or night, rain or shine – I'm at your service! " +
    "Let's make those legal troubles disappear like magic, with a touch of Saul Goodman style."
};
        responses = new string[] { "Take the deal (Pay 500$ and 200$ each month)", "Ask him to leave" };

        eventImage = GetImage("SaulGoodman");

        base.Start();
    }

    public override void Respond(int n)
    {
        switch (n)
        {
            case 0:
                Player.money -= 499.99f;
                Player.expenses.Add(new Expense("Lawyer (the best there is~)", 199f));
                Player.AddCard('L');

                text.text = "<wave>Glad to make business with you!</wave>";

                hasLawyer = true;

                break;
            case 1:

                text.text = "Ah, come on! Seriously? " +
                    "You're turning down the golden opportunity of a lifetime here! " +
                    "You don't want the legal Picasso in your corner, huh? " +
                    "Well, good luck navigating the treacherous legal waters without the one and only Saul Goodman. " +
                    "When you change your mind – and believe me, you will – just remember, I'm the guy who could've made all your legal problems vanish into thin air. " +
                    "You'll be begging for a second chance, mark my words!";

                break;
        }

        base.Respond(n);

        RemoveRandomEvent(Evnt.Lawyer);
    }
}
