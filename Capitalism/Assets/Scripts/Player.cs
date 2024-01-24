using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player self;

    public static float money = 1000;
    public static float income = 1500;
    public static List<Expense> expenses = new List<Expense>() {new Expense("Food and Rent", 900)};
    public static List<Expense> tempExpenses = new List<Expense>();

    public static int stress = 0;

    public static char[] skills = { 'A', 'B', 'C','D', 'E', 'K', 'L'};
    public static Stock[] assets = new Stock[0];// = {new Stock("AAPL")};

    public static bool loadingStock;

    public static float assetValue = 0;

    static GameObject hand;

    static GameObject card;
    static GameObject asset;

    public GameObject cardOriginPoint;


    public List<GameObject> currentHand = new List<GameObject>();
    public List<CardBehavior> currentHandCB = new List<CardBehavior>();

    public static GameObject DamageIndicator;

    public static int CardsToDraw = 6;

    // Start is called before the first frame update
    void Start()
    {
        card = Resources.Load<GameObject>("Card");
        asset = Resources.Load<GameObject>("Asset");

        hand = GameObject.Find("HandPlane");
        cardOriginPoint = GameObject.Find("CardDeck");

        self = this;
        DamageIndicator = Resources.Load<GameObject>("Damage");

        //StartCoroutine(StartRound());
    }

    private void Awake()
    {
        card = Resources.Load<GameObject>("Card");
        asset = Resources.Load<GameObject>("Asset");

        hand = GameObject.Find("HandPlane");
        cardOriginPoint = GameObject.Find("CardDeck");

        self = this;
        DamageIndicator = Resources.Load<GameObject>("Damage");
    }

    public static void UpdateCardInHand()
    {
        UpdateCardBehavoir();
        int numberOfCards = self.currentHand.Count;
        float cardSpacing = 6f / numberOfCards;

        for(int i = 0; i < numberOfCards; i++)
        {
            if (MouseInput.selected != null || self.currentHand[i] != MouseInput.selected)
            {
                float xOffset = (numberOfCards - 1) * 0.5f * cardSpacing;
                float cardPositionX = i * cardSpacing - xOffset;
                self.currentHandCB[i].MoveTo(hand.transform.position + new Vector3(cardPositionX, .05f, 0f));
                self.currentHand[i].transform.forward = -hand.transform.up;
            }
        }
    }

    public static void UpdateCardBehavoir()
    {
        self.currentHandCB = new List<CardBehavior>();
        foreach(GameObject g in self.currentHand)
        {
            CardBehavior cb = g.GetComponent<CardBehavior>();
            if(cb.currentAsset == null) cb.closed = true;
            self.currentHandCB.Add(cb);
        }
    }


    public IEnumerator StartRound()
    {
        currentHand.Clear();

        CardBehavior[] list = FindObjectsOfType<CardBehavior>();

        foreach(CardBehavior cb in list)
        {
            Destroy(cb.gameObject);
        }

        yield return null;

        DrawCards(CardsToDraw);
    }

    public static void TakeDamage(float damage) 
    {
        var g = Instantiate(DamageIndicator);
        g.GetComponent<TextMeshPro>().text = $"-{damage:N2}$";

        money -= damage;
    }

    public void DrawCards(int n)
    {
        List<char> skillList = new List<char>();
        List<Stock> assetList = new List<Stock>();
        if(skills.Length != 0) skillList = skills.OfType<char>().ToList();
        if(assets.Length != 0) assetList = assets.OfType<Stock>().ToList();

        int Max = skillList.Count + assetList.Count;

        if (Max > 0)
        {
            for (int i = 0; i < Mathf.Clamp(n, 0, Max); i++)
            {
                int pick = Random.Range(0, skillList.Count + assetList.Count);
                GameObject g = null;
                if (pick < skillList.Count)
                {
                    g = Instantiate(card);
                    assignSkill(g, skillList[pick]);

                    skillList.RemoveAt(pick);
                }
                else
                {
                    pick -= skillList.Count;
                    g = null;
                    g = Instantiate(asset);
                    g.GetComponent<Asset>().Assign(assetList[pick]);

                    assetList.RemoveAt(pick);
                }

                currentHand.Add(g);
                g.transform.forward = -hand.transform.up;
                g.transform.position = cardOriginPoint.transform.position + new Vector3(0, 0, -1f);

            }
        }


        MouseInput.updateCardCount();
        UpdateCardInHand();
    }

    public static void AddUnique(GameObject g)
    {
        bool unique = true;
        foreach(GameObject obj in self.currentHand)
        {
            if (obj == g)
            {
                unique = false;
                break;
            }
        }
        if (unique) self.currentHand.Add(g);
        UpdateCardInHand();
    }

    public static void updateValue()
    {
        float value = 0;
        foreach(Stock a in assets)
        {
            value += a.getValue() * a.amount;
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
                
            case 'B':
                newCard.AddComponent<SkillBribe>();
                break;

            case 'C':
                newCard.AddComponent<SkillCounterfeit>();
                break;

            case 'D':
                newCard.AddComponent<SkillDistractAndDefund>();
                break;

            case 'E':
                newCard.AddComponent<SkillEmbezzle>();
                break;
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
            case 'K':
                newCard.AddComponent<SkillKustomer>();
                break;

            case 'L':
                newCard.AddComponent<SkillLawsuit>();
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
                newCard.AddComponent<SkillBase>();
                break;

                #endregion
        }
    }

    public static string getSkillName(char Skill)
    {
        string componentName = "";
        switch (Skill)
        {
            case 'A':
                componentName = nameof(SkillAccumulate);
                break;

            case 'B':
                componentName = nameof(SkillBribe);
                break;

            case 'C':
                componentName = nameof(SkillCounterfeit);
                break;

            case 'D':
                componentName = nameof(SkillDistractAndDefund);
                break;

            case 'E':
                componentName = nameof(SkillEmbezzle);
                break;
            #region Unused Letters
            /*
            case 'F':
                componentName = nameof(SkillF);
                break;

            case 'G':
                componentName = nameof(SkillG);
                break;

            case 'H':
                componentName = nameof(SkillH);
                break;

            case 'I':
                componentName = nameof(SkillI);
                break;

            case 'J':
                componentName = nameof(SkillJ);
                break;
            */
            #endregion

            case 'K':
                componentName = nameof(SkillKustomer);
                break;

            case 'L':
                componentName = nameof(SkillLawsuit);
                break;

            #region Unused Letters
            /*
            case 'M':
                componentName = nameof(SkillM);
                break;

            case 'N':
                componentName = nameof(SkillN);
                break;

            case 'O':
                componentName = nameof(SkillO);
                break;

            case 'P':
                componentName = nameof(SkillP);
                break;

            case 'Q':
                componentName = nameof(SkillQ);
                break;

            case 'R':
                componentName = nameof(SkillR);
                break;

            case 'S':
                componentName = nameof(SkillS);
                break;

            case 'T':
                componentName = nameof(SkillT);
                break;

            case 'U':
                componentName = nameof(SkillU);
                break;

            case 'V':
                componentName = nameof(SkillV);
                break;

            case 'W':
                componentName = nameof(SkillW);
                break;

            case 'X':
                componentName = nameof(SkillX);
                break;

            case 'Y':
                componentName = nameof(SkillY);
                break;

            case 'Z':
                componentName = nameof(SkillZ);
                break;
            */
            #endregion

            default:
                Debug.LogError("Cannot find the name of card");
                break;
        }

        componentName = componentName.Replace("Skill", "").Trim();
        componentName = Regex.Replace(componentName, "(\\B[A-Z])", " $1");

        return componentName;
    }


    public IEnumerator assignStock(Stock s, string symbol)
    {
        loadingStock = false;
        s.CreateStock(symbol, "1w", "1d", 0);
        yield return new WaitUntil(() => loadingStock);
        yield return new WaitForSeconds(1);
    }

    public IEnumerator assignAssets(Asset[] array)
    {
        foreach(Asset asset in array)
        {
            loadingStock = false;
            Stock s = new Stock();
            s.CreateStock("AAPL", "1w","1d",1);
            yield return new WaitUntil(() => loadingStock);
            yield return new WaitForSeconds(1);
            asset.Assign(s);
        }
    }

    public static void AddCard(char card)
    {
        var list = skills.ToList();
        list.Add(card);
        skills = list.ToArray();
    }
}


public class Expense
{
    public string Source;
    public float Cost;
    public float newTempCost = 0;

    public Expense(string source, float cost)
    {
        Source = source;
        Cost = cost;
    }

    public float getCost()
    {
        if (newTempCost == 0) return Cost;
        else return newTempCost;
    }
    public string getWritenCost()
    {
        string c = getCost().ToString("N2");

        if (getCost() > 0) return $"<color=red>{c}</color>";
        else return $"<color=green>{c}</color>";
    }

    public string getSource()
    {
        if (getCost() > 0) return $"<color=red>{Source}</color>";
        else return $"<color=green>{Source}</color>";
    }

    public static void ChangeExpense(string name, float tempCost)
    {
        foreach(Expense expense in Player.expenses)
        {
            if(name == expense.Source) expense.newTempCost = expense.Cost - tempCost;
        }
    }
}