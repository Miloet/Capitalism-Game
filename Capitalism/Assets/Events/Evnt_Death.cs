using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_Death : MonoEvent
{
    public override void Start()
    {
        name = "An end.";
        
        monolog = new string[] {"<i>Through your financial failure your own value has become negative.",
            "<i>There is no cure but death is a good way to \"heard the cattle\" so to speak.",
            "<i>I will give you a luxury few people have gotten. Choose your death."};
        responses = new string[] {"Instant", "Pleasant", "A second chance?"};

        eventImage = GetImage("Victory");

        base.Start();
    }

    public override void Respond(int n)
    {
        Application.Quit();
    }
}
