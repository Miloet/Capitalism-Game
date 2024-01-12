using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCombat : MonoBehaviour
{

    public static Evnt nextEvent;
    public static void StartCombatWithEnemy(string name, float money, int stress, string picture, Attack[] opening, Attack[] repeat, Evnt next = Evnt.Victory)
    {
        Enemy.name = name;
        Enemy.money = money;
        Enemy.stress = stress;
        Enemy.str = 0;
        Enemy.openingAttacks = opening;
        Enemy.repeatingAttacks = repeat;

        Enemy.sprite = Resources.Load<Sprite>("EnemyPortraits/" +picture);

        Enemy.startingMoney = money;

        nextEvent = next;


        SceneManager.sceneLoaded += DrawCards;
        SceneManager.LoadScene("Fight");
    }

    static void DrawCards(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= DrawCards;
        Game.player.DrawCards(Player.CardsToDraw);
    }

}
