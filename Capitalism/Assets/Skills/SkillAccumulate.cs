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
        description = $"Deal {FinancialDamage("(+35%)", true)} to the enemy.";

        spriteResourcePath = "Skills/Accumulate";

        base.Start();
    }

    public override void Effect(float multiplier = 1f)
    {
        Enemy.money -= currentAsset.value * multiplier * .35f;

        base.Effect();
    }

    public override string writeEffect()
    {
        string effect = NoAsset;
        if (currentAsset != null) effect = (currentAsset.value * .35f * GetMultiplier()).ToString("N0");
        return $"Deal {FinancialDamage(effect)}";
    }
}
