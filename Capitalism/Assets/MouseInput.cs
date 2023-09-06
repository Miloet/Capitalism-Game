using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    Camera cam;
    public static Vector2 worldPosition = Vector2.zero;
    public static Vector2 mousePosition = Vector2.zero;

    private static string[] tagsToCheck = {"Asset", "Skill"};

    private Vector2 orginialPos = Vector2.zero;

    public GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        worldPosition = cam.ScreenToWorldPoint(mousePosition);

        

        if (Input.GetMouseButtonDown(0))
        {
            print("Pressed");
            if (test == null)
            {

                RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
                if (hit.collider != null && tagCheck(hit.collider.gameObject.tag, tagsToCheck))
                {
                    orginialPos = hit.transform.position;
                    test = hit.transform.gameObject;
                }
            }
            else
            {
                test.transform.position = orginialPos;
                test = null;
            }

        }

        if (test != null) test.transform.position = worldPosition;
    }

    public static bool IsClose(Vector2 pos, float distance = 1f)
    {
        return Vector2.Distance(pos, worldPosition) < distance;
    }


    static bool tagCheck(string tag, string[] tagsToCheck)
    {
        foreach (string t in tagsToCheck)
        {
            if (tag == t)
            {
                return true;
            }
        }
        return false;
    }
}

