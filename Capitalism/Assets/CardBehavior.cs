using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardBehavior : MonoBehaviour
{
    public Transform assetPlace;
    public Asset currentAsset;

    private TextMeshPro nameText;
    private TextMeshPro descriptionText;
    
    private void Start()
    {
        assetPlace = transform.Find("AssetInput/AssetCollider");
        currentAsset = null;
    }

    private void Update()
    {
        if (currentAsset != null) currentAsset.transform.position = assetPlace.position;
    }


}
