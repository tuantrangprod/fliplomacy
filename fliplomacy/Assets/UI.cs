using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
     Vector3 startPos;
     Vector3 endPos;
    public float speed = 0.8f;
    public float distanceX;
    public float distanceY;

    private void Start()
    {
        startPos = gameObject.transform.position;
        endPos = startPos + new Vector3(distanceX, distanceY, 0);
    }
    public void TeleToEndPos()
    {
        gameObject.transform.position = endPos;
    }
    public void TeleToStartPos()
    {
        gameObject.transform.position = startPos;
    }
    public void MoveToEndPos()
    {
        StartCoroutine(speed.Tweeng((p) => gameObject.transform.position = p, startPos, endPos));
    }
    public void BackToStartPos()
    {
        StartCoroutine(speed.Tweeng((p) => gameObject.transform.position = p, endPos  ,startPos ));
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.K)))
        {
            endPos = gameObject.transform.position;
        }
    }
}
