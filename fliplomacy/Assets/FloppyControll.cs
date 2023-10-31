using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FloppyControll : MonoBehaviour
{
    public float timeInAAnimLoop = 1f;
    private GameObject FloopySprite;
    //private GameObject floppyOnStart;
    private GameObject floppyOnJump;
    private void Start()
    {
        
        FloopySprite = gameObject.transform.GetChild(0).gameObject;
        StartIdelAnim();
        floppyOnJump = Instantiate(FloopySprite, FloopySprite.transform.position, quaternion.identity);
        floppyOnJump.gameObject.SetActive(false);

    }
    public IEnumerator RePlayFloppyIdelAnim()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeInAAnimLoop * 2);
            StartCoroutine(FloppyIdelAnim());
        }
        
    }

    public IEnumerator FloppyIdelAnim()
    {
        StartCoroutine(timeInAAnimLoop.Tweeng((p) => FloopySprite.transform.localScale = p,
            FloopySprite.transform.localScale,
            new Vector3(0.4f, 0.3f, 0.4f)));
        yield return new WaitForSeconds(timeInAAnimLoop);
        StartCoroutine(timeInAAnimLoop.Tweeng((p) => FloopySprite.transform.localScale = p,
            FloopySprite.transform.localScale,
            new Vector3(0.5f, 0.5f, 0.5f)));
    }
    public bool canswipe = true;
    public void StartJumpAnim()
    {
        floppyOnJump.gameObject.SetActive(true);
        floppyOnJump.transform.localScale = FloopySprite.transform.localScale;
        StartCoroutine(0.3f.Tweeng((p) => floppyOnJump.transform.localScale = p,
            FloopySprite.transform.localScale,
            new Vector3(0.2f, 0.2f, 0.2f)));

        FloopySprite.gameObject.SetActive(false);
        StopIdelAnim();
    }

    public void floppyOnStartReScale()
    {
        StartCoroutine(EndfloppyOnStartAnim());
    }

    IEnumerator EndfloppyOnStartAnim()
    {
        StartCoroutine(0.3f.Tweeng((p) => floppyOnJump.transform.localScale = p,
            floppyOnJump.transform.localScale,
            new Vector3(0.5f, 0.5f, 0.5f)));

        yield return new WaitForSeconds(0.3f);

        FloopySprite.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        FloopySprite.gameObject.SetActive(true);
        StartIdelAnim();

        floppyOnJump.gameObject.SetActive(false);

       
    }

    public void JumpAnim()
    {
        canswipe = false;

        floppyOnJump.gameObject.SetActive(true);
        StartCoroutine(0.2f.Tweeng((p) => floppyOnJump.transform.localScale = p,
            floppyOnJump.transform.localScale,
            new Vector3(0.5f, 0.5f, 0.5f)));

        StartCoroutine(0.2f.Tweeng((p) => floppyOnJump.transform.position = p,
            floppyOnJump.transform.position,
            gameObject.transform.position));

        StartCoroutine(floppySpriteReScale());

        FloopySprite.gameObject.SetActive(false);
        StopIdelAnim();
    }

    IEnumerator floppySpriteReScale()
    {
        yield return new WaitForSeconds(0.2f);
        
        FloopySprite.transform.localScale= new Vector3(0.5f, 0.5f, 0.5f);
        FloopySprite.gameObject.SetActive(true);
        StartIdelAnim();

        floppyOnJump.gameObject.SetActive(false);

        canswipe = true;
    }
    public void StopIdelAnim()
    {
        StopCoroutine(FloppyIdelAnim());
        StopCoroutine(RePlayFloppyIdelAnim());
    }
    public void StartIdelAnim()
    {
        StopCoroutine(FloppyIdelAnim());
        StopCoroutine(RePlayFloppyIdelAnim());

        StartCoroutine(FloppyIdelAnim());
        StartCoroutine(RePlayFloppyIdelAnim());
    }
}
