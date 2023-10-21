using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{

    public static Enemy enemy;
    public static Player player;

    public TextMeshProUGUI textPlayerMoney;
    public TextMeshProUGUI textPlayerAssets;
    public TextMeshProUGUI textPlayerStress;

    public TextMeshPro enemyName;
    public TextMeshPro textEnemyMoney;
    public TextMeshPro textEnemyStress;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(updateText());
    }

    public IEnumerator updateText()
    {
        while (true)
        {
            textPlayerAssets.text = $"Asset value: {Player.assetValue}$";
            textPlayerStress.text = $"Stress: {Player.stress}";
            textPlayerMoney.text = $"Money: {Player.money}$";

            textEnemyStress.text = $"Stress: {Enemy.stress}";
            textEnemyMoney.text = $"Money: {Enemy.money}$";
            enemyName.text = Enemy.name.ToUpper();

            yield return new WaitForSeconds(.2f);
        }
    }


    public static int IntMultiply(float n)
    {
        return Mathf.RoundToInt(n);
    }
}
