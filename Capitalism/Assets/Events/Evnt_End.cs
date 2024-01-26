using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_End : MonoEvent
{
    public override void Start()
    {
        name = "The end.";

        string homophobic = AliciaEnding.Ending ? "take that pretty girlfriend of yours and " : "";
        string gay = AliciaEnding.Ending ? "...i'll be completly honest, alicia kissed be right before i entered and i havent been able to focus on a single word you have said" : "idk :P";

        monolog = new string[] { "<i>...", 
            "haha... to imagine... such power...", 
            "it took me what? 10 years to get here? it even took holmes two years to get to this point...",
            $"kid... {homophobic} go fuck yourself. t-tell me... are you this strong because of primoridal luck...? or are you this strong because of divine suicide...?"};
        responses = new string[] {gay};

        eventImage = GetImage("Defeated");

        base.Start();
    }

    public override void Respond(int n)
    {
        StartCoroutine(StartFinale());

        for (int i = 0; i < Mathf.Min(responses.Length, responsButtons.Length); i++)
        {
            responsButtons[i].onClick.RemoveAllListeners();
            StartCoroutine(ReverseButtonAnimation(i));
        }
        base.Respond(n);

    }

    public IEnumerator StartFinale()
    {
        text.text = "a Natural fucking genius yet it doesnt seam like you even know what a trigone is... stupid l-lucky ba... sta... r-";
        
        yield return new WaitUntil(() => monologAnimator.allLettersShown);
        yield return new WaitForSeconds(.5f);
        yield return new WaitUntil(() => Input.anyKeyDown);

        text.text = "<i>the final light of his soul fades as his value hits the hard negatives.";

        yield return new WaitUntil(() => monologAnimator.allLettersShown);
        yield return new WaitForSeconds(.5f);
        yield return new WaitUntil(() => Input.anyKeyDown);

        text.text = "<i>this is the end... for now...";

        yield return new WaitUntil(() => monologAnimator.allLettersShown);
        yield return new WaitForSeconds(.5f);
        yield return new WaitUntil(() => Input.anyKeyDown);

        Application.Quit();
    }
}
