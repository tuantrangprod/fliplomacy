using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMarkedTile : MonoBehaviour
{
    [HideInInspector] public GameObject BombMarkedSprite;
    public void SetUP()
    {
        Instantiate(BombMarkedSprite, gameObject.transform.position, Quaternion.identity).transform.SetParent(gameObject.transform);
    }
    public void Hiding()
    {
        StartCoroutine(0.5f.Tweeng((p) => gameObject.transform.localEulerAngles = p,
        gameObject.gameObject.transform.localEulerAngles,
        gameObject.gameObject.transform.localEulerAngles + new Vector3(0, 0, 360)));
        StartCoroutine(0.6f.Tweeng((p) => gameObject.transform.localScale = p,
          gameObject.gameObject.transform.localScale,
           new Vector3(0, 0, 0)));
    }
}
