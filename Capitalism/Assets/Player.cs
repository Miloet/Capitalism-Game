using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float money = 1000;
    public static int stress = 0;

    public static char[] skills = {'A','A','B','B','C'};
    public static Stock[] assets = {new Stock("APPL")};

    public static float assetValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        var c = Resources.Load<GameObject>("Card");
        var a = Resources.Load<GameObject>("Asset");
        //updateValue();

        foreach (char skill in skills)
        {
            assignSkill(Instantiate(c), skill);
        }
        MouseInput.updateCardCount();

        foreach(Stock asset in assets)
        {
            Instantiate(a).GetComponent<Asset>().Assign(asset);
        }
    }

    public void updateValue()
    {
        float value = 0;
        foreach(Stock a in assets)
        {
            value += a.getValue(1);
        }
        assetValue = value;
    }

    private static void assignSkill(GameObject newCard, char skill)
    {
        switch (char.ToUpper(skill)) // Convert skill to uppercase to handle both upper and lower case letters
        {
            case 'A':
                newCard.AddComponent<SkillAccumulate>();
                break;
                /*
            case 'B':
                //newCard.AddComponent<SkillB>();
                break;

            case 'C':
                //newCard.AddComponent<SkillC>();
                break;

            #region Unused Letters

            case 'D':
                // newCard.AddComponent<SkillD>();
                break;

            case 'E':
                // newCard.AddComponent<SkillE>();
                break;

            case 'F':
                // newCard.AddComponent<SkillF>();
                break;

            case 'G':
                // newCard.AddComponent<SkillG>();
                break;

            case 'H':
                // newCard.AddComponent<SkillH>();
                break;

            case 'I':
                // newCard.AddComponent<SkillI>();
                break;

            case 'J':
                // newCard.AddComponent<SkillJ>();
                break;

            case 'K':
                // newCard.AddComponent<SkillK>();
                break;

            case 'L':
                // newCard.AddComponent<SkillL>();
                break;

            case 'M':
                // newCard.AddComponent<SkillM>();
                break;

            case 'N':
                // newCard.AddComponent<SkillN>();
                break;

            case 'O':
                // newCard.AddComponent<SkillO>();
                break;

            case 'P':
                // newCard.AddComponent<SkillP>();
                break;

            case 'Q':
                // newCard.AddComponent<SkillQ>();
                break;

            case 'R':
                // newCard.AddComponent<SkillR>();
                break;

            case 'S':
                // newCard.AddComponent<SkillS>();
                break;

            case 'T':
                // newCard.AddComponent<SkillT>();
                break;

            case 'U':
                // newCard.AddComponent<SkillU>();
                break;

            case 'V':
                // newCard.AddComponent<SkillV>();
                break;

            case 'W':
                // newCard.AddComponent<SkillW>();
                break;

            case 'X':
                // newCard.AddComponent<SkillX>();
                break;

            case 'Y':
                // newCard.AddComponent<SkillY>();
                break;

            case 'Z':
                // newCard.AddComponent<SkillZ>();
                break;

            #endregion 
                */
            default:
                newCard.AddComponent<SkillBase>();
                break;
        }
    }
}
