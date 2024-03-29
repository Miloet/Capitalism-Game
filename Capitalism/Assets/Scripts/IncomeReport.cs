using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IncomeReport : MonoBehaviour
{
    public static bool IncomeReady;

    public TextMeshProUGUI expensesSource;
    public TextMeshProUGUI expensesCost;
    public TextMeshProUGUI hours;
    public TextMeshProUGUI income;

    public TextMeshProUGUI subtotal;
    public TextMeshProUGUI total;
    public TextMeshProUGUI date;
    public Image signature;
    public Button sign;


    public Toggle overtime;

    Expense totalCost;

    private void Start()
    {
        gameObject.SetActive(false);
        IncomeReady = false;
        UpdateIncome();
    }

    private void Update()
    {
        sign.interactable = IncomeReady;
    }

    public void UpdateIncome()
    {
        float Income = Player.income;

        income.text = (Income / 200f).ToString("N2") + "$ / h";
        if (overtime.isOn) hours.text = "385 Hours";
        else hours.text = "195 Hours";

        subtotal.text = (Income * (overtime.isOn ? 2f : 1f)).ToString("N2") + "$";

        expensesCost.text = GetExpensesWriten();
        totalCost = GetExpenses();
        expensesSource.text = totalCost.Source;
        date.text = $"{Event.date.Day} / {Event.date.Month}\n{Event.date.Year}";
        total.text = (Income * (overtime.isOn ? 2f : 1f) - totalCost.Cost).ToString("N2") + "$";

        RectTransform rt = expensesSource.GetComponent<RectTransform>();
        float preferredHeight = expensesSource.preferredHeight;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, preferredHeight);

        signature.fillAmount = 0;
    }
    public void ButtonSignReport()
    {
        StartCoroutine(SignReport());
    }
    public IEnumerator SignReport()
    {
        sign.enabled = false;
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime*2f;
            signature.fillAmount = time;
            yield return null;
        }
        yield return new WaitUntil(() => Input.anyKeyDown);

        Player.money += Player.income * (overtime.isOn ? 2f : 1f) - totalCost.Cost;

        if (overtime.isOn) Player.stress++;
        foreach(Expense ex in Player.expenses)
        {
            if (ex.newTempCost != 0) ex.newTempCost = 0;
        }
        if(Player.tempExpenses.Count > 0) Player.tempExpenses.Clear();

        Event.NextMonth();

        yield return new WaitForSeconds(1);
        sign.enabled = true;
        IncomeReady = false;
    }

    public Expense GetExpenses()
    {
        string sources = "Tax";
        float costs = Player.income * (overtime.isOn ? 2f : 1f) * GetTaxBracket();
        
        foreach(Expense ex in Player.expenses)
        {
            sources = sources + "\n" + ex.getSource();
            costs += ex.getCost();
        }
        foreach(Expense ex in Player.tempExpenses)
        {
            sources = sources + "\n" + ex.getSource();
            costs += ex.getCost();
        }

        return new Expense(sources, costs);
    }

    public string GetExpensesWriten()
    {
        string costs = $"{GetTaxBracket()*100f:N2}% ({Player.income * (overtime.isOn ? 2f : 1f) * GetTaxBracket():N2}$)";

        foreach (Expense ex in Player.expenses)
        {
            costs += $"\n{ex.getWritenCost():N2}$";
        }
        foreach (Expense ex in Player.tempExpenses)
        {
            costs += $"\n{ex.getWritenCost():N2}$";
        }
        return costs;
    }


    public float GetTaxBracket()
    {
        float i = Player.income * (overtime.isOn ? 2f : 1f);

        if (i > 1000000) return 0.0f;
        if (i > 100000) return 0.01f;
        if (i > 80000) return 0.05f;
        if (i > 10000) return 0.10f;
        if (i > 5000) return 0.20f;
        if (i > 1000) return 0.30f;
        if (i > 500) return 0.75f;
        else return 0;
    }
}
