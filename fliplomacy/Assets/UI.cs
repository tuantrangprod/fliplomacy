using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;

    private void Start()
    {
        startPos = gameObject.transform.position;
    }
    public void MoveToEndPos()
    {
        StartCoroutine(1f.Tweeng((p) => gameObject.transform.position = p, startPos, endPos));
    }
    public void BackToStartPos()
    {
        StartCoroutine(1f.Tweeng((p) => gameObject.transform.position = p, endPos  ,startPos ));
    }
}
