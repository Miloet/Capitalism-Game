using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Animate());
    }
    IEnumerator Animate()
    {
        float t = 0;
        while(t < 1)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero,Vector3.one*2f,t);
            t += Time.deltaTime*2f;
            yield return null;
        }
    }
}
