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
        description = "Increase your next cards effect by X (price)";

        spriteResourcePath = "Skills/Bribe";

        base.Start();
    }

    public override void Effect(float multiplier = 1f)
    {
        CardCompiler.multiplier = multiplier * (1 + Mathf.Sqrt(currentAsset.value/50f));

        base.Effect();
    }
    public override string writeEffect()
    {
        string effect = "<color=red>(no asset)</color>";
        if (currentAsset != null) effect = (Mathf.Sqrt(currentAsset.value) * GetMultiplier()).ToString("0.0");
        return $"Increase next cards effect by {effect}x";
    }
}
