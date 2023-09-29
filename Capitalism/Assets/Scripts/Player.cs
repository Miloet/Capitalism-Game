using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static float money = 1000;
    public static int stress = 0;

    public static char[] skills = {'A','B','A','B','B','B','C'};
    public static Stock[] assets;// = {new Stock("AAPL")};

    public static bool loadingStock;

    public static float assetValue = 0;

    GameObject hand;

    GameObject card;
    GameObject asset;


    public List<GameObject> currentHand = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        card = Resources.Load<GameObject>("Card");
        asset = Resources.Load<GameObject>("Asset");

        hand = GameObject.Find("HandPlane");

        string[] Symbols = { "AAPL", "MSFT", "GOOGL", "AMZN", "TSLA", "JPM", "WMT", "NVDA", "DIS" };
        List<Stock> stocks = new List<Stock>();
        Stock stock;
        foreach (string s in Symbols)
        {
            stock = new Stock();
            
            StartCoroutine(assignStock(stock, s));
            stocks.Add(stock);
        }

        assets = stocks.ToArray();

        StartRound();
    }

    private void Update()
    {
        
        int ammount = currentHand.Count;
        int offset = 0;
        if (MouseInput.selected != null) offset = 1;
        int a = 0;
        for (int i = 0; i < ammount; i++)
        {
            if (currentHand[i] != MouseInput.selected) currentHand[i].transform.position = hand.transform.position +
                    new Vector3(8f / (ammount - offset) * ((i-a) - (ammount-1f-offset) / 2f),
                    .05f + Mathf.Pow(Mathf.Sin(Time.time + i), 2));
            else a = 1;
        }
    }


    public void StartRound()
    {
        /*
        int draw = 5;
        if(skills.Length > 5 || assets.Length > 5)
        {
            draw = Random.Range(0, assets.Length);
        }*/

        currentHand.Clear();

        int ammount = skills.Length + assets.Length;
        float x = 8f / ammount;

        for (int i = 0; i < 3; i++)
        {
            var g = Instantiate(card);
            g.transform.forward = -hand.transform.up;
            g.transform.position = hand.transform.position + new Vector3(x * (i - ammount / 2), .05f);
            currentHand.Add(g);


            assignSkill(g, skills[Random.Range(0, skills.Length)]);
        }

        for (int i = 0; i < 2; i++)
        {
            var g = Instantiate(asset);
            g.transform.forward = -hand.transform.up;
            g.transform.position = hand.transform.position + new Vector3(x * (i + skills.Length - ammount / 2), .05f);
            currentHand.Add(g);


            g.GetComponent<Asset>().Assign(assets[Random.Range(0, assets.Length)]);
        }


        MouseInput.updateCardCount();
    }



    public void updateValue()
    {
        float value = 0;
        foreach(Stock a in assets)
        {
            value += a.getValue();
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
                newCard.AddComponent<SkillBase>();
                break;

                #endregion
        }
    }

    public IEnumerator assignStock(Stock s, string symbol)
    {
        loadingStock = false;
        s.CreateStock(symbol, "1w", "1d", Random.Range(1, 10));
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
}