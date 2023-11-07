using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class FlagTile : MonoBehaviour
{
    [HideInInspector] public GameObject flagSprite;
    [HideInInspector] public int flagStatus = 0;
    //private Color _color1;
    private Color _color;
    private GameObject flag;
    public void SetUP()
    {
        flag = Instantiate(flagSprite, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,-0.01f), Quaternion.identity);
        flag.transform.SetParent(gameObject.transform.parent);
        flag.name = "Flag";
        // SetColor();
        // gameObject.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>()
        //     .color = _color;

    }
    public void ChangeFlag(string swipeDirection)
    {
        if (gameObject.transform.childCount == 1)
        {
            flag.transform.SetParent(gameObject.transform);
        }
        if (swipeDirection == "Left" || swipeDirection == "Right")
        {
           //  StartCoroutine(0.2f.Tweeng((p) => flag.gameObject.transform.localEulerAngles = p,
           // flag.gameObject.transform.localEulerAngles,
           // flag.gameObject.transform.localEulerAngles + new Vector3(0, 180, 0)));
            StartCoroutine(0.2f.Tweeng((p) => gameObject.transform.localEulerAngles = p,
                gameObject.transform.localEulerAngles,
                gameObject.transform.localEulerAngles + new Vector3(0, 360, 0)));
        }
        else
        {
            StartCoroutine(0.2f.Tweeng((p) => gameObject.transform.localEulerAngles = p,
           gameObject.transform.localEulerAngles,
           gameObject.transform.localEulerAngles + new Vector3(360, 0, 0)));
            // StartCoroutine(0.2f.Tweeng((p) => flag.gameObject.transform.localEulerAngles = p,
            //     flag.gameObject.transform.localEulerAngles,
            //     flag.gameObject.transform.localEulerAngles + new Vector3(360, 0, 0)));
        }


        SetColor();
        TweenColor();

    }

    public void SetColor()
    {
        if (flagStatus == 0)
        {
            flagStatus = 1;
            _color = flag.gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color;
           // _color2 = flag.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
        }
        else
        {
            flagStatus = 0;
            _color = flag.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
            //_color2 = flag.gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color;
        }
    }
    private void TweenColor()
    {

        var flagsprite = flag.gameObject.transform.GetChild(2).gameObject;
        var spriteRenderer = flagsprite.GetComponent<SpriteRenderer>();
        System.Action<ITween<Color>> updateColor = (t) =>
        {
            spriteRenderer.color = t.CurrentValue;
        };
        var key = Random.Range(0, 1000000);
        flagsprite.gameObject.Tween(key.ToString(), spriteRenderer.color,
            _color, 0.1f, TweenScaleFunctions.QuadraticEaseOut, updateColor);
    }
    public void WinGameAnin(string key, Color color, AnimationCurve curve, GameObject wave)
    {
        var flagbackgroundsprite = gameObject.transform.GetChild(0).gameObject;
        var spriteRenderer = flagbackgroundsprite.GetComponent<SpriteRenderer>();
        System.Action<ITween<Color>> updateColor = (t) =>
        {
               spriteRenderer.color = t.CurrentValue;
        };
        flagbackgroundsprite.gameObject.Tween(key, spriteRenderer.color,
               color, 0.2f, TweenScaleFunctions.QuadraticEaseOut, updateColor);
        gameObject.transform.position -= new Vector3(0, 0, 2);
        flag.transform.position -= new Vector3(0, 0, 2);
        flag.transform.SetParent(gameObject.transform.parent);
        StartCoroutine(RoatateFlag(curve));
        StartCoroutine(SquareEffect(wave));

    }
    IEnumerator RoatateFlag(AnimationCurve curve)
    {
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(0.5f.Tweeng((p) => gameObject.transform.localEulerAngles = p, gameObject.transform.localEulerAngles + new Vector3(0, 0, 180), gameObject.transform.localEulerAngles));
        StartCoroutine(0.8f.Tweeng((p) => gameObject.transform.localScale = p, gameObject.transform.localScale, gameObject.transform.localScale*1.06f, curve));
    }
    IEnumerator SquareEffect(GameObject wave)
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(wave, gameObject.transform.position + new Vector3(0, 0, 0.5f), Quaternion.identity).transform.SetParent(gameObject.transform.parent);
        
    }
}
