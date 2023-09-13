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

    public TextMeshProUGUI textEnemyMoney;
    public TextMeshProUGUI textEnemyStress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textPlayerAssets.text = $"Asset value: {Player.assetValue}";
        textPlayerStress.text = $"Stress: {Player.stress}";
        textPlayerMoney.text = $"Money: {Player.money}";

        textEnemyMoney.text = $"Stress: {Enemy.stress}";
        textEnemyStress.text = $"Money: {Enemy.money}";
}


    public static int IntMultiply(float n)
    {
        return Mathf.RoundToInt(n);
    }
}
