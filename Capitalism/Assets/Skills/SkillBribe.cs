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
        description = $"Increase your next cards effect by {GainMoney(MultiplierAssetValue("(+1%)"))}%";

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
        string effect = NoAsset;
        if (currentAsset != null) effect = (Mathf.Sqrt(currentAsset.value / 50f) * GetMultiplier() * 100f).ToString("N1");
        return $"Increase next cards effect by {GainMoney(effect)}%";
    }
}
