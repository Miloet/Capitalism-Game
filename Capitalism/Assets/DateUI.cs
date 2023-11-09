using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DateUI : MonoBehaviour
{
    public float frequency = 1;
    public float applitude = 1;


    float t;

    static TextMeshProUGUI day;
    static TextMeshProUGUI month;
    static TextMeshProUGUI year;

    Vector3 dayPosition;
    Vector3 monthPosition;

    private void Start()
    {
        day = transform.Find("Day").GetComponent<TextMeshProUGUI>();
        month = transform.Find("Month").GetComponent<TextMeshProUGUI>();
        year = transform.Find("Year").GetComponent<TextMeshProUGUI>();

        dayPosition = day.transform.position;
        monthPosition = month.transform.position;
    }

    private void Update()
    {
        t = Time.time;
        day.rectTransform.position = dayPosition + new Vector3(0,Mathf.Sin(t*frequency)*applitude);
        month.rectTransform.position = monthPosition + new Vector3(0,Mathf.Sin(t * frequency + 2)*applitude);
    }
    public static void UpdateDate()
    {
        day.text = Event.date.Day.ToString();
        month.text = Event.date.Month.ToString();
        year.text = Event.date.Year.ToString();
    }
}
