using UnityEngine;

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
}