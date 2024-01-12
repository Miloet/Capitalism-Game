using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_Karen : MonoEvent
{
    public override void Start()
    {
        name = "Customer";

        monolog = new string[] { "Ma'am, i would like to return this product pleaaase~", 
            "<i>She hands you an expired can of baked beans... the experation date is 1985. The company it comes from is a reacently aquired subsidiary of CONTROL Inc.",
            "",

        };
        responses = new string[] {"I... I cant refund you for this... Where did you even get this?" };

        eventImage = GetImage("Karen");

        base.Start();
    }

    public override void Respond(int n)
    {
        switch (n)
        {
            case 0:

                text.text = ">:0!!!!! I WILL NEED TO SPEAK WITH YOUR MANAGER!!! " +
                    "THE PEOPLE AT CORPOS FOOD TOLD ME THAT YOURE THE SUITS OF THE " +
                    "OPPERATION SO I WENT TO YOU AFTER GETTING KICKED OUT AND I GUESS I HAVE TO GO THROUGH YOU TOO >:[";

                break;
        }
        StartCoroutine(Combat());
        //base.Respond(n);
        //AltResponse(Evnt.nextEvent);
    }

    public override IEnumerator Combat()
    {
        yield return new WaitUntil(() => monologAnimator.allLettersShown && Input.anyKeyDown);

        Attack[] opening = { new Attack("Give me you manager!", 3, 150, Attack.AttackType.IncreaseDamage),
                new Attack("Objection! Call Lawyer!", 0, 0, Attack.AttackType.IncreaseStress,Attack.AttackType.IncreaseDamage,Attack.AttackType.ReduseStress)};
        Attack[] repeating = { new Attack(">:[!", 5, 50, Attack.AttackType.Damage),
                        new Attack("angy!!!", 1, 150, Attack.AttackType.IncreaseDamage)
                };

        yield return new WaitForSeconds(0.5f);

        StartCombat.StartCombatWithEnemy("Karen", 1792, 5, "Karen", opening, repeating);
    }


}
