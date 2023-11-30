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

    }

    public new string name;

    public string[] monolog;
    public string[] responses;


    static TextMeshProUGUI eventName;
    static Image image;
    static TextAnimator monologAnimator;
    static TextMeshProUGUI text;
    static Button[] responsButtons;
    static TextMeshProUGUI[] responsButtonTexts;

    bool input;

    public void Start()
    {
        if (text == null) FindUI();

        StartCoroutine(ShowEvent());
    }

    public void FindUI()
    {
        string s = "UI/Event/";

        eventName = GameObject.Find(s + "Name").GetComponent<TextMeshProUGUI>();
        image = GameObject.Find(s + "Image").GetComponent<Image>();

        GameObject g = GameObject.Find(s + "Dialog");
        monologAnimator = g.GetComponent<TextAnimator>();
        text = g.GetComponent<TextMeshProUGUI>();

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
            text.text = s;
            if (s != monolog[monolog.Length - 1]) yield return new WaitUntil(() => monologAnimator.allLettersShown && Input.anyKeyDown);
            else yield return null; yield return new WaitUntil(() => monologAnimator.allLettersShown);
        }

        for (int i = 0; i < Mathf.Min(responses.Length, responsButtons.Length); i++)
        {
            int index = i;
            responsButtons[i].onClick.AddListener(() => Respond(index));
            responsButtonTexts[i].text = responses[i];
        }
    }

    public virtual void Respond(int n)
    {
        switch (n)
        {
            case 0:
                Player.stress++;
                break;
            case 1:
                Player.money *= 1.2f;
                break;
        }
    }

    public static void NewEvent(Evnt evnt)
    {
        switch(evnt)
        {
            default:
                Debug.LogWarning("Event outside defined events OR the NewEvent() does not integrate this event: "  + evnt.ToString() );
                break;

            //case Evnt.:
                
                break;
        }
    }
}
