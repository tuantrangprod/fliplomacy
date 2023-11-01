using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormholeTile : MonoBehaviour
{
    [HideInInspector] public GameObject WormholeSprite;
    public GameObject ConnetedObject;
    public bool mainHole = false;
    public Color color;
    public float rotateSpeed = 0;
    GameObject gameObjectSprite;
    GameObject connetedSprite;
    bool startRotate = false;
    public void SetUP()
    {
        Instantiate(WormholeSprite, gameObject.transform.position, Quaternion.identity).transform.SetParent(gameObject.transform);
        if (mainHole)
        {
            //randomColor = Random.ColorHSV(0.0f, 1.0f, 0.0f, 1.0f, 0.5f, 1.0f, 0.5f, 0.5f);
            gameObjectSprite = gameObject.transform.GetChild(1).transform.GetChild(0).gameObject;
            gameObjectSprite.GetComponent<SpriteRenderer>().color = color;
            connetedSprite = ConnetedObject.transform.GetChild(1).transform.GetChild(0).gameObject;
            connetedSprite.GetComponent<SpriteRenderer>().color = color;
           // rotateSpeed = 0.5f;

            startRotate = true;

            //StartCoroutine(ReplayWormHoleIdelAnim());
        }
    }
    private void Update()
    {
        if (startRotate)
        {
            gameObjectSprite.transform.localEulerAngles += new Vector3(0, 0, rotateSpeed);
  
            connetedSprite.transform.localEulerAngles += new Vector3(0, 0, rotateSpeed);
        }
    }
    //public IEnumerator ReplayWormHoleIdelAnim()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(timeInAAnimLoop * 2);
    //        StartCoroutine(CallAWormHoleIdelAnim());
    //    }
    //}
    //public IEnumerator CallAWormHoleIdelAnim()
    //{
    //    StartCoroutine(timeInAAnimLoop.Tweeng((p) => gameObjectSprite.transform.localScale = p,
    //        gameObjectSprite.transform.localScale,
    //        new Vector3(0.8f, 0.8f, 0.8f)));
    //    yield return new WaitForSeconds(timeInAAnimLoop);
    //    StartCoroutine(WormHoleIdelAnim());

    //}

    //public IEnumerator WormHoleIdelAnim()
    //{
    //    StartCoroutine(timeInAAnimLoop.Tweeng((p) => gameObjectSprite.transform.localScale = p,
    //        gameObjectSprite.transform.localScale,
    //        new Vector3(1f, 1f, 1f)));

    //    StartCoroutine(timeInAAnimLoop.Tweeng((p) => connetedSprite.transform.localScale = p,
    //       connetedSprite.transform.localScale,
    //       new Vector3(0.8f, 0.8f, 0.8f)));

    //    yield return new WaitForSeconds(timeInAAnimLoop);

    //    StartCoroutine(timeInAAnimLoop.Tweeng((p) => gameObjectSprite.transform.localScale = p,
    //        gameObjectSprite.transform.localScale,
    //        new Vector3(0.8f, 0.8f, 0.8f)));


    //    StartCoroutine(timeInAAnimLoop.Tweeng((p) => connetedSprite.transform.localScale = p,
    //       connetedSprite.transform.localScale,
    //       new Vector3(1f, 1f, 1f)));
    //}
}
