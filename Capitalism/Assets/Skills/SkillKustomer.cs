using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillKustomer : SkillBase
{
    public override void Start()
    {
        requireAsset = false;

        letter = "K";
        name = "Kustomer";
        description = $"Deal {FinancialDamage("300", true)} and cause the enemy {GainStress("+1")}";

        spriteResourcePath = "Skills/Kustomer";

        base.Start();
    }

    public override void Effect(float multiplier = 1f)
    {
        Enemy.stress++;
        Enemy.money -= 300;

        base.Effect();
    }
    public override string writeEffect()
    {
        string effect = $"Deal {FinancialDamage("300")} and cause {GainStress("+1")}";

        return $"{effect}";
    }
}
