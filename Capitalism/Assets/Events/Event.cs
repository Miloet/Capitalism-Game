using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class Event : MonoBehaviour
{
    public static string StartDate = "9/11-2001";
    public static DateTime date;
    public static int time;

    public static int month;

    public static TextMeshProUGUI textBox;

    void Start()
    {
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
        //date = date.AddDays(1);
        while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) date = date.AddDays(1);
        
        Player.money += Player.income;

        TimeBetween();
        DateUI.UpdateDate();
        StockBuy.UpdateAllText();

        DoEvent(new MonoEvent());
    }
    
    public static void DoEvent(MonoEvent evt)
    {
        currentEvent = evt.Name;

        foreach(string text in evt.preDialouge)
        {

            //wait until animator done and any button is pressed
        }


        //Show buttons
        for (int i = 0; i < evt.answers.Length; i++)
        {
            //menuButtons[i].button.onClick.AddListener(delegate { GetEffect(evt.name,i); });
        }
    }

    public void GetEffect(string name, int respons)
    {
        switch(name)
        {
            case "ElonMuskyTusky":

                if (respons == 0) Player.money += 1000000000000000;
                if (respons == 1) Player.money -= 99999;

                break;



        }
    }

    public class MonoEvent
    {
        public string Name;
        public string[] preDialouge;

        public string[] answers;

        public string DefaultReply = "";

    }

}
