using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evnt_ : MonoEvent
{
    public override void Start()
    {
        name = "title";

        monolog = new string[] {"first monolog" , "second monolog" };
        responses = new string[] { "first repons", "second repons" };

        eventImage = GetImage("imageName");

        base.Start();
    }

    public override void Respond(int n)
    {
        switch (n)
        {
            case 0:

                text.text = "retort to player respons 1";

                break;
            case 1:

                text.text = "retort to player respons 2";

                break;
        }

        base.Respond(n);
        //AltResponse(Evnt.nextEvent);
    }
}
