using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseInput : MonoBehaviour
{
    
    Camera cam;
    public static Vector3 worldPosition = Vector2.zero;
    public static Vector3 mousePosition = Vector2.zero;

    private static string[] tagsToCheck = {"Asset", "Skill"};
    private static string[] otherTags = {"Hand"};

    public static CardBehavior[] cards;

    private Vector2 orginialPos = Vector2.zero;

    public static GameObject selected;

    LayerMask defaultLM;
    LayerMask csLM;


    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        defaultLM = LayerMask.GetMask("Default");
        csLM = LayerMask.GetMask("Card");
        cards = new CardBehavior[0];
        updateCardCount();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        worldPosition = cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, cam.nearClipPlane));

        if (Input.GetMouseButtonDown(0))
        {
            OnLeftClick();
        }
        

        if (Input.GetMouseButtonDown(1))
        {
            OnRightClick();
        }

        if (selected != null)
        {
            Ray ray = cam.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, defaultLM) && tagCheck(hit.collider.gameObject.tag, otherTags))
            {
                selected.transform.position = hit.point + hit.normal/2;
                selected.transform.forward = -hit.normal;
            }
        }

        UpdateCamera();
    }

    public void OnLeftClick()
    {

        if (selected == null)
        {
            Ray ray = cam.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, csLM) && tagCheck(hit.collider.gameObject.tag, tagsToCheck))
            {
                orginialPos = hit.transform.position;
                selected = hit.transform.gameObject;
                if (selected.tag == "Asset")
                {
                    selected.GetComponent<Asset>().Free();
                }
            }
        }
        else
        {
            switch (selected.tag)
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
                    else
                    {
                        resetSelected();
                    }

                    break;
            }


        }

        CardCompiler.UpdateText();
    }

    private void OnRightClick()
    {
        CameraController.UpdateCamera(CameraController.State.Default);
        if (selected != null)
        {
            selected = null;
        }
        CardCompiler.UpdateText();
    }


    public static void updateCardCount()
    {
        cards = FindObjectsByType<SkillBase>(FindObjectsSortMode.None);
    }

    public void UpdateCamera()
    {
        if (selected)
        {
            CameraController.UpdateCamera(CameraController.State.Selected);
        }
        else
        {
            if(CameraController.self.state == CameraController.State.Default) if(mousePosition.y < 200) CameraController.UpdateCamera(CameraController.State.Inspect);
            else CameraController.UpdateCamera(CameraController.State.Default);
            else if(mousePosition.y > 800) CameraController.UpdateCamera(CameraController.State.Default);
        }
    }


    private void resetSelected(bool original = true)
    {
        if(original) selected.transform.position = orginialPos;
        selected = null;
    }
    public bool IsClose(Vector3 pos, float distance = 1f)
    {
        return Vector3.Distance(pos, selected.transform.position) < distance;
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

