using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTaken : MonoBehaviour
{
    float time = 1.5f;
    public TextMeshPro text;
    public static Transform basePosition;

    public Rigidbody rb;

    static float EaseInOut(float t)
    {
        // Ensure t is in the range [0, 1]
        t = Mathf.Max(0, t);

        // Apply the smoothstep formula
        return t * t * (3 - 2 * t);
    }

    void Start()
    {
        if (basePosition == null) basePosition = GameObject.Find("Base").transform;

        transform.position = basePosition.position + new Vector3(Random.Range(-2,2), Random.Range(-2, 2));

        text = GetComponent<TextMeshPro>();

        GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2, 2), Random.Range(.5f, 2), Random.Range(-.5f, .5f));
    }

    // Update is called once per frame
    void Update()
    {
        text.fontSize = 2;
        if (text.text.Length > 10) text.fontSize = 3;
        if (text.text.Length > 12) text.fontSize = 4;

        Color c = text.color;
        c.a = time;
        text.color = c;

        transform.localScale = Vector3.one * EaseInOut(time);

        time -= Time.deltaTime;
        if (time < 0) Destroy(gameObject);

    }
}
