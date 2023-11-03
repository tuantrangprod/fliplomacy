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
        StopCoroutine("RePlayDisappearingOnTrigerIdelAnim");
        StopCoroutine("DisappearingOnTrigerIdelAnim");
        StopCoroutine(scale);

        StartCoroutine(0.5f.Tweeng((p) => gameObject.transform.localEulerAngles = p,
          gameObject.gameObject.transform.localEulerAngles,
          gameObject.gameObject.transform.localEulerAngles + new Vector3(0, 0, 360)));
        StartCoroutine(0.6f.Tweeng((p) => gameObject.transform.localScale = p,
          gameObject.gameObject.transform.localScale,
           new Vector3(0, 0, 0)));
    }
    public void HaveObjectOn()
    {
        StartCoroutine("RePlayDisappearingOnTrigerIdelAnim");
        Debug.Log("DisappearingOnTriger");
    }
    public IEnumerator RePlayDisappearingOnTrigerIdelAnim()
    {
        StartCoroutine("DisappearingOnTrigerIdelAnim");
        while (true)
        {
            yield return new WaitForSeconds(0.6f * 2);
            StartCoroutine("DisappearingOnTrigerIdelAnim");
        }
    }

    IEnumerator scale;
    public IEnumerator DisappearingOnTrigerIdelAnim()
    {
        scale = 0.6f.Tweeng((p) => gameObject.transform.localScale = p,
         gameObject.transform.localScale,
         new Vector3(0.85f, 0.85f, 0.85f));
        StartCoroutine(scale);

        yield return new WaitForSeconds(0.5f);

        scale = 0.6f.Tweeng((p) => gameObject.transform.localScale = p,
        gameObject.transform.localScale,
        new Vector3(1f, 1f, 1f));
        StartCoroutine(scale);
    }
}
