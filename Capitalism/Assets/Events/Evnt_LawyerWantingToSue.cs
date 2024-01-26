using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_LawyerWantingToSue : MonoEvent
{

    public string[] killed = new string[] { "Grandma", "Goldfish", "Husband's girlfriend", "Favorite plant", "Lucky charm", "Pet rock", "Neighbor's bonsai tree", "Best friend's cactus", "Classroom hamster", "Boss's fern", "Aunt's succulent", "Nephew's imaginary friend", "Co-worker's office plant", "Local celebrity's potted herb garden" };

    public string sueForKilled;

    public override void Start()
    {
        name = "Lawyers...";


        sueForKilled = killed[Random.Range(0, killed.Length)];
        monolog = new string[] { $"oooooooooooohhhh you freakshits done it now. <shake>YOUR PRODUCT KILLED MY {sueForKilled.ToUpper()}.</shake> I WILL SUE RIGTH NOW. PREPARE TO DIE IDIOT!!!!",
        };
        responses = new string[] {"what the actual fuck are you on about-", "huh?"};
        if (Evnt_Lawyer.hasLawyer) responses[1] = "Call lawyer";

        eventImage = GetImage("Iz Mezzed Upman");

        base.Start();
    }

    public override void Respond(int n)
    {
        switch (n)
        {
            case 0:

                text.text = ">>>>>>>>>>>>>>>>>>>:[[[[[[[[[[[";
                StartCoroutine(Combat());
                
                break;
            case 1:

                if (!Evnt_Lawyer.hasLawyer)
                {
                    text.text = ">>>>>>>>>>>>>>>>>>>:[[[[[[[[[[[";
                    StartCoroutine(Combat());
                }
                else
                {
                    text.text = $"<i>Saul Goodman here. Aaaahhh dawn- huh? control did what now? a {sueForKilled}? and how is this your probelm? he wants to sue you? fucking idiot of a lawyer" +
                        $" <size=30><wave>BET HE DOESNT EVEN HAVE A LAW DEGREE.</wave></size> He heard that right~? Anyways call security and i'll fix it behind closed doors and you will get a cut from it.";
                    Player.tempExpenses.Add(new Expense("This is why you hired me~", -3000));
                    Player.AddCard(Resources.Load<SkillWeightList>("LawyerSkillPool").GetRandomSkill());
                }
                break;
        }
        base.Respond(n);
        //AltResponse(Evnt.nextEvent);
    }

    public override IEnumerator Combat()
    {

        yield return new WaitUntil(() => monologAnimator.allLettersShown && Input.anyKeyDown);

        Attack[] opening = { 
                new Attack("Legal nonesense!", 0, 0, Attack.AttackType.IncreaseDamage),
                new Attack($"YOU KILLED MY {sueForKilled}!!!", 1, 1000, Attack.AttackType.Damage)};
        Attack[] repeating = { 
                new Attack("Racial slurs!!", 10, 50, Attack.AttackType.Damage),
                new Attack("Fake law degree", 0, 0, Attack.AttackType.IncreaseDamage)
                };

        yield return new WaitForSeconds(0.5f);

        StartCombat.StartCombatWithEnemy("Iz \"Mezzed\" Upman", Random.Range(13000, 15000), 0, "Iz Mezzed Upman", opening, repeating, Evnt.Victory, 2000);
    }


}
