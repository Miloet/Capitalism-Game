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
    public TextMeshProUGUI textPlayerIncome;
    public TextMeshProUGUI textPlayerAssets;
    public TextMeshProUGUI textPlayerStress;
    public RectTransform selector;

    public TextMeshPro enemyName;
    public TextMeshPro textEnemyMoney;
    public TextMeshPro textEnemyStress;

    // Update is called once per frame

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        if (enemy == null) gameObject.AddComponent<Enemy>();
        player = GetComponent<Player>();
        if (player == null) gameObject.AddComponent<Player>();
    }
    void Start()
    {
        StartCoroutine(updateText());
    }

    public IEnumerator updateText()
    {
        while (true)
        {
            textPlayerMoney.text = $"{Player.money.ToString("N2")}$";
            textPlayerIncome.text = $"{Player.income.ToString("N2")}$ / Month";
            textPlayerAssets.text = $"{Player.assetValue.ToString("N2")}$";
            textPlayerStress.text = $"Stress: {Player.stress}";

            float x = 120f / 10f * Player.stress;

            selector.localPosition = new Vector3(x, 0, 0);
            if (textEnemyStress != null)
            {
                textEnemyStress.text = $"Stress: {Enemy.stress}";
                textEnemyMoney.text = $"Money: {Enemy.money.ToString("N2")}$";
                enemyName.text = Enemy.name.ToUpper();
            }

            yield return new WaitForSeconds(.1f);
        }
    }

    public static int IntMultiply(float n)
    {
        return Mathf.RoundToInt(n);
    }
}
