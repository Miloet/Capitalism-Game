using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{

    public static DateTime date;

    // Start is called before the first frame update
    void Start()
    {
        date = DateTime.Parse("9/11-2001");
    }

    public static void NextMonth()
    {
        date = date.AddMonths(1);
        while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) date = date.AddDays(1);
        DateUI.UpdateDate();

        Player.money += Player.income;
    }

    // Update is called once per frame
    void Update()
    {
        NextMonth();
    }
}
