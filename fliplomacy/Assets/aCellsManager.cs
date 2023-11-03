using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aCellsManager : MonoBehaviour
{
    public void StartAnim(float time, AnimationCurve curve)
    {
        StartCoroutine(time.Tweeng((p) => gameObject.transform.position = p, new Vector3(2, 10, 0), gameObject.transform.position, curve));
        StartCoroutine(time.Tweeng((p) => gameObject.transform.localEulerAngles = p, new Vector3(0, 0, Random.Range(-360,360)), gameObject.transform.localEulerAngles, curve));
    }
}
