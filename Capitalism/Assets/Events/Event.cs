using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class Event : MonoBehaviour
{
    public static string StartDate = "2/5-2011";
    public static DateTime date;
    public static int time;

    public static int month = 0;

    public static TextMeshProUGUI textBox;

    public static GameObject eventObject;

    void Start()
    {
        eventObject = gameObject;
        date = DateTime.Parse(StartDate);
    }

    public static void TimeBetween()
    {
        DateTime start = DateTime.Parse(StartDate);

        time = (date - start).Days - CountWeekendDays(start, date); 
    }
    static int CountWeekendDays(DateTime startDate, DateTime endDate)
    {
        int weekendDays = 0;

        // Iterate through each day in the date range
        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
        {
            // Check if the current day is a Saturday or Sunday
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                weekendDays++;
            }
        }

        return weekendDays;
    }
    public static void NextMonth()
    {
        date = date.AddMonths(1);
        while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) date = date.AddDays(1);
        month++;

        Player.money += Player.income;

        TimeBetween();
        DateUI.UpdateDate();
        StockBuy.UpdateAllText();
        
        //MonoEvent.NewEvent(MonoEvent.Evnt.Lawyer);
    }

}
