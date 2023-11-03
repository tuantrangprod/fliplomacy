using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    public float showTime = 0.6f;
    private void OnEnable()
    {
        StartCoroutine(Hiding());
    }
    IEnumerator Hiding()
    {
        yield return new WaitForSeconds(showTime);
        gameObject.SetActive(false);
    }
}
