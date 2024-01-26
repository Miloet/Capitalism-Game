using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_Spa : MonoEvent
{
    public override void Start()
    {
        
        name = "Spa?";

        monolog = new string[] {$"<i>Alicia comes into your office and slams down a flyer for a spa on your desk.", 
            "You have been stressed lately, i can see it on you. Lets go to this spa. Its said to remove all your stress. <size=25>It is kinda expensive though...</>"};
        responses = new string[] { "Sure, I'd love. (reduse stress massivly, costs 2000$)", "It's a bit too expensive..."};

        eventImage = GetImage("Party");

        base.Start();
    }

    public override void Respond(int n)
    {
        switch (n)
        {
            case 0:
                text.text = "<wave>...you actually said yes... i love you</wave>";
                int s = Player.stress;
                Player.stress = 0;
                if(s <= 3) Player.stress -= 3 - s;
                AliciaFriendShip += 2;
                Player.tempExpenses.Add(new Expense("Spa week with the gf", 2000));

                RemoveRandomEvent(Evnt.Spa);

                break;
            case 1:
                text.text = "Hehe, yeah i figured you'd say that. Oh well~ <i>She gives you a peck on the cheek.";
                break;
        }


        base.Respond(n);
    }
}
