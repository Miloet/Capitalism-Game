using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillToUI : MonoBehaviour
{

    public static GameObject card;
    private void Start()
    {
        RectTransform sourceRect = GetComponent<RectTransform>();
        
        sourceRect.sizeDelta = new Vector2(sourceRect.rect.width, (Player.skills.Length / 6 + 1) * 295f);
        Vector2 upperBound = new Vector2(sourceRect.anchoredPosition.x, 
            sourceRect.anchoredPosition.y);

        card = Resources.Load<GameObject>("DisplayCard");

        for(int i = 0; i < Player.skills.Length; i++)
        {
            var g = Instantiate(card, transform);
            SetText(Player.skills[i],
            g.transform.Find("Name").GetComponent<TextMeshProUGUI>(),
            g.transform.Find("Body").GetComponent<TextMeshProUGUI>(),
            g.transform.Find("Picture").GetComponent<Image>(),
            g.transform.Find("AssetInput").gameObject);

            RectTransform rtrans = g.GetComponent<RectTransform>();
            rtrans.anchoredPosition = GetPosition(i, upperBound, rtrans) + new Vector2(rtrans.rect.width / 2f, rtrans.rect.height / 2f);
        }
    }
    public static Vector2 GetPosition(int n, Vector2 upperBound, RectTransform trans)
    {
        return new Vector2(upperBound.x + Mathf.Repeat(n, 6) * (trans.rect.width + 30), upperBound.y - n / 6 * (trans.rect.height + 10)); //- n * trans.rect.height + 190f);
    }
    public static void SetText(char skill, TextMeshProUGUI Name, TextMeshProUGUI Description, Image image, GameObject takeAsset)
    {

        bool requireAsset = false;
        string name = "";
        string description = "";
        string spriteResourcePath = "";
        Sprite sprite = null;


        switch (skill)
        {
            case 'A':
                requireAsset = true;
                name = "Accumulate";
                description = "Use your Assets to damage the opponents cash. Deal 2.5 x Price dmg";
                spriteResourcePath = "Skills/Accumulate";
                break;

            case 'B':
                requireAsset = true;
                name = "Bribe";
                description = "Bribe lawmakers to make new laws in your favor. Multiply your next cards effect by 1 + Price / 100.";
                spriteResourcePath = "Skills/Bribe";
                break;

            case 'C':
                requireAsset = true;
                name = "Counterfeit";
                description = "Use an asset to print fake money equal to 10 times its value. Add 1 stress.";
                spriteResourcePath = "Skills/Counterfeit";
                break;

            #region Unused Letters
            /*
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

                Debug.LogError("Card not found for UI");

                break;

                #endregion
        }

        sprite = Resources.Load<Sprite>(spriteResourcePath);
        Name.text = skill + " - " + name;
        Description.text = description;
        image.sprite = sprite;
        takeAsset.SetActive(requireAsset);


    }
}
