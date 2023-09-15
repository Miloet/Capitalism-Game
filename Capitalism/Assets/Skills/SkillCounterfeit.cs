using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCounterfeit : SkillBase
{
    public override void Start()
    {
        requireAsset = true;

        letter = "C";
        name = "Counterfeit";
        description = "Use an asset to print fake money equal to 10 times its value. Add 1 stress.";

        spriteResourcePath = "Skills/Counterfeit";

        base.Start();
    }

    public override void Effect(float multiplier = 1f)
    {
        Player.money += currentAsset.value * multiplier * 10f;

        Player.stress++;

        base.Effect();
    }
    public override string writeEffect()
    {
        string effect = "<color=red>(no asset)</color>";
        if (currentAsset != null) effect = (currentAsset.value * 10f).ToString();
        return $"Gain {effect} money";
    }
}
