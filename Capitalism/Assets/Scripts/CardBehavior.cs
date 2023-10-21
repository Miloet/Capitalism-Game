using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardBehavior : MonoBehaviour
{
    public Transform assetPlace;
    public Asset currentAsset;

    public Vector3 next;

    public bool closed = true;
    public bool placed;

    private TextMeshPro nameText;
    private TextMeshPro descriptionText;

    private TextMeshPro asset;

    private SpriteRenderer background;
    private SpriteRenderer picture;
    private SpriteRenderer assetInput;

    public Transform assetInputTransfrom;

    private float assetInputTime;
    private Vector3 assetInputOriginal;

    public bool move = true;

    Vector3 originalPosition;
    float time;
    
    private void Awake()
    {
        assetPlace = transform.Find("AssetInput/AssetPosition").transform;

        background = transform.Find("Background").GetComponent<SpriteRenderer>();
        picture = transform.Find("Picture").GetComponent<SpriteRenderer>();

        assetInput = transform.Find("AssetInput").GetComponent<SpriteRenderer>();
        assetInputTransfrom = assetInput.transform;
        assetInputOriginal = assetInputTransfrom.localPosition;

        nameText = transform.Find("Name").GetComponent<TextMeshPro>();
        descriptionText = transform.Find("Body").GetComponent<TextMeshPro>();
        asset = transform.Find("AssetInput/Asset:").GetComponent<TextMeshPro>();

        next = transform.position;
        
        StartCoroutine(Sorting());

        currentAsset = null;
    }

    private void Update()
    {
        if (currentAsset != null)
        {
            currentAsset.move = false;
            currentAsset.transform.position = assetPlace.position;
            currentAsset.transform.rotation = assetPlace.rotation; 
        }

        if (closed && currentAsset != null)
        {
            currentAsset.Free();
        }

        if (MouseInput.selected != gameObject && move)
        {
            time += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(time));
            transform.position = Vector3.Lerp(originalPosition, next, t);
        }
        if (assetInputTransfrom != null)
        {
            if (closed)
                assetInputTime = Mathf.Clamp01(assetInputTime + Time.deltaTime);
            else
                assetInputTime = Mathf.Clamp01(assetInputTime - Time.deltaTime);
            assetInputTransfrom.localPosition =
                Vector3.Lerp(assetInputOriginal,
                new Vector3(assetInputOriginal.x - 1.7f,
                assetInputOriginal.y), assetInputTime);
        }
    }

    private IEnumerator<WaitForSeconds> Sorting()
    {
        while (true)
        {
            if (MouseInput.selected == gameObject) updateOrder(1000);
            else updateOrder((int)Mathf.Floor(transform.position.x * 50f));
            yield return new WaitForSeconds(Time.deltaTime + 0.1f);
        }
    }

    public void updateOrder(int baseID)
    {
        background.sortingOrder = baseID + 2;
        picture.sortingOrder = baseID + 4;
        descriptionText.sortingOrder = baseID + 3;
        nameText.sortingOrder = baseID + 3;
        

        assetInput.sortingOrder = baseID + 0;
        asset.sortingOrder = baseID + 1;
    }

    public void MoveTo(Vector3 pos)
    {
        time = 0;
        originalPosition = transform.position;
        next = pos;
    }
}
