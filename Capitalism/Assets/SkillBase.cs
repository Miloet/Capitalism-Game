using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillBase : CardBehavior
{
    public int id;
    public bool requireAsset = false;

    public string letter = "0";
    public string name = "Null";
    public string description = "Removes 1 Stress and gives 100$";

    string spriteResourcePath = "Resources/SkillImages/Error";
    SpriteRenderer image;
    TextMeshPro displayName;
    TextMeshPro displayDescription;



    private void Start()
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
    public virtual void Effect(float multiplier = 1f)
    {
        Player.stress -= Game.IntMultiply(1 * multiplier);
        Player.money += 100f * multiplier;
        print($"{name} effect played");
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
