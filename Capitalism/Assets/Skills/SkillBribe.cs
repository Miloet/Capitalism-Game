using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBribe : SkillBase
{
    public override void Start()
    {
        requireAsset = true;

        letter = "B";
        name = "Bribe";
        description = "Bribe lawmakers to make new laws in your favor. Multiply your next cards effect by 1 + Price / 100.";

        spriteResourcePath = "Skills/Bribe";

        base.Start();
    }

    public override void Effect(float multiplier = 1f)
    {
        CardCompiler.multiplier = multiplier * (1 + currentAsset.value / 100f);

        base.Effect();
    }
    public override string writeEffect()
    {
        string effect = "<color=red>(no asset)</color>";
        if (currentAsset != null) effect = ((100 + currentAsset.value) * GetMultiplier()).ToString("0.0");
        return $"Increase next cards effect by {effect}%";
    }
}
