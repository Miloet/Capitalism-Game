using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static float startingMoney;
    public static float money = 2f;
    public static int stress = -5;
    public static string name = "Boss";
    public static int str = 0;
    public static int turn;

    public static Sprite sprite;

    public static Attack[] openingAttacks;
    public static Attack[] repeatingAttacks;

    public void Attack()
    {
        print("turn before attack " + turn);
        if (turn < openingAttacks.Length) StartCoroutine(openingAttacks[turn].DoAttack());
        else
        {
            StartCoroutine(repeatingAttacks[(turn - openingAttacks.Length) % repeatingAttacks.Length].DoAttack());
        }
        turn++;
    }
    public static string DisplayAttack()
    {
        int currentAttack = turn;

        if (turn < openingAttacks.Length) return openingAttacks[currentAttack].GetAttack();
        else return repeatingAttacks[(currentAttack- openingAttacks.Length)% repeatingAttacks.Length].GetAttack();
    }

    public static float GetStressValue()
    {
        if (stress > -5) return -stress / 10f + 1f;
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
        string attack = $"<color=grey><size=25>{name}</size></color>\n";
        if (damage * times > 0)
        {
            attack += $"<size={GetSize(damage + Enemy.str)}>{damage + Enemy.str}</size> ";
            if (times > 1) attack += $"x <size={GetSize(times,10,1)}>{times}</size> ";
            if (Enemy.stress != 1) attack += $"x ({(Enemy.GetStressValue() * 100f):N1}%) ";
        }
        if (types.Length > 0 || types[0] == AttackType.Damage)
        {
            attack += "\nSpecial Effect ";
        }

        return attack;
    }

    public static int GetSize(float value, float maxValue = 1000, float minValue = 100, int maxSize = 15, int minSize = 10)
    {
        if (value < minValue) return minSize;
        else if (value > maxValue) return maxSize;
        else
        {
            float normalizedValue = Mathf.InverseLerp(minValue, maxValue, value);
            int interpolatedSize = Mathf.RoundToInt(Mathf.Lerp(minSize, maxSize, normalizedValue));

            return interpolatedSize;
        }
    }

    public IEnumerator DoAttack()
    {
        for(int i = 0; i < times; i++)
        {
            Player.TakeDamage(Mathf.Max(damage + Enemy.str, damage * 0.5f));
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
