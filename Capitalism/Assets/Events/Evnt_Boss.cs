using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_Boss : MonoEvent
{
    public override void Start()
    {
        name = "First of five, Security.";


        string s = AliciaEnding.Ending ? "You created a new trigo- tetragon with that women... I am sure Holmes would hire you as one of us if you truely wanted. But..." : "";
        monolog = new string[] { $"Well well well... here you are. A no brainer going for me first i supose. " + s,
            "You seam to have made up your mind."
        };



        responses = new string[] {"My name is Dawn Morningstar, i wont rest until the rot of this world is cleansed."};

        eventImage = GetImage("Security");

        base.Start();
    }

    public override void Respond(int n)
    {
        
        text.text = "So be it.";
        StartCoroutine(Combat());
               
        base.Respond(n);
        //AltResponse(Evnt.nextEvent);
    }

    public override IEnumerator Combat()
    {
        yield return new WaitUntil(() => monologAnimator.allLettersShown && Input.anyKeyDown);

        Attack[] opening = { 
                new Attack("Show me what you got.", 0, 0, Attack.AttackType.Damage),
                new Attack($"Pathetic.", 5, 1000, Attack.AttackType.Damage)};
        Attack[] repeating = {
                new Attack("First: Hollow Life", 0, 0, Attack.AttackType.GainHealth),
                new Attack("Second: Complete Death", 50, 10, Attack.AttackType.Damage),
                !AliciaEnding.Ending ? new Attack("Third: Infinite Finance", 0, 0, Attack.AttackType.IncreaseDamage, Attack.AttackType.ReduseStress, Attack.AttackType.IncreaseDamage, Attack.AttackType.IncreaseDamage,Attack.AttackType.ReduseStress) :
                new Attack("Nothing...?", 0, 0, Attack.AttackType.Damage)
                };

        yield return new WaitForSeconds(0.5f);

        StartCombat.StartCombatWithEnemy("Head of Security", 150000, -5, "Security", opening, repeating, Evnt.End, 2000);
    }


}
