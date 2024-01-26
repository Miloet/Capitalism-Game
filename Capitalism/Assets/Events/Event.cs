using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

        if (month == 0) MonoEvent.NewEvent(Evnt.Intro);
        print("Next event is " + StartCombat.nextEvent.ToString());
        if (StartCombat.nextEvent != Evnt.Intro && SceneManager.GetActiveScene().name == "Events") 
        {
            MonoEvent.NewEvent(StartCombat.nextEvent);
            StartCombat.nextEvent = Evnt.Intro;
        }
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
        if (MonoEvent.AliciaFriendShip >= 10) MonoEvent.AddRandomEvent(Evnt.AwayTogether);
        date = date.AddMonths(1);
        while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) date = date.AddDays(1);
        month++;

        TimeBetween();
        DateUI.UpdateDate();
        StockBuy.UpdateAllText();

        var e = GetNextEvent();

        MonoEvent.NewEvent(e);
    }


    public static Evnt GetNextEvent()
    {

        switch(month)
        {
            case 0:
                return Evnt.Intro;
            case 1:
                return Evnt.PartyInvite;
            case 3:
                return Evnt.TaxMan;
            case 11:
                if (MonoEvent.AliciaFriendShip >= 7 && !AliciaEnding.Ending) return Evnt.AwayTogether;
                break;
            case 12:
                return Evnt.Boss;
        }
        return MonoEvent.GetRandomEvent();
    }

}
