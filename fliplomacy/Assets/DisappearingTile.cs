using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingTile : MonoBehaviour
{
    [HideInInspector] public GameObject DisappearingSprite;
    public void SetUP()
    {
        Instantiate(DisappearingSprite, gameObject.transform.position, Quaternion.identity).transform.SetParent(gameObject.transform);
    }
    public void HideObject()
    {
        //gameObject.SetActive(false);
        //var DisappearingSprite = gameObject.transform.GetChild(0);
        //StartCoroutine(0.3f.Tweeng((p) => DisappearingSprite.transform.localEulerAngles = p,
        //  DisappearingSprite.gameObject.transform.localEulerAngles,
        //  DisappearingSprite.gameObject.transform.localEulerAngles + new Vector3(0,0 , 360)));
        //StartCoroutine(0.3f.Tweeng((p) => DisappearingSprite.transform.localScale = p,
        //  DisappearingSprite.gameObject.transform.localScale,
        //   new Vector3(0, 0, 0)));
        StartCoroutine(0.5f.Tweeng((p) => gameObject.transform.localEulerAngles = p,
          gameObject.gameObject.transform.localEulerAngles,
          gameObject.gameObject.transform.localEulerAngles + new Vector3(0, 0 , 360)));
        StartCoroutine(0.6f.Tweeng((p) => gameObject.transform.localScale = p,
          gameObject.gameObject.transform.localScale,
           new Vector3(0, 0, 0)));
    }
}
