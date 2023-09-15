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
        description = "Use your Assets to damage the opponents cash. Deal 2.5 x Price dmg";

        spriteResourcePath = "Skills/Accumulate";

        base.Start();
    }

    public override void Effect(float multiplier = 1f)
    {
        Enemy.money -= currentAsset.value * multiplier * 2.5f;

        base.Effect();
    }

    public override string writeEffect()
    {
        string effect = "<color=red>(no asset)</color>";
        if (currentAsset != null) effect = (currentAsset.value * 2.5f).ToString();
        return $"Deal {effect} damage";
    }
}
