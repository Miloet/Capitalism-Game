using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_Party : MonoEvent
{
    public override void Start()
    {
        
        name = "Party?";

        monolog = new string[] {$"<i>The higher ups needs this work done by tomorrow morning... i might get a raise if i pull an all-nighter on this one.",
            "<i>You hear the familiar clacks of the pair of heels that a woman you know wears. Alicia pops her head into your office.",
            $"Hey hon~ Wanna go with me and the boys to the pub tonight? {(Evnt_Bill.YouPaid ? "The tabs on me this time~<3" : "")}"};
        responses = new string[] { "I'd love to Alicia. (reduse stress)", "Sorry, i cant tonight. I got a lot of work to do. (pay rise)"};

        eventImage = GetImage("Party");

        base.Start();
    }

    public override void Respond(int n)
    {
        switch (n)
        {
            case 0:

                Evnt_Bill.YouPaid = false;
                if(AliciaFriendShip >= 4)
                {
                    text.text = "Amazing! <i>She runs up and gives you a quick friendly kiss on cheek.</i> <wave>See you tonight then babe~</wave>";
                    Player.stress -= 2;
                }
                else
                {
                    text.text = "Amazing! <i>She runs up and gives you a quick hug.</i> <wave>See you tonight then~</wave>";
                    Player.stress--;
                }
                AliciaFriendShip++;
                break;
            case 1:
                string hardWorkPaysOff = "<i>\n\n...Thanks to your hard work you have gotten a raise of 500$ per month...";
                if (AliciaFriendShip >= 3)
                {
                    text.text = "Oh... I'll stay with you at the office. Two heads are better than one after all. Just tell me what needs to be done boss!" + 
                        hardWorkPaysOff + 
                        "\n...In addition Alicia got the same raise for her hard work...";
                    AliciaFriendShip++;
                }
                else
                {
                    text.text = "Oh... i am sorry. maybe next time..." + hardWorkPaysOff;
                    Player.stress++;
                    
                }
                Player.income += 500;
                break;
        }


        base.Respond(n);
    }
}
