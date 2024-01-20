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
        description = $"Print {GainMoney($"{MultiplierAssetValue("(+10%)")} Money")} and gain {GainStress("+1")}";

        spriteResourcePath = "Skills/Counterfeit";

        base.Start();
    }

    public override void Effect(float multiplier = 1f)
    {
        Player.money += currentAsset.value * multiplier * 0.1f;

        Player.stress++;

        base.Effect();
    }
    public override string writeEffect()
    {
        string effect = NoAsset;
        if (currentAsset != null) effect = (currentAsset.value * 0.1f * GetMultiplier()).ToString("N0");
        return $"Gain {GainMoney(effect)}";
    }
}
