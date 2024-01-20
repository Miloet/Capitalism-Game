using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillToUI : MonoBehaviour
{

    public static GameObject card;
    public static bool Done;

    private void Awake()
    {
        RectTransform sourceRect = GetComponent<RectTransform>();

        sourceRect.sizeDelta = new Vector2(sourceRect.rect.width, ((Player.skills.Length-1) / 6 + 1) * 280f);
        Vector2 upperBound = new Vector2(sourceRect.anchoredPosition.x,
            sourceRect.anchoredPosition.y);

        card = Resources.Load<GameObject>("DisplayCard");

        for (int i = 0; i < Player.skills.Length; i++)
        {
            var g = Instantiate(card, transform);
            SetText(Player.skills[i],
            g.transform.Find("Name").GetComponent<TextMeshProUGUI>(),
            g.transform.Find("Body").GetComponent<TextMeshProUGUI>(),
            g.transform.Find("Picture").GetComponent<Image>(),
            g.transform.Find("AssetInput").gameObject);

            RectTransform rtrans = g.GetComponent<RectTransform>();
            rtrans.anchoredPosition = GetPosition(i, upperBound, rtrans) + new Vector2(rtrans.rect.width / 2f, -rtrans.rect.height / 2f - 10f);
        }
    }
    public static Vector2 GetPosition(int n, Vector2 upperBound, RectTransform trans)
    {
        return new Vector2(upperBound.x + Mathf.Repeat(n, 6) * (trans.rect.width + 30),  0-(n / 6 * (trans.rect.height + 10))); //- n * trans.rect.height + 190f);
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
                description = $"Deal {SkillBase.FinancialDamage("(+35%)", true)} to the enemy.";
                spriteResourcePath = "Skills/Accumulate";
                break;

            case 'B':
                requireAsset = true;
                name = "Bribe";
                description = $"Increase your next cards effect by {SkillBase.GainMoney(SkillBase.MultiplierAssetValue("(+1%)"))}%";
                spriteResourcePath = "Skills/Bribe";
                break;

            case 'C':
                requireAsset = true;
                name = "Counterfeit";
                description = $"Print {SkillBase.GainMoney($"{SkillBase.MultiplierAssetValue("(+10%)")} Money")} and gain {SkillBase.GainStress("+1")}";
                spriteResourcePath = "Skills/Counterfeit";
                break;
        case 'D':
                requireAsset = false;
                name = "Distract and Defund";
                description = "The enemy permanantly deals 100$ less finacial damage per attack.";
                spriteResourcePath = "Skills/Distract";
                break;
        case 'E':
                requireAsset = true;
                name = "Embezzle";
                description = $"Embezzle {SkillBase.FinancialDamage("(+10%)", true)} of the enemies money and gain it as your own causing them {SkillBase.GainStress("+1")} for every 1000$ of financial damage up to {SkillBase.GainStress("+3")}. " +
                    $"This ability becomes more effective the more stress you have ranging from {SkillBase.GainMoney("0% - 200%")} based on{SkillBase.GainStress("")}";
                break;

            #region Unused Letters
            /*
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
            */
                #endregion

                case 'K':
                requireAsset = false;
                name = "Kustomer";
                description = $"Deal {SkillBase.FinancialDamage("300", true)} and cause the enemy {SkillBase.GainStress("+1")}";
                spriteResourcePath = "Skills/Kustomer";
                break;

                case 'L':
                requireAsset = false;
                name = "Lawsuit";
                description = $"Gain {SkillBase.GainStress("+3")}. If you have more money than the enemy then gain all of their money. Other wise the reverse happends.";
                spriteResourcePath = "Skills/Lawsuit";
                break;

            #region Unused Letters

            /*
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
