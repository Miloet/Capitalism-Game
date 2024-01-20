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

    private Vector3 originalPos = Vector2.zero;
    private Vector3 originalUp = Vector2.zero;

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
                originalPos = hit.transform.position;
                selected = hit.transform.gameObject;
                Player.self.currentHand.Remove(selected);
                Player.UpdateCardInHand();
                if (selected.tag == "Asset")
                {
                    selected.GetComponent<Asset>().Free(true);
                }
                if (selected.tag == "Skill")
                {
                    selected.GetComponent<CardBehavior>().closed = false;
                }
            }
        }
        else
        {
            switch (selected.tag)
            {
                case "Asset":
                    
                    bool inRange = false; 
                    foreach (SkillBase card in cards)
                    {
                        if (IsClose(card.assetPlace.position, 1))
                        {
                            if (card.currentAsset != null) card.currentAsset.Free();
                            card.closed = false;
                            var asset = selected.GetComponent<Asset>();
                            card.currentAsset = asset;
                            asset.owner = card;
                            inRange = true;

                            break;
                        }
                    }
                    resetSelected(false, !inRange);

                    break;

                case "Skill":
                    if (CardCompiler.inBounds(selected.transform))
                    {
                        selected.GetComponent<CardBehavior>().MoveTo(selected.transform.position);
                        resetSelected(false, false);
                    }
                    else resetSelected();
                    break;
            }


        }

        CardCompiler.UpdateText();
    }

    private void OnRightClick()
    {
        if(!DrawPile.showing)
        {
            if (selected == null && CameraController.self.state != CameraController.State.Selected)
                CameraController.UpdateCamera(CameraController.State.Selected);
            else CameraController.UpdateCamera(CameraController.State.Default);
        }
        resetSelected();
        CardCompiler.UpdateText();
    }


    public static void updateCardCount()
    {
        cards = FindObjectsByType<SkillBase>(FindObjectsSortMode.None);
    }

    public void UpdateCamera()
    {
        if (DrawPile.showing) CameraController.UpdateCamera(CameraController.State.Default);
        else if (selected)
        {
            CameraController.UpdateCamera(CameraController.State.Selected);
        }
        else
        {
            if(CameraController.self.state == CameraController.State.Default) if(mousePosition.y < 100) CameraController.UpdateCamera(CameraController.State.Inspect);
            else CameraController.UpdateCamera(CameraController.State.Default);
            else if(CameraController.self.state == CameraController.State.Inspect && mousePosition.y > 800) CameraController.UpdateCamera(CameraController.State.Default);
        }
    }


    private void resetSelected(bool original = true, bool add = true)
    {
        if (selected != null)
        {
            CardBehavior cb = selected.GetComponent<CardBehavior>();
            if (original)
            {
                if (cb != null) cb.MoveTo(originalPos);
                else selected.transform.position = originalPos;
            }
            if (add) Player.AddUnique(selected);

            Player.UpdateCardInHand();
            selected = null;
        }
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

