using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : CardBehavior
{
    public int id;
    public bool requireAsset = false;

    public string name = "Null";
    public string description = "Removes 1 Stress and gives 1$";

    public virtual void Effect()
    {
        Player.stress--;
        Player.money++;
    }

    public bool Validate()
    {
        if (requireAsset)
            if (currentAsset != null) return true;
            else return false;
        else return true;
    }

    public string ValidateReason()
    {
        if (requireAsset)
            if (currentAsset != null) return "This is a validation Error.\n";
            else return $"{name} card does not have an Asset!\n";
        else return "This is a validation Error.\n";
    }
}