using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    Camera cam;
    public static Vector2 worldPosition = Vector2.zero;
    public static Vector2 mousePosition = Vector2.zero;

    private static string[] tagsToCheck = {"Asset", "Skill"};

    public static CardBehavior[] cards;

    private Vector2 orginialPos = Vector2.zero;

    public GameObject selected;

    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        cards = new CardBehavior[0];
        updateCardCount();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        worldPosition = cam.ScreenToWorldPoint(mousePosition);

        
        if (Input.GetMouseButtonDown(0))
        {
            print("Left Pressed");
            
           
            if (selected == null)
            {
                RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
                if (hit.collider != null && tagCheck(hit.collider.gameObject.tag, tagsToCheck))
                {
                    orginialPos = hit.transform.position;
                    selected = hit.transform.gameObject;
                    if(selected.tag == "Asset")
                    {
                        selected.GetComponent<Asset>().Free();
                    }
                }
            }
            else
            {
                switch(selected.tag)
                {
                    case "Asset":
                        foreach (SkillBase card in cards)
                        {
                            if (IsClose(card.assetPlace.position, 1))
                            {
                                var asset = selected.GetComponent<Asset>();
                                card.currentAsset = asset;
                                asset.owner = card;
                                break;
                            }
                        }
                        resetSelected(false);

                        break;

                    case "Skill":
                        if (CardCompiler.inBounds(selected.transform))
                        {
                            resetSelected(false);
                        }
                        else resetSelected();

                    break;
                }

               
            }

            CardCompiler.UpdateText();
        }

        if (Input.GetMouseButtonDown(1))
        {
            print("Right Pressed");
            
            if (selected != null)
            {
                selected = null;
            }
            CardCompiler.UpdateText();
        }

        if (selected != null) selected.transform.position = worldPosition;

        
    }

    public static void updateCardCount()
    {
        cards = FindObjectsByType<SkillBase>(FindObjectsSortMode.None);
    }

    private void resetSelected(bool original = true)
    {
        if(original) selected.transform.position = orginialPos;
        selected = null;
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

