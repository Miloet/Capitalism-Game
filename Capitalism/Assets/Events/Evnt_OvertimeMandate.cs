using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_OvertimeMandate : MonoEvent
{
    public override void Start()
    {
        
        name = "Non-mandatory Mandatory Overtime";

        monolog = new string[] {$"<i>The boss has called for mandatory overtime at the offices."};
        responses = new string[] { "Accept the orders", "Ignore the orders"};

        eventImage = GetImage("Party");

        base.Start();
    }

    public override void Respond(int n)
    {
        switch (n)
        {
            case 0:
                text.text = "You got an overtime bonus for you hard work.";
                Player.stress += 1;
                Player.tempExpenses.Add(new Expense("Overtime bonus", -4000));
                break;
            case 1:
                text.text = "You recived disiplanary action.";
                Player.tempExpenses.Add(new Expense("Disiplanary action", 1000));
                break;
        }
        base.Respond(n);
    }
}
