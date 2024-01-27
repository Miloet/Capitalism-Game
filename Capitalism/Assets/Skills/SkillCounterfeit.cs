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
        description = $"Print {GainMoney($"{MultiplierAssetValue("(+80%)")} Money")} and gain {GainStress("+1")} for every 1,000$ printed.";

        spriteResourcePath = "Skills/Counterfeit";

        base.Start();
    }

    public override void Effect(float multiplier = 1f)
    {
        Player.money += currentAsset.value * multiplier * 0.80f;

        Player.stress += Mathf.Clamp((int)Mathf.FloorToInt((currentAsset.value * 0.80f * multiplier) / 1000f),0, 9);

        base.Effect();
    }
    public override string writeEffect()
    {
        string effect = NoAsset;
        if (currentAsset != null) effect = (currentAsset.value * 0.80f * GetMultiplier()).ToString("N0");
        return $"Gain {GainMoney(effect)} and {(int)Mathf.FloorToInt((currentAsset.value * 0.80f * GetMultiplier())/1000f)}";
    }
}
