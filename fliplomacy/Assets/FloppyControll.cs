using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FloppyControll : MonoBehaviour
{
    public float timeInAAnimLoop = 1f;
    [HideInInspector] public GameObject floopySprite;
    public event Action EndJump;
    //private GameObject floppyOnStart;
    //private GameObject floppyOnJump;
    private void Awake()
    {
        canswipe = false;

    }
    public void SetUp()
    {
        
        var sprite = gameObject.transform.GetChild(0).gameObject;
        sprite.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        floopySprite = Instantiate(sprite,  new Vector3(0,0,-2), quaternion.identity);
        floopySprite.gameObject.SetActive(true);

    }
    public void ClearLevel()
    {
        canswipe = false;
        floppyInWormHole = false;
        inMovingTile = false;
        haveMovingTile = false;
        StopIdelAnim();
        if (floopySprite != null)
        {
            Destroy(floopySprite);
        }
        
    }
    IEnumerator floppyMove;
    IEnumerator floppyScale;
    public IEnumerator RePlayFloppyIdelAnim()
    {
        StartCoroutine("FloppyIdelAnim");
        while (true)
        {
            yield return new WaitForSeconds(timeInAAnimLoop * 2);
            StartCoroutine("FloppyIdelAnim");
        }
    }
    

    public IEnumerator FloppyIdelAnim()
    {
        FloopySpriteScale(timeInAAnimLoop, new Vector3(0.3f, 0.3f, 0.3f));

        yield return new WaitForSeconds(timeInAAnimLoop);

        FloopySpriteScale(timeInAAnimLoop, new Vector3(0.5f, 0.5f, 0.5f));
    }
    public void StartIdelAnim()
    {
        StartCoroutine("RePlayFloppyIdelAnim");
    }
    public void StopIdelAnim()
    {
        StopCoroutine("FloppyIdelAnim");
        StopCoroutine("RePlayFloppyIdelAnim");

    }
    public bool canswipe = true;

    [Obsolete]
    public void StartJumpAnim()
    {
        floopySprite.transform.position = gameObject.transform.position -  new Vector3(0,0,2);

        StopIdelAnim();
      

        FloopySpriteScale(0.1f, new Vector3(0.2f, 0.2f, 0.2f));

          EffectWhenEndMove(0.6f,0.2f);
    }

    public void FloppyReScale()
    {
        StartCoroutine(EndStartJumpAnim());
    }

    IEnumerator EndStartJumpAnim()
    {
        FloopySpriteScale(0.1f, new Vector3(0.5f, 0.5f, 0.5f));

        yield return new WaitForSeconds(0.1f);
        StartIdelAnim();
        Debug.Log("EndStartJumpAnim");



    }
    public bool floppyInWormHole = false;
    public bool inMovingTile = false;
    public Vector3 movingTilePos = new Vector3();
    public float wormholeStartPosX;
    public float wormholeStartPosY;

    [Obsolete]
    public void JumpAnim()
    {
        canswipe = false;

        FloopySpriteScale(0.2f, new Vector3(0.5f, 0.5f, 0.5f));

        if (!floppyInWormHole)
        {
            if (!inMovingTile) 
            {
                FloopySpriteMove(gameObject.transform.position); 
            }
            else
            {
                FloopySpriteMove(movingTilePos);
            }

            StartCoroutine(FloppySpriteEndJumpAnim(0.2f));
        }
        else
        {
            FloopySpriteMove(new Vector3(wormholeStartPosX, wormholeStartPosY, floopySprite.transform.position.z));
            StartCoroutine(TeleportThroughWormholes());
        }

    }  
    public IEnumerator TeleportThroughWormholes()
    {
        yield return new WaitForSeconds(0.2f);

        var floopySpriteWormHoleClone = Instantiate(floopySprite, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -2), quaternion.identity);

        StartCoroutine(0.5f.Tweeng((p) => floopySpriteWormHoleClone.transform.localScale = p,
            new Vector3(0f, 0f, 0f),
            new Vector3(0.5f, 0.5f, 0.5f)));

        FloopySpriteScale(0.5f, new Vector3(0f, 0f, 0f));

        StartCoroutine(EndTeleport(floopySpriteWormHoleClone));
    }
    IEnumerator EndTeleport(GameObject clone)
    {
        yield return new WaitForSeconds(0.5f);
        floppyInWormHole = false;
        canswipe = true;
        floopySprite.transform.position = gameObject.transform.position - new Vector3(0, 0, 2);
        floopySprite.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Destroy(clone);
        StartIdelAnim();
        EventEndJump();
    }

    public bool haveMovingTile = false;

    [Obsolete]
    public void EffectWhenEndMove(float size, float lifetime)
    {
        GameObject waveEfect = WavePool.SharedInstance.GetPooledObject();
        if (waveEfect != null)
        {
            waveEfect.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
            waveEfect.GetComponent<ParticleSystem>().startSize = size;
            waveEfect.GetComponent<ParticleSystem>().startLifetime = lifetime;
            waveEfect.gameObject.SetActive(true);
        }
    }
    public void EventEndJump()
    {
        EndJump?.Invoke();
    }
    [Obsolete]
    IEnumerator FloppySpriteEndJumpAnim(float time)
    {
        yield return new WaitForSeconds(time);
        StartIdelAnim();
        if (!inMovingTile)
        {
            // Effect
            EffectWhenEndMove(1.1f,0.5f);
            

            if (!haveMovingTile)
            {
                canswipe = true;
                EventEndJump();
            }
            else
            {
                StartCoroutine(WaitMovingTile());
            }

        }
        else
        {
            FloopySpriteMove(gameObject.transform.position - new Vector3(0, 0, 2));

            StartCoroutine(WaitMovingTileEnd());

            StartCoroutine(WaitMovingTile());
        }
    }
    public IEnumerator WaitMovingTile()
    {
        yield return new WaitForSeconds(0.2f);
        canswipe = true;
        EventEndJump();
    }

    public IEnumerator WaitMovingTileEnd()
    {
        yield return new WaitForSeconds(0.2f);
        StartIdelAnim();
        inMovingTile = false;
    }
    void FloopySpriteMove(Vector3 pos)
    {
        if (floppyMove != null)
        {
            StopCoroutine(floppyMove);
        }
        floppyMove = TweengMove(0.2f, floopySprite.transform.position, pos);
        StartCoroutine(floppyMove);
    }
    void FloopySpriteScale(float time, Vector3 scale)
    {
        if (floppyScale != null)
        {
            StopCoroutine(floppyScale);
        }
        floppyScale = TweengScale(time, floopySprite.transform.localScale, scale);
        StartCoroutine(floppyScale);
    }
    public void StopAllAnim()
    {
        floopySprite.gameObject.SetActive(false);
        var sprite = gameObject.transform.GetChild(0).gameObject;
        sprite.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        var clone =Instantiate(sprite, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -2), quaternion.identity);
        clone.gameObject.SetActive(true);
        StartCoroutine(0.5f.Tweeng((p) => clone.transform.localScale = p, clone.transform.localScale, new Vector3(0, 0, 0)));
    }
    public IEnumerator TweengScale(float duration, Vector3 aa, Vector3 zz)
    {
        float sT = Time.time;
        float eT = sT + duration;

        while (Time.time < eT && floopySprite != null)
        {
            float t = (Time.time - sT) / duration;
             floopySprite.transform.localScale = Vector3.Lerp(aa, zz, Mathf.SmoothStep(0f, 1f, t));
       
            yield return null;
        }
        if(floopySprite != null)
        {
            floopySprite.transform.localScale = zz;
        }
         
    }
    public IEnumerator TweengMove(float duration, Vector3 aa, Vector3 zz)
    {
        float sT = Time.time;
        float eT = sT + duration;

        while (Time.time < eT && floopySprite != null)
        {
            float t = (Time.time - sT) / duration;
            floopySprite.transform.position = Vector3.Lerp(aa, zz, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        if (floopySprite != null)
        {
            floopySprite.transform.position = zz;
        }
       
    }

}
