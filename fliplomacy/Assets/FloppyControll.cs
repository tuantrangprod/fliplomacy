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
        floppyOnJump = Instantiate(FloopySprite,  new Vector3(0,0,-2), quaternion.identity);
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
        floppyOnJump.transform.position = FloopySprite.transform.position;
        floppyOnJump.transform.localScale = FloopySprite.transform.localScale;

        StartCoroutine(0.1f.Tweeng((p) => floppyOnJump.transform.localScale = p,
            FloopySprite.transform.localScale,
            new Vector3(0.2f, 0.2f, 0.2f)));

        FloopySprite.gameObject.SetActive(false);
        StopIdelAnim();

        //StopIdelAnim();
    }

    public void floppyOnStartReScale()
    {
        StartCoroutine(EndfloppyOnStartAnim());
    }

    IEnumerator EndfloppyOnStartAnim()
    {
        StartCoroutine(0.1f.Tweeng((p) => floppyOnJump.transform.localScale = p,
            floppyOnJump.transform.localScale,
            new Vector3(0.5f, 0.5f, 0.5f)));

        yield return new WaitForSeconds(0.1f);

        FloopySprite.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        FloopySprite.gameObject.SetActive(true);

        StartIdelAnim();

        floppyOnJump.gameObject.SetActive(false);

       
    }
    public bool FloppyInWormHole = false;
    public bool InMovingTile = false;
    public Vector3 movingTilePos = new Vector3();
    public void JumpAnim()
    {
        canswipe = false;

        StartCoroutine(0.2f.Tweeng((p) => floppyOnJump.transform.localScale = p,
            floppyOnJump.transform.localScale,
            new Vector3(0.5f, 0.5f, 0.5f)));

        if(!FloppyInWormHole)
        {
            if (!InMovingTile) 
            { 
                FloppyJumpEndPos(gameObject.transform.position); 
            }
            else
            {
                FloppyJumpEndPos(movingTilePos);
            }

            StartCoroutine(floppySpriteReScale(0.2f));
        }

    }
    
    public void TeleAnim(float floppyOnJumpX, float floppyOnJumpY)
    {
        FloppyJumpEndPos(new Vector3(floppyOnJumpX, floppyOnJumpY, gameObject.transform.position.z));

        StartCoroutine(TeleportThroughWormholes());
    }
    
    public IEnumerator TeleportThroughWormholes()
    {
        yield return new WaitForSeconds(0.2f);
        FloopySprite.transform.localScale = new Vector3(0, 0, 0);
        FloopySprite.gameObject.SetActive(true);

        StartCoroutine(0.5f.Tweeng((p) => FloopySprite.transform.localScale = p,
            FloopySprite.transform.localScale,
            new Vector3(0.5f, 0.5f, 0.5f)));

        StartCoroutine(0.5f.Tweeng((p) => floppyOnJump.transform.localScale = p,
            floppyOnJump.transform.localScale,
            new Vector3(0, 0, 0)));

        StartCoroutine(floppySpriteReScale(0.5f));
    }

    public bool haveMovingTile = false;

    IEnumerator floppySpriteReScale(float time)
    {
        yield return new WaitForSeconds(time);

        if (!InMovingTile)
        {
            FloppyReSacle();

            if (!haveMovingTile)
            {
                canswipe = true;
            }
            else
            {
                StartCoroutine(WaitMovingTile());
            }

            FloppyInWormHole = false;
        }
        else
        {
            FloppyJumpEndPos(gameObject.transform.position);
            StartCoroutine(WaitMovingTileEnd());
            if (!haveMovingTile)
            {
                canswipe = true;
            }
            else
            {
                StartCoroutine(WaitMovingTile());
            }
        }
    }
    public IEnumerator WaitMovingTile()
    {
        yield return new WaitForSeconds(0.2f);
        canswipe = true;
    }

    public IEnumerator WaitMovingTileEnd()
    {
        yield return new WaitForSeconds(0.2f);
        FloppyReSacle();
        InMovingTile = false;
    }
    public void FloppyReSacle()
    {
        FloopySprite.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        FloopySprite.gameObject.SetActive(true);
        StartIdelAnim();

        floppyOnJump.gameObject.SetActive(false);
    }
    void FloppyJumpEndPos(Vector3 pos)
    {
        StartCoroutine(0.2f.Tweeng((p) => floppyOnJump.transform.position = p,
         floppyOnJump.transform.position,
         pos));
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
