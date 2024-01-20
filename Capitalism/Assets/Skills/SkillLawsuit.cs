using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLawsuit : SkillBase
{
    public override void Start()
    {
        requireAsset = false;

        letter = "L";
        name = "Lawsuit";
        description = $"Gain {GainStress("+3")}. If you have more money than the enemy then gain all of their money. Other wise the reverse happends.";

        spriteResourcePath = "Skills/Lawsuit";

        base.Start();
    }

    public override void Effect(float multiplier = 1f)
    {
        Player.stress += 3;
        if (Enemy.money < Player.money)
        {
            Player.money += Enemy.money;
            Enemy.money = 0;
        }
        else
        {
            Enemy.money += Player.money;
            Player.money = 0;
        }

        base.Effect();
    }
    public override string writeEffect()
    {
        string effect = "Start Lawsuit";

        return $"{effect}";
    }
}
