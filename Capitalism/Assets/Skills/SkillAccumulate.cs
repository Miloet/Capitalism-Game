using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAccumulate : SkillBase
{
    public override void Start()
    {
        requireAsset = true;

        letter = "A";
        name = "Accumulate";
        description = "Procure intrest on money giving you money equal to a stock!";

        spriteResourcePath = "Skills/Accumulate";

        base.Start();
    }

    public override void Effect(float multiplier = 1f)
    {
        Player.money += currentAsset.value * multiplier;

        base.Effect(multiplier);
    }
}
