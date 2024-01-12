using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static float startingMoney;
    public static float money = 100000f;
    public static int stress = -5;
    public static string name = "Boss";
    public static int str = 0;
    public static int turn;

    public static Sprite sprite;

    public static Attack[] openingAttacks;
    public static Attack[] repeatingAttacks;

    public void Attack()
    {
        if (turn < openingAttacks.Length) StartCoroutine(openingAttacks[turn].DoAttack());
        else
        {
            StartCoroutine(repeatingAttacks[turn-openingAttacks.Length].DoAttack());
        }
        turn++;
        turn = (turn % (openingAttacks.Length + repeatingAttacks.Length)) + openingAttacks.Length * (turn / openingAttacks.Length);
    }
    public static string DisplayAttack()
    {
        print($"It is turn {turn} for the enemy and it has {openingAttacks.Length + repeatingAttacks.Length} attacks");
        if (turn < openingAttacks.Length) return openingAttacks[turn].GetAttack();
        else return repeatingAttacks[turn-openingAttacks.Length].GetAttack();
    }

    public static float GetStressValue()
    {
        if (stress < -4) return -stress / 10f + 1f;
        else return Mathf.Pow(-stress - 5 + Mathf.Sqrt(6f) / 2f, 2);

    }
}

public class Attack
{
    public string name;

    public bool special;


    public int times;
    public float damage;
    public AttackType[] types;

    public enum AttackType
    {
        Damage,
        ReduseStress,
        IncreaseStress,
        IncreaseDamage,

    }
    public Attack(string Name, int TimesToHit, float Damage, params AttackType[] attackType)
    {
        name = Name;
        times = TimesToHit;
        damage = Damage;

        types = attackType;
    }
    public string GetAttack()
    {
        string attack = $"{name}     -     ";
        if (damage*times > 0)
        {
            attack += $"{damage + Enemy.str} ";
            if (times > 1) attack += $"x {times} ";
            if (Enemy.stress != 1) attack += $"x ({Enemy.GetStressValue()/100f:N0}%) ";
        }
        if(types.Length > 0 || types[0] == AttackType.Damage)
        {
            attack += "Special ";
        }

        return attack;
    }

    public IEnumerator DoAttack()
    {
        for(int i = 0; i < times; i++)
        {
            Player.TakeDamage(damage + Enemy.str);
            yield return new WaitForSeconds(0.1f);
        }

        foreach(AttackType type in types)
            switch(type)
            {
                case AttackType.IncreaseStress:
                    Player.stress++;
                    break;
                case AttackType.ReduseStress:
                    Enemy.stress--;
                    break;
                case AttackType.IncreaseDamage:
                    Enemy.str += 100;
                    break;
            }
    }
}
