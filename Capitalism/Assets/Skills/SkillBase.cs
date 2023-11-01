using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillBase : CardBehavior
{
    public bool requireAsset = false;

    public bool burnt = false;

    public string letter = "0";
    public new string name = "Null";
    public string description = "Does nothing.";

    public string spriteResourcePath = "Skills/Error";
    SpriteRenderer image;
    TextMeshPro displayName;
    TextMeshPro displayDescription;

    public static GameObject fireParticles;
    public static GameObject blessParticles;

    public virtual void Start()
    {
        if (fireParticles == null) fireParticles = Resources.Load<GameObject>("Burning");
        if (blessParticles == null) blessParticles = Resources.Load<GameObject>("Blessed");

        if(Player.stress != 0)
        {
            float f = Random.value;

            if (f < Mathf.Abs(Player.stress) / 10f)
            {
                if(Player.stress > 0)Instantiate(fireParticles, transform);
                else Instantiate(blessParticles, transform);
                burnt = true;
            }
        }

        assetPlace = transform.Find("AssetInput/AssetPosition");

        image = transform.Find("Picture").GetComponent<SpriteRenderer>();
        displayName = transform.Find("Name").GetComponent<TextMeshPro>();
        displayDescription = transform.Find("Body").GetComponent<TextMeshPro>();

        image.sprite = Resources.Load<Sprite>(spriteResourcePath);
        displayName.text = letter + " - " + name;
        displayDescription.text = description;

        if (!requireAsset) transform.Find("AssetInput").gameObject.SetActive(false);

    }
    public virtual void Effect(float multipier = 1f)
    {
        if (letter != "B") CardCompiler.multiplier = 1f;
        print($"{name} effect played");
    }

    public virtual string writeEffect()
    {
        return "Nothing.";
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

    public float GetMultiplier()
    {
        if(!burnt) return 1f;
        else
        {
            if(Player.stress > 0) return 0f;
            else return 2f;
        }
    }
}
