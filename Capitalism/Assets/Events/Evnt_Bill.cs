using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_Bill : MonoEvent
{
    public int payTab = 0;

    float tab;
    float tabTogether;
    public override void Start()
    {
        name = "The Bill";

        tab = Evnt_Drinking.tab;
        tabTogether = Evnt_Drinking.tabTogether;

        monolog = new string[] { "Drink", $"<color=#6e0000>Hey ladies, we are closing soon. Your tabes comes out to {tab}$ and {tabTogether - tab}$ respectivly. How you ladies payin'?"};

        switch (Evnt_Drinking.drink)
        {
            case 0:
                monolog[0] = "<i>You drink all night long. Many of your coworkers leave but you two keep on drinking and having fun together.";
                break;
            case 1:
                monolog[0] = "<i>You talk and drink all night long and feel a true connection to Alicia. Like you two got to show off who you truely are to eachother.";
                break;
            case 2:
                monolog[0] = "<i>You talk and drink all night. You listen intently as Alicia passionately rambles off about some rom-com she is a big fan. She talks and you listen for what probably was hours yet neither of you tire.";
                break;
            case 3:
                monolog[0] = "<i>You and Alicia talk about everything under the sun while in the nice soft light of the red bar. Alicia's rosed cheaks and drunken demeanor is cute.";
                break;
        }

        responses = new string[] { $"Should we split? ({tab}$)", $"I'll pay for both of us. ({tabTogether}$)", "Alicia, could you take it this time?" };

        eventImage = GetImage("imageName");

        base.Start();
    }

    public override void Respond(int n)
    {
        payTab = n;

        switch (n)
        {
            case 0:

                text.text = "Seams fair!";
                Player.money -= tab;
                AliciaFriendShip++;
                break;
            case 1:

                text.text = "i- but you didnt have to do that- :( thank you so much!!!!! i swear ill pay it next time!!!";
                Player.money -= tabTogether;
                AliciaFriendShip += 2;

                break;
            case 2:

                text.text = "Sure hon~~! You take it next time, alright~~?";

                break;
        }

        base.Respond(n);
        //AltResponse(Evnt.nextEvent);
    }
}
