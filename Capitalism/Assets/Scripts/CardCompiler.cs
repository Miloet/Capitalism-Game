using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class CardCompiler : MonoBehaviour
{
    public static Vector3 bounds = new Vector3(4.5f, 2f, 4.5f);
    private static TextMeshProUGUI abilityText;
    private static TextMeshProUGUI effectText;
    private static ScrollRect scroll;
    public static float multiplier = 1f;

    public static TextMeshPro enemyAttack;
    public static SpriteRenderer enemy;
    

    private void Awake()
    {
        abilityText = GameObject.Find("CompilerText").GetComponent<TextMeshProUGUI>();
        effectText = GameObject.Find("EffectCompileText").GetComponent<TextMeshProUGUI>();
        scroll = GameObject.Find("CardEffects").GetComponent<ScrollRect>();

        enemyAttack = GameObject.Find("EnemyAttack").GetComponent<TextMeshPro>();
        enemy = GameObject.Find("Boss").GetComponent<SpriteRenderer>();

        enemy.sprite = Enemy.sprite;

        enemyAttack.text = Enemy.DisplayAttack();

        UpdateText();
    }

    public void Compile()
    {
        StartCoroutine(CompileCoroutine());
    }

    public IEnumerator CompileCoroutine()
    {
        SkillBase[] cards = FindObjectsOfType<SkillBase>();
        List<SkillBase> cardsInPlay = new List<SkillBase>();
        
        foreach(SkillBase card in cards)
        {
            if(inBounds(card.transform)) cardsInPlay.Add(card);
        }

        cards = SortArray(cardsInPlay.ToArray());

        bool valid = true;
        string reason = ""; 

        foreach(SkillBase c in cards)
        {
            if(!c.Validate()) 
            {
                reason += c.ValidateReason();
                valid = false;
            }
        }

        multiplier = 1f;

        if (!valid)
        {
            print(reason);
        }
        else
        {
            int stress = Player.stress;
            CameraController.UpdateCamera(CameraController.State.Default);


            foreach (SkillBase c in cards)
            {
                if(!c.burnt) c.Effect(multiplier);
                else if (stress < 0) c.Effect(multiplier * 2f);
                yield return new WaitForSeconds((float)c.clip.length + 0.05f);
            }

            if (!(Enemy.money > 0))
            {
                SceneManager.LoadScene("Events");
            }
            else
            {
                yield return new WaitForSeconds(0.25f);

                Game.enemy.Attack();
            }
            yield return new WaitForSeconds(0.5f);
            UpdateText();

            if (Player.money > 0 && Enemy.money > 0)
            {
                enemyAttack.text = Enemy.DisplayAttack();

                Player.self.StartCoroutine(Player.self.StartRound());
            }
            else
            {
                if(!(Player.money > 0))
                {
                    StartCombat.nextEvent = Evnt.Death;
                    SceneManager.LoadScene("Events");
                }
            }
        }


    }

    public static bool inBounds(Transform t)
    {
        if (t.position.x > -bounds.x && t.position.x < bounds.x
            && t.position.y > -bounds.y && t.position.y < bounds.y
            && t.position.z > -bounds.z && t.position.z < bounds.z) return true;
        return false;
    }
    public static SkillBase[] SortArray(SkillBase[] array)
    {
        return array.OrderBy(go => go.transform.position.x).ToArray();
    }

    public static void UpdateText()
    {
        SkillBase[] cards = FindObjectsOfType<SkillBase>();
        List<SkillBase> cardsInPlay = new List<SkillBase>();

        foreach (SkillBase card in cards)
        {
            if (inBounds(card.transform)) cardsInPlay.Add(card);
        }

        cards = SortArray(cardsInPlay.ToArray());


        string text = "";

        foreach (SkillBase card in cards)
        {
            text += card.letter;
            if (card.requireAsset)
            {
                string asset = "";
                if (card.currentAsset != null) if(card.currentAsset.self != null) asset = card.currentAsset.self.stockSymbol;
                else asset = "!";
                text += $"({asset})";
            }
        }
        abilityText.text = text;

        string effects="";
        foreach(SkillBase card in cards)
        {
            effects += card.writeEffect() + "\n";
        }
        effectText.text = effects;

        float preferredHeight = effectText.GetPreferredValues().y;
        RectTransform contentRect = scroll.content;
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, preferredHeight - 200f);


    }
}
