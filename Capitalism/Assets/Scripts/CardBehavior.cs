using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardBehavior : MonoBehaviour
{
    public Transform assetPlace;
    public Asset currentAsset;

    public bool closed;
    public bool placed;

    private TextMeshPro nameText;
    private TextMeshPro descriptionText;

    private TextMeshPro asset;

    private SpriteRenderer background;
    private SpriteRenderer picture;
    private SpriteRenderer assetInput;

    private void Start()
    {
        assetPlace = transform.Find("AssetInput/AssetCollider");
        currentAsset = null;

        background = transform.Find("Background").GetComponent<SpriteRenderer>();
        picture = transform.Find("Picture").GetComponent<SpriteRenderer>();
        assetInput = transform.Find("AssetInput").GetComponent<SpriteRenderer>();

        nameText = transform.Find("Name").GetComponent<TextMeshPro>();
        descriptionText = transform.Find("Body").GetComponent<TextMeshPro>();
        asset = transform.Find("AssetInput/Asset:").GetComponent<TextMeshPro>();
        

    }

    private void Update()
    {
        if (currentAsset != null)
        {
            currentAsset.transform.position = assetPlace.position;
            currentAsset.transform.rotation = assetPlace.rotation;
        }

        if(closed && currentAsset != null)
        {
            currentAsset.Free();
        }
    }

    public void updateOrder(int baseID)
    {
        background.sortingOrder = baseID + 1;
        picture.sortingOrder = baseID + 2;
        assetInput.sortingOrder = baseID + 0;

        nameText.sortingOrder = baseID + 2;
        descriptionText.sortingOrder = baseID + 2;
        asset.sortingOrder = baseID + 2;
    }


}
