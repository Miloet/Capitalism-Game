using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WeightedSkillsData", menuName = "ScriptableObjects/WeightedSkillsData", order = 1)]
public class SkillWeightList : ScriptableObject
{
    [System.Serializable]
    public class WeightedSkill
    {
        public char skill;
        public float weight;
    }

    public WeightedSkill[] WeightedSkills;

    public char GetRandomSkill()
    {
        float totalWeight = 0f;

        foreach (WeightedSkill WeightedSkill in WeightedSkills)
        {
            totalWeight += WeightedSkill.weight;
        }

        float randomValue = Random.Range(0f, totalWeight);

        foreach (WeightedSkill WeightedSkill in WeightedSkills)
        {
            if (randomValue <= WeightedSkill.weight)
            {
                return WeightedSkill.skill;
            }

            randomValue -= WeightedSkill.weight;
        }

        // Fallback: return the last string in case of any issues
        return WeightedSkills[WeightedSkills.Length - 1].skill;
    }

    public char GetUniqueRandomSkill(List<char> selectedSkills)
    {
        List<WeightedSkill> availableSkills = WeightedSkills
            .Where(ws => !selectedSkills.Contains(ws.skill))
            .ToList();

        if (availableSkills.Count == 0)
        {
            // If all skills have been selected, reset the selectedSkills list
            selectedSkills.Clear();
            availableSkills = WeightedSkills.ToList();
        }

        float totalWeight = 0f;

        foreach (WeightedSkill weightedSkill in availableSkills)
        {
            totalWeight += weightedSkill.weight;
        }

        float randomValue = Random.Range(0f, totalWeight);

        foreach (WeightedSkill weightedSkill in availableSkills)
        {
            if (randomValue <= weightedSkill.weight)
            {
                return weightedSkill.skill;
            }

            randomValue -= weightedSkill.weight;
        }

        // Fallback: return the last skill in case of any issues
        return availableSkills[availableSkills.Count - 1].skill;
    }

}