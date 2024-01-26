using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_Taxman : MonoEvent
{

    public static bool hasLawyer = false;
    public override void Start()
    {
        name = "Taxman";

        monolog = new string[] {
            "Hello ma'am. My name is Gill Maceson, i am from the IRS tax department. I can see that look on your face and dont worry youre not in trouble.",
            "You have resently entred the job market and found a job by the looks of things, i am just here to inform you about your finances and taxes.",
            "You are currently in the <i>employed tax bracket</i> meaning you pay 30% tax on all non-investment related income.",
            "The bracket bellow you is the <i>low income</i> bracket that has a 75% tax. Its that high to insetivice working harder and not slipping up with paycuts.",
            "The above brackets, that you will hopefully soon be part of, are 5,000$ at 20%, 10,000$ at 10%, 80,000$ at 5% and so on.",
            "Riiiight... Control Inc is part of <i><wave>that deal.</wave></i> You get to choose between a months meal ticket at corpos food, just down the street, or a 1000$ bonus."
        };
        responses = new string[] { "Meal ticket (less food cost and stress)", "1000$ Bonus" };

        eventImage = GetImage("Taxman");

        base.Start();
    }

    public override void Respond(int n)
    {
        text.text = "Good choice. Well i'll be leaving now. Good luck in the corprate life, you will need it.";
        switch (n)
        {
            case 0:
                Player.stress -= 2;
                Expense.ChangeExpense("Food and Rent", 300);
                break;
            case 1:
                Player.tempExpenses.Add(new Expense("Bonus", -1000f));
                break;
        }

        base.Respond(n);
    }
}
