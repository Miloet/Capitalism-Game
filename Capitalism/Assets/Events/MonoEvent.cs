using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Febucci.UI;


public class MonoEvent : MonoBehaviour
{
    public enum Evnt
    {
        Intro,
        Lawyer,
        Karen,
        PartyInvite,
        TheBill,
        WalkHome
    }

    public static Evnt[] randomEvents = {Evnt.Karen, Evnt.Lawyer, Evnt.PartyInvite};

    public static bool EventDone = true;

    public new string name;

    public string[] monolog;
    public string[] responses;


    static TextMeshProUGUI eventName;
    static Image image;
    static TextAnimator monologAnimator;
    static TextAnimatorPlayer monologPlayer;
    public static TextMeshProUGUI text;
    static Button[] responsButtons;
    static TextMeshProUGUI[] responsButtonTexts;

    public virtual void Start()
    {
        if (text == null) FindUI();

        StartCoroutine(ShowEvent());
    }

    private void Update()
    {
        if (Input.anyKeyDown) monologPlayer.SetTypewriterSpeed(2);
    }

    public void FindUI()
    {
        string s = "UI/Event/";

        eventName = GameObject.Find(s + "Name").GetComponent<TextMeshProUGUI>();
        image = GameObject.Find(s + "Image").GetComponent<Image>();

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

    }

    public IEnumerator ShowEvent()
    {
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
            if (s != monolog[monolog.Length - 1]) yield return new WaitUntil(() => monologAnimator.allLettersShown && Input.anyKeyDown);
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

        Vector3 newPos = trans.position + new Vector3(1000,0);
        Vector3 target = trans.position;

        float time = 0;

        while(time < 1)
        {
            time += Time.deltaTime;

            var c = buttonImage.color;
            c.a = time;
            buttonImage.color = c;

            trans.position = Vector3.Lerp(newPos, target, Easing01(time));

            yield return null;
        }
    }
    private IEnumerator ReverseButtonAnimation(int id)
    {
        Image buttonImage = responsButtons[id].GetComponent<Image>();
        RectTransform trans = responsButtons[id].GetComponent<RectTransform>();

        Vector3 newPos = trans.position + new Vector3(1000, 0);
        Vector3 target = trans.position;

        float time = 1;

        yield return new WaitForSeconds(0.25f);

        while (time > 0)
        {
            time -= Time.deltaTime;

            var c = buttonImage.color;
            c.a = time;
            buttonImage.color = c;

            trans.position = Vector3.Lerp(newPos, target, Easing01(time));

            yield return null;
        }
        trans.position = target;
    }

    public static float Easing01(float t)
    {
        t = Mathf.Clamp01(t);
        return Mathf.Pow(Mathf.Sin(t * Mathf.PI * 0.5f), 2);
    }
    public virtual void Respond(int n)
    {
        EventDone = true;

        for (int i = 0; i < Mathf.Min(responses.Length, responsButtons.Length); i++)
        {
            responsButtons[i].onClick.RemoveAllListeners();
            StartCoroutine(ReverseButtonAnimation(i));
        }

        Destroy(this, 1.2f);
    }

    public static Evnt GetEvent()
    {
        return randomEvents[Random.Range(0, randomEvents.Length-1)];
    }
    public static void NewEvent(Evnt evnt)
    {
        if (EventDone)
        {
            EventDone = false;
            GameObject g = Event.eventObject;

            switch (evnt)
            {
                default:
                    Debug.LogWarning("Event outside defined events OR the NewEvent() does not integrate this event: " + evnt.ToString());
                    break;

                case Evnt.Lawyer:
                    g.AddComponent<Evnt_Lawyer>();
                    break;
            }
        }
    }
}
