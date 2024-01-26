using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEmbezzle : SkillBase
{
    public override void Start()
    {
        requireAsset = true;

        letter = "E";
        name = "Embezzle";
        description = $"Embezzle {FinancialDamage("(+20%)", true)} of the enemies money and gain it as your own causing them {GainStress("+1")} for every 1000$ of financial damage up to {GainStress("+3")}. " +
            $"This ability becomes more effective the more stress you have ranging from {GainMoney("0% - 200%")} based on{GainStress("")}";

        spriteResourcePath = "Skills/Embezzle";

        base.Start();
    }

    public override void Effect(float multiplier = 1f)
    {
        float stressEffect = Mathf.Clamp(1 + Player.stress * 0.2f, 0, 2);
        float damage = currentAsset.value * 0.20f * multiplier * stressEffect;
        Enemy.money -= damage;

        Enemy.stress += Mathf.FloorToInt(Mathf.Clamp(damage / 1000, 0, 3));
        Player.money += damage;

        base.Effect();
    }
    public override string writeEffect()
    {
        string effect = NoAsset;
        if (currentAsset != null) effect = (currentAsset.value * 0.2f * GetMultiplier() * Mathf.Clamp(1 + Player.stress * 0.2f, 0, 2)).ToString("N0");
        return $"Deal {FinancialDamage(effect)} and gain {GainMoney(effect)}";
    }
}
