using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_Intro : MonoEvent
{
    public override void Start()
    {
        name = "New Day";

        monolog = new string[] { "Welcome to your new job at <color = #97f0ea>CONTROL.</color> Inc. Are you prepared to work yourself to the bone for your new family~?",
            "Set Goals. Have a ten-year plan. Wake up early. <color=green><wave>Invest.</wave></color> <shake><color=red>CEO Mindset.", 
            "Good luck and once again, welcome to the family."};
        responses = new string[] {"Continue"};

        eventImage = GetImage("Tower");

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
