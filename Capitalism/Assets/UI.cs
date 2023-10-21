using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject endTurn;
    public GameObject cardList;

    Vector3 endTurnOriginal;
    Vector3 cardListOriginal;

    float time;

    void Start()
    {
        endTurnOriginal = endTurn.transform.position;
        cardListOriginal = cardList.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(CameraController.self.state == CameraController.State.Inspect)
        {
            time = Mathf.Clamp01(time + Time.deltaTime);
        }
        else
        {
            time = Mathf.Clamp01(time - Time.deltaTime);
        }

        endTurn.transform.position = Vector3.Lerp(endTurnOriginal, new Vector3(endTurnOriginal.x - 1000, endTurnOriginal.y), time);
        cardList.transform.position = Vector3.Lerp(cardListOriginal, new Vector3(cardListOriginal.x + 1000, cardListOriginal.y), time);
    }
}
