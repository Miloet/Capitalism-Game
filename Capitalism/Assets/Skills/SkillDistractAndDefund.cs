using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDistractAndDefund : SkillBase
{
    public override void Start()
    {
        requireAsset = false;

        letter = "D";
        name = "Distract and Defund";
        description = "The enemy permanantly deals 100$ less finacial damage per attack.";

        spriteResourcePath = "Skills/Distract";

        base.Start();
    }

    public override void Effect(float multiplier = 1f)
    {
        Enemy.str -= Mathf.FloorToInt(100 * multiplier);

        base.Effect();
    }
    public override string writeEffect()
    {
        string effect = "Enemy loses 100$ of financial strength";

        return $"{effect}";
    }
}
