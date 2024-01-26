using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Febucci.UI;
using System.Linq;

public enum Evnt
{
    Intro,
    Lawyer,
    Karen,
    TaxMan,

    PartyInvite,
    Drinking,
    TheBill,
    WalkHome,

    Spa,
    OvertimeMandate,
    NewWorkEquipment,
    LawyerWantingToSue,


    Party,
    Boss,

    Death,
    Victory,

    AwayTogether,
    End,
}

public class MonoEvent : MonoBehaviour
{
    public static List<Evnt> randomEvents = new List<Evnt> { Evnt.Karen, Evnt.Lawyer, Evnt.OvertimeMandate, Evnt.NewWorkEquipment};
    #region var

    public static bool EventDone = true;

    public int speedUp = 4;

    public new string name;

    public Sprite eventImage;

    public string[] monolog;
    public string[] responses;

    static GameObject fullUI;
    static Vector3 uiPosition;

    static TextMeshProUGUI eventName;
    static Image image;
    public static TextAnimator monologAnimator;
    static TextAnimatorPlayer monologPlayer;
    public static TextMeshProUGUI text;
    public static Button[] responsButtons;
    static TextMeshProUGUI[] responsButtonTexts;
    static GameObject earningsRapport;

    static int buttonInView = 550;
    static int buttonOutofView = buttonInView + 1000;


    public static int AliciaFriendShip;

    #endregion 

    public static void NewEvent(Evnt evnt)
    {
        if (EventDone)
        {
            EventDone = false;
            GameObject g = FindObjectOfType<Event>().gameObject;

            switch (evnt)
            {
                default:
                    Debug.LogWarning("Event outside defined events OR the NewEvent() does not integrate this event: " + evnt.ToString());
                    EventDone = true;
                    break;
                case Evnt.Intro:
                    g.AddComponent<Evnt_Intro>();
                    break;
                case Evnt.Lawyer:
                    g.AddComponent<Evnt_Lawyer>();
                    break;
                case Evnt.PartyInvite:
                    g.AddComponent<Evnt_PartyInvite>();
                    break;
                case Evnt.Drinking:
                    g.AddComponent<Evnt_Drinking>();
                    break;
                case Evnt.TheBill:
                    g.AddComponent<Evnt_Bill>();
                    break;
                case Evnt.Karen:
                    g.AddComponent<Evnt_Karen>();
                    break;
                case Evnt.Victory:
                    g.AddComponent<Evnt_Victory>();
                    break;
                case Evnt.Death:
                    g.AddComponent<Evnt_Death>();
                    break;
                case Evnt.TaxMan:
                    g.AddComponent<Evnt_Taxman>();
                    break;
                case Evnt.Party:
                    g.AddComponent<Evnt_Party>();
                    break;
                case Evnt.AwayTogether:
                    SceneManager.LoadScene("AwayAtLast");
                    break;
                case Evnt.End:
                    g.AddComponent<Evnt_End>();
                    break;
                case Evnt.LawyerWantingToSue:
                    g.AddComponent<Evnt_LawyerWantingToSue>();
                    break;
                case Evnt.Spa:
                    g.AddComponent<Evnt_Spa>();
                    break;
                case Evnt.OvertimeMandate:
                    g.AddComponent<Evnt_OvertimeMandate>();
                    break;
                case Evnt.NewWorkEquipment:
                    g.AddComponent<Evnt_NewWorkEquipment>();
                    break;
                case Evnt.Boss:
                    g.AddComponent<Evnt_Boss>();
                    break;

            }
        }
    }

    

    public static void AddRandomEvent(Evnt evnt)
    {
        if(!randomEvents.Contains(evnt)) randomEvents.Add(evnt);
    }
    public static void RemoveRandomEvent(Evnt evnt)
    {
        if (randomEvents.Contains(evnt)) randomEvents.Remove(evnt);
    }
    public static Evnt GetRandomEvent()
    {
        int i = Random.Range(0, randomEvents.Count);
        return randomEvents[i];
    }

    #region Animation & UI

    public void FindUI()
    {
        string s = "UI/UI/Event/";

        fullUI = GameObject.Find("UI/UI");
        uiPosition = fullUI.transform.position;

        eventName = GameObject.Find(s + "Name").GetComponent<TextMeshProUGUI>();
        image = GameObject.Find(s + "EventImage").GetComponent<Image>();
        

        GameObject g = GameObject.Find(s + "Dialog");
        monologAnimator = g.GetComponent<TextAnimator>();
        monologPlayer = g.GetComponent<TextAnimatorPlayer>();
        text = g.GetComponent<TextMeshProUGUI>();

        responsButtons = new Button[4];
        responsButtonTexts = new TextMeshProUGUI[4];

        for (int i = 0; i < 4; i++)
        {
            GameObject b = GameObject.Find(s + "Button" + i);
            responsButtons[i] = b.GetComponent<Button>();
            responsButtonTexts[i] = b.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        }

        earningsRapport = GameObject.Find("UI/UI/IncomeReport");
    }
    public IEnumerator ShowEvent()
    {
        image.sprite = eventImage;

        foreach (Button b in responsButtons)
        {
            b.onClick.RemoveAllListeners();
            b.gameObject.SetActive(false);
        }

        eventName.text = name;
        yield return new WaitForSeconds(0.5f);

        foreach (string s in monolog)
        {
            yield return new WaitForSeconds(0.25f);
            text.text = s;
            if (s != monolog[^1]) yield return new WaitUntil(() => monologAnimator.allLettersShown && Input.anyKeyDown);
            else yield return new WaitUntil(() => monologAnimator.allLettersShown);

            yield return new WaitForSeconds(0.5f);
        }

        for (int i = 0; i < Mathf.Min(responses.Length, responsButtons.Length); i++)
        {
            int index = i;
            responsButtons[i].gameObject.SetActive(true);
            responsButtons[i].onClick.AddListener(() => Respond(index));
            responsButtonTexts[i].text = responses[i];

            StartCoroutine(ButtonAnimation(i));
            yield return new WaitForSeconds(0.25f);
        }
    }
    private IEnumerator ButtonAnimation(int id)
    {
        Image buttonImage = responsButtons[id].GetComponent<Image>();
        RectTransform trans = responsButtons[id].GetComponent<RectTransform>();

        Vector3 newPos = new Vector3(buttonOutofView, trans.localPosition.y);
        Vector3 target = new Vector3(buttonInView, trans.localPosition.y);

        float time = 0;

        while(time < 1)
        {
            time += Time.deltaTime;

            var c = buttonImage.color;
            c.a = time;
            buttonImage.color = c;
            trans.localPosition = Vector3.Lerp(newPos, target, Easing01(time));

            yield return null;
        }
    }
    public IEnumerator ReverseButtonAnimation(int id)
    {
        Image buttonImage = responsButtons[id].GetComponent<Image>();
        RectTransform trans = responsButtons[id].GetComponent<RectTransform>();

        Vector3 newPos = new Vector3(buttonOutofView, trans.localPosition.y);
        Vector3 target = new Vector3(buttonInView, trans.localPosition.y);

        float time = 1;

        yield return new WaitForSeconds(0.25f);

        while (time > 0)
        {
            time -= Time.deltaTime;

            var c = buttonImage.color;
            c.a = time;
            buttonImage.color = c;

            trans.localPosition = Vector3.Lerp(newPos, target, Easing01(time));

            yield return null;
        }
        trans.localPosition = target;
    }
    public static float Easing01(float t)
    {
        t = Mathf.Clamp01(t);
        return Mathf.Pow(Mathf.Sin(t * Mathf.PI * 0.5f), 2);
    }
    public IEnumerator EaringsRapport()
    {
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => monologAnimator.allLettersShown && Input.anyKeyDown);
        Destroy(this,5f);

        earningsRapport.SetActive(true);

        var ir = FindObjectOfType<IncomeReport>();
        ir.UpdateIncome();
        IncomeReport.IncomeReady = true;

        Vector3 p_newPos = uiPosition;
        Vector3 p_target = uiPosition + new Vector3(-2000, 0);

        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime;
            fullUI.transform.position = Vector3.Lerp(p_newPos, p_target, Easing01(time));

            yield return null;
        }
        ir.sign.enabled = true;
    }
    public IEnumerator ReverseRapportAnimation()
    {
        

        Vector3 p_newPos = uiPosition;
        Vector3 p_target = uiPosition + new Vector3(-2000, 0);

        float time = 1;

        while (time > 0)
        {
            time -= Time.deltaTime;
            fullUI.transform.position = Vector3.Lerp(p_newPos, p_target, Easing01(time));

            yield return null;
        }
    }
    public static Sprite GetImage(string name)
    {
        return Resources.Load<Sprite>("Event/" + name);
    }

    //Start is virtual for custom text and image
    public virtual void Start()
    {
        if (text == null) FindUI();

        StartCoroutine(ReverseRapportAnimation());

        StartCoroutine(ShowEvent());
    }
    private void Update()
    {
        if (Input.anyKeyDown) monologPlayer.SetTypewriterSpeed(speedUp);
    }

    #endregion

    public virtual void Respond(int n)
    {
        EventDone = true;

        for (int i = 0; i < Mathf.Min(responses.Length, responsButtons.Length); i++)
        {
            responsButtons[i].onClick.RemoveAllListeners();
            StartCoroutine(ReverseButtonAnimation(i));
        }

        if (AliciaFriendShip >= 1) AddRandomEvent(Evnt.Party);
        if (AliciaFriendShip >= 3) AddRandomEvent(Evnt.Spa);
        if (AliciaFriendShip >= 7) AddRandomEvent(Evnt.AwayTogether);

        StartCoroutine(EaringsRapport());
    }
    public void AltResponse(Evnt nextEvent)
    {
        EventDone = true;

        for (int i = 0; i < Mathf.Min(responses.Length, responsButtons.Length); i++)
        {
            responsButtons[i].onClick.RemoveAllListeners();
            StartCoroutine(ReverseButtonAnimation(i));
        }
        StartCoroutine(WaitForDialogFinish(nextEvent));
    }
    public IEnumerator WaitForDialogFinish(Evnt nextEvent)
    {
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => monologAnimator.allLettersShown && Input.anyKeyDown);
        Destroy(this, 5f);
        NewEvent(nextEvent);
    }

    public virtual IEnumerator Combat()
    {
        yield return new WaitUntil(() => monologAnimator.allLettersShown && Input.anyKeyDown);
    }
    public virtual void RespondNextEvent(Evnt evnt)
    {
        EventDone = true;

        for (int i = 0; i < Mathf.Min(responses.Length, responsButtons.Length); i++)
        {
            responsButtons[i].onClick.RemoveAllListeners();
            StartCoroutine(ReverseButtonAnimation(i));
        }

        NewEvent(evnt);

        Destroy(this, 1f);
    }
    
    
}
