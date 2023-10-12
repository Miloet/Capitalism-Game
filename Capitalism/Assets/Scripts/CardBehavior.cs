using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardBehavior : MonoBehaviour
{
    public Transform assetPlace;
    public Asset currentAsset;

    public Vector3 next;
    float speed;

    public bool closed;
    public bool placed;

    private TextMeshPro nameText;
    private TextMeshPro descriptionText;

    private TextMeshPro asset;

    private SpriteRenderer background;
    private SpriteRenderer picture;
    private SpriteRenderer assetInput;

    public bool move = true;

    Vector3 originalPosition;
    float time;

    private void Start()
    {
        assetPlace = transform.Find("AssetInput/AssetPosition");
        currentAsset = null;

        background = transform.Find("Background").GetComponent<SpriteRenderer>();
        picture = transform.Find("Picture").GetComponent<SpriteRenderer>();
        assetInput = transform.Find("AssetInput").GetComponent<SpriteRenderer>();

        nameText = transform.Find("Name").GetComponent<TextMeshPro>();
        descriptionText = transform.Find("Body").GetComponent<TextMeshPro>();
        asset = transform.Find("AssetInput/Asset:").GetComponent<TextMeshPro>();

        next = transform.position;

        StartCoroutine(sorting());
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
    }

    private IEnumerator<WaitForSeconds> sorting()
    {
        while (true)
        {
            if (MouseInput.selected == gameObject) updateOrder(100);
            else updateOrder((int)Mathf.Floor(transform.position.x * 10f));
            yield return new WaitForSeconds(Time.deltaTime + 0.1f);
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

    public void MoveTo(Vector3 pos)
    {
        time = 0;
        originalPosition = transform.position;
        next = pos;
    }
}
