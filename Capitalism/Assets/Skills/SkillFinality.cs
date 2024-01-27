using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFinality : SkillBase
{
    public override void Start()
    {
        requireAsset = false;

        letter = "F";
        name = "Finality";
        description = $"Destroy <b><color=#b83100>30% of the financial value</color></b> to the enemies finances.";

        spriteResourcePath = "Skills/Finality";

        base.Start();
    }

    public override void Effect(float multiplier = 1f)
    {
        Enemy.money -= Enemy.money * 0.3f * Mathf.Sqrt(multiplier);

        base.Effect();
    }

    public override string writeEffect()
    {
        string effect = (Enemy.money * 0.3f * Mathf.Sqrt(GetMultiplier())).ToString("N0");
        return $"Destroy <b><color=#b83100>{effect}$ financial value</color></b>";
    }
}
