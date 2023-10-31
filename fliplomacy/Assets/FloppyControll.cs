using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FloppyControll : MonoBehaviour
{
    public float timeInAAnimLoop = 1f;
    private GameObject FloopySprite;
    public bool inJump = false;
    private void Start()
    {
        FloopySprite = gameObject.transform.GetChild(0).gameObject;
        StartCoroutine(FloppyIdelAnim());
        StartCoroutine(RePlayFloppyIdelAnim());
        
    }
    public IEnumerator RePlayFloppyIdelAnim()
    {
        while (!inJump)
        {
            yield return new WaitForSeconds(timeInAAnimLoop*2);
            StartCoroutine(FloppyIdelAnim());
        }
        
    }

    public IEnumerator FloppyIdelAnim()
    {
        StartCoroutine(timeInAAnimLoop.Tweeng((p) => FloopySprite.transform.localScale = p,
            FloopySprite.transform.localScale,
            new Vector3(0.4f, 0.4f, 0.4f)));
        yield return new WaitForSeconds(timeInAAnimLoop);
        StartCoroutine(timeInAAnimLoop.Tweeng((p) => FloopySprite.transform.localScale = p,
            FloopySprite.transform.localScale,
            new Vector3(0.5f, 0.5f, 0.5f)));
    }

    private GameObject floppyOnStart;
    private GameObject  floppyOnJump;
    public void StartJumpAnim()
    {
        floppyOnStart = Instantiate(FloopySprite, FloopySprite.transform.position, quaternion.identity);
        floppyOnStart.transform.localScale = FloopySprite.transform.localScale;
        FloopySprite.gameObject.SetActive(false);
        StartCoroutine(0.2f.Tweeng((p) => floppyOnStart.transform.localScale = p,
            floppyOnStart.transform.localScale,
            new Vector3(0.2f, 0.2f, 0.2f)));
        
    }

    public void floppyOnStartReScale()
    {
        //yield return new WaitForSeconds(0.2f);
        // StartCoroutine(0.2f.Tweeng((p) => floppyOnStart.transform.localScale = p,
        //     floppyOnStart.transform.localScale,
        //     new Vector3(0.5f, 0.5f, 0.5f)));
        StartCoroutine(EndfloppyOnStartAnim());
    }

    IEnumerator EndfloppyOnStartAnim()
    {
        StartCoroutine(0.2f.Tweeng((p) => floppyOnStart.transform.localScale = p,
            floppyOnStart.transform.localScale,
            new Vector3(0.5f, 0.5f, 0.5f)));
        yield return new WaitForSeconds(0.2f);
        floppyOnStart.gameObject.SetActive(false);
        FloopySprite.gameObject.SetActive(true);
    }

    public void JumpAnim()
    {
        inJump = true;
        floppyOnJump= Instantiate(floppyOnStart, floppyOnStart.transform.position, quaternion.identity);
        floppyOnJump.transform.localScale = floppyOnStart.transform.localScale;
        floppyOnStart.gameObject.SetActive(false);
        StartCoroutine(0.2f.Tweeng((p) => floppyOnJump.transform.localScale = p,
            floppyOnJump.transform.localScale,
            new Vector3(0.5f, 0.5f, 0.5f)));
        StartCoroutine(0.2f.Tweeng((p) => floppyOnJump.transform.position = p,
            floppyOnJump.transform.position,
            gameObject.transform.position));
        StartCoroutine(floppySpriteReScale());
    }

    IEnumerator floppySpriteReScale()
    {
        yield return new WaitForSeconds(0.2f);
        inJump = false;
        floppyOnJump.gameObject.SetActive(false);
        FloopySprite.gameObject.SetActive(true);
        StartCoroutine(FloppyIdelAnim());
        StartCoroutine(RePlayFloppyIdelAnim());
    }
}
