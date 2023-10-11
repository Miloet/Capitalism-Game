using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillBase : CardBehavior
{
    public bool requireAsset = false;

    public string letter = "0";
    public new string name = "Null";
    public string description = "Does nothing.";

    public string spriteResourcePath = "Skills/Error";
    SpriteRenderer image;
    TextMeshPro displayName;
    TextMeshPro displayDescription;



    public virtual void Start()
    {
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
}
