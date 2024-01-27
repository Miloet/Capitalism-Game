using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Febucci.UI;
using UnityEngine.SceneManagement;

public class AliciaEnding : MonoBehaviour
{
    int currentText;

    public AudioClip hiddenInTheSand;
    public AudioClip justTheTwoOfUs;
    new AudioSource audio;

    public Button runWithHer;
    public Button fightControl;

    public Image hide;

    string[] dialog = { "> Hey Dawn... i have been thinking- <wave><i>sigh</wave></i> how do you feel about all of this?",
        "> what do you mean...?",
        "> about our work. its clear to me its been wearing you down a lot. what you have been doing to yourself is'nt healthy.",
        "> its not like we have much of a choice.",
        "> Dawn, please, run away with me. ",
        "> i- ...i want to, i really do. if i could i would run away with you alicia. but i don't know if we can do it. " +
            "you know just as well as i that the system- hell, the world, is rotten to the core.",
        "> you want to change that don't you? take down the demons at the top. HR, Technology, Security, Finances and... <shake><color=red>her.</color><shake>",
        "> it's like you can read my mind hehe...",
        "> I am with you. whether you choose to run away with me or stay and fight, i will support and be there for you. I love you dawn.",
        "> I love you too."
    };

    public TextMeshProUGUI text;
    public TextAnimator textAnimator;

    public static bool Ending;
    private void Start()
    {
        Ending = true;

        hide.color = new Color(0, 0, 0, 0);
        text.text = dialog[currentText];
        currentText++;

        audio = GetComponent<AudioSource>();
        audio.clip = hiddenInTheSand;
        audio.PlayDelayed(3);

        MonoEvent.RemoveRandomEvent(Evnt.AwayTogether);
    }

    bool done = false;

    private void Update()
    {
        if (done) return;

        if (textAnimator.allLettersShown && currentText < dialog.Length && Input.anyKeyDown)
        {
            text.text = dialog[currentText];
            currentText++;
        }
        if(currentText >= dialog.Length)
        {
            runWithHer.gameObject.SetActive(true);
            fightControl.gameObject.SetActive(true);
            StartCoroutine(ReverseHideButton());
            done = true;
        }
    }


    public void Run()
    {
        fightControl.gameObject.SetActive(false);
        runWithHer.enabled = false;
        StartCoroutine(HideButton());
        StartCoroutine(RunCoroutine());
    }
    public IEnumerator RunCoroutine()
    {
        text.text = "> thank you. i won't waste the faith you have in me.";

        yield return new WaitUntil(() => textAnimator.allLettersShown);

        float t = 1;
        while(t > 0)
        {
            t -= Time.deltaTime;
            audio.volume = t;
            yield return null;
        }
        audio.Stop();
        yield return new WaitForSeconds(3);
        while (t < 1)
        {
            t += Time.deltaTime;
            audio.volume = t;
            yield return null;
        }
        audio.PlayOneShot(justTheTwoOfUs);
        text.text = "The End";
        text.alignment = TextAlignmentOptions.Center;
        yield return new WaitForSeconds(5);

        Application.Quit();
    }

    public void Fight()
    {
        runWithHer.gameObject.SetActive(false);
        fightControl.enabled = false;
        StartCoroutine(HideButton());
        StartCoroutine(FightCoroutine());
    }
    public IEnumerator FightCoroutine()
    {
        text.text = "<i>The smile you fell in love with grows on her face.</i>\n> Let's do this then.";
        yield return null;
        yield return new WaitUntil(() => textAnimator.allLettersShown);
        yield return new WaitForSeconds(2);
        yield return new WaitUntil(() => Input.anyKeyDown);

        text.text = "<i>At this moment, your union of love and determination spawned forth a fourth <b>Trigon</b>. Now the <b>Tetragons</b>. The <b>Tetragons of love</b>, the <b>Tetragons of humanity</b>. " +
            "With its birth it gives it's mothers F - Finality, the card to mark the end of an age.";
        Player.AddCard('F');

        yield return null;
        yield return new WaitUntil(() => textAnimator.allLettersShown);
        yield return new WaitForSeconds(2);
        yield return new WaitUntil(() => Input.anyKeyDown);

        StartCombat.nextEvent = Evnt.Intro;
        MonoEvent.EventDone = true;
        SceneManager.LoadScene("Events");
    }


    public static void NextMonth(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= NextMonth;
        Event.NextMonth();
    }

    public IEnumerator HideButton()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            hide.color = new Color(0,0,0,t);
            yield return null;
        }
    }
    public IEnumerator ReverseHideButton()
    {
        float t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime;
            hide.color = new Color(0, 0, 0, t);
            yield return null;
        }
    }
}
