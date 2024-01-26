using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_NewWorkEquipment : MonoEvent
{
    public override void Start()
    {
        
        name = "New Equipment";

        monolog = new string[] { $"<i>New experimental equipment has come in for work. To learn how the new equipment works will take time and might lead to a raise."
        , "However if it doesnt pan out it will be time wasted. What to do?"};
        responses = new string[] { "Adapt to the new (30% chance)", "Stay with the old (70% chance)" };
        eventImage = GetImage("Party");

        base.Start();
    }

    public override void Respond(int n)
    {

        float random = Random.value;

        switch (n)
        {
            case 0:
                
                if(random <= 0.3f)
                {
                    text.text = "Your bet paid off and you got a raise of 2000$ for being the best person capable of using and teaching about the new equipment.";
                    Player.income += 2000;
                }
                else
                {
                    text.text = "The equipment was thrown out and you wasted a months time. You recived disciplinary action losing 500$ of income.";
                    Player.income -= 500;
                }
                break;
            case 1:

                if (random <= 0.7f)
                {
                    text.text = "Your bet paid off and you got a raise of 500$.";
                    Player.income += 500;
                }

                break;
        }

        RemoveRandomEvent(Evnt.NewWorkEquipment);
        base.Respond(n);
    }
}
