using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_PartyInvite : MonoEvent
{
    public override void Start()
    {
        
        name = "Party...?";

        monolog = new string[] {"Yo girl! " +
            "Me and some of the other guys at the office are gonna hit a pub later tonight. " +
            "You new here so I thought I should invite you too. You in?" };
        responses = new string[] { "Sure thing.", "Fucking finally. I need some of that.", "Nah, got other plans." };

        eventImage = GetImage("Party");

        base.Start();
    }

    public override void Respond(int n)
    {
        switch (n)
        {
            case 0:

                text.text = "Hell yeah! See you tonight then~";
                AltResponse(Evnt.Drinking);
                break;
            case 1:

                text.text = "Don't we all~? These hours are torture!";
                AltResponse(Evnt.Drinking);
                break;
            case 2:

                text.text = "Aw... too bad... Maybe next time!";
                base.Respond(n);
                break;
        }
    }
}
