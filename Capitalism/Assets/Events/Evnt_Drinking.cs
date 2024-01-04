using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_Drinking : MonoEvent
{


    public static int drink = 3;
    public static float tab;
    public static float tabTogether;
    public override void Start()
    {
        name = "Bar";

        monolog = new string[] { "<i>You and your new friend Alicia talk in the bar for a while...</i>", 
            "-it feels nice to get away from work like this from time to time.", 
            "Or getting away from life in general I guess. You feel me?" ,
            "<i>The bartender comes up to you two and asks.</i><color=#6e0000> What are you two ladies gonna drink?"};
        responses = new string[] { "Something strong (severly alcoholic)", "Something sweet (alcoholic)", "What she is having (?)" , "Water (non alcoholic)"};

        eventImage = GetImage("Bar");

        base.Start();
    }

    public override void Respond(int n)
    {
        drink = n;

        float usual = 29.9f;

        switch (n)
        {
            case 0://Strong

                text.text = "Damn girl~! Something strong for me too, keep!";
                Player.stress -= 2;
                tab = 64.30f;
                tabTogether = tab * 2f + 4.99f;


                break;
            case 1://Sweet

                text.text = "<color=#6e0000>Pina Colada coming up.</color> Ooooohh~ I'll have the usual, keep!";
                Player.stress -= 1;
                tab = 14.99f;
                tabTogether = usual + tab;

                break;
            case 2://What she is having

                text.text = "Hehe~ your good friend Alicia knows best, eh~? I'll have some lemon vodka with your usual twist, keep!";
                tab = usual;
                tabTogether = usual * 2f;

                break;
            case 3://Water

                text.text = "Bit boring but you're nice company so I don't mind~ and I guess you can drive me home too! <i>(you don't have a car...)";
                tab = 0;
                tabTogether = usual;

                break;
        }

        AltResponse(Evnt.TheBill);
        
    }
}
