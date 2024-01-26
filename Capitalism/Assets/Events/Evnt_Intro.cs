using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_Intro : MonoEvent
{
    public override void Start()
    {
        name = "The first day";

        monolog = new string[] { "Welcome to your new job at <color=#97F0EA>CONTROL.</color> Inc. Are you prepared to work yourself to the bone for your new family~?",
            "To your left you can see we have provided you a standard issue briefcase. Use it to store you skills and assets into a portfolio. (click on it!)",
            "<wave>Amazing right?</wave>",
            "We even added a stock index right inside it! <size=25>it cant show graphs because of budget cuts to the graphics department</size> <wave>ANYWAYS!",
            "Enought about the perks, what are you going to be doing? You will greet customers and solve whatever problems they may have.",
            "Some customers wont like your solution and might resort to financial violance against you. During these \"Financial battles\" you will use your skills and assets to defeat them.",
            "Defeating \"Enemies\" like this will grant you a meriad of benifits! now we are not telling you to go and kill the customers but...",
            "<wave>MOVING ON!!</wave> Some skills will require funds to use, specifically speculative funds, <wave>more specifically;</wave> stocks. (see your portfolio to purchase those)",
            "You may also add additional stock options by typing in a valid stock symbol in the box bellow the stock menu.",
            "Right... acording to the Control v. American Health Institute agreement (1999) we must inform you that stress has a negative effect on mental and physical well being and may impact your preformance.",
            "Something, something, worklife balance or some liberal communist shit like that. <wave>sounds pretty un-american if you ask me.</wave> whats next? <color=#ffc4c4><wave>\u2665universal healthcare?\u2665</wave></color> thats some commie shit right there.",
            "Anyways here at control we have a motto that we believe all employees should follow;",
            "Set Goals. Have a ten-year plan. Wake up early. <color=green><wave>Invest.</wave></color> <shake><color=red>CEO Mindset.", 
            "Good luck and once again, welcome to the family."};
        responses = new string[] {"Thanks...?"};

        eventImage = GetImage("Tower");

        base.Start();
    }

    public override void Respond(int n)
    {
        switch (n)
        {
            case 0:
                
                break;
            case 1:

                break;
        }

        base.Respond(n);
    }
}
