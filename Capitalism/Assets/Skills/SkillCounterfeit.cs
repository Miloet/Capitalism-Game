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
        description = "Gain X (price * 0.7) <color=green>Money</color> and add <color=red>1 Stress";

        spriteResourcePath = "Skills/Counterfeit";

        base.Start();
    }

    public override void Effect(float multiplier = 1f)
    {
        Player.money += currentAsset.value * multiplier * 0.7f;

        Player.stress++;

        base.Effect();
    }
    public override string writeEffect()
    {
        string effect = "<color=red>(no asset)</color>";
        if (currentAsset != null) effect = (currentAsset.value * 0.7f * GetMultiplier()).ToString();
        return $"Gain {effect} <color=green>Money";
    }
}
