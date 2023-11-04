using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWord : MonoBehaviour
{
    NameGameAnimation panelWord;
    public int ID;
    public float speedFlip;
    void Start()
    {
        
        
    }
    public void RegritEvent(GameObject obj)
    {
        panelWord = obj.GetComponent<NameGameAnimation>();
        speedFlip = panelWord.speedFlip;
        panelWord.EndAWord += OnThingHappened;

    }
    // Update is called once per frame
    void OnThingHappened()
    {
        if(panelWord.curentWord == ID)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(speedFlip.Tweeng((p) => gameObject.transform.localEulerAngles = p, gameObject.transform.localEulerAngles , gameObject.transform.localEulerAngles + new Vector3(0, -90,0 )));
            StartCoroutine(FlipWordXuoi());
        }
        if (panelWord.curentWord == panelWord.Word.Count + ID)
        {
            
            StartCoroutine(speedFlip.Tweeng((p) => gameObject.transform.localEulerAngles = p, gameObject.transform.localEulerAngles, gameObject.transform.localEulerAngles + new Vector3(0, -90, 0)));
            StartCoroutine(FlipWordNguoc());
        }
    }
    public IEnumerator FlipWordXuoi()
    {
        yield return new WaitForSeconds(speedFlip);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        StartCoroutine(speedFlip.Tweeng((p) => gameObject.transform.localEulerAngles = p, gameObject.transform.localEulerAngles, gameObject.transform.localEulerAngles + new Vector3(0, -90, 0)));
        StartCoroutine(EndMove());

    }
    public IEnumerator FlipWordNguoc()
    {
        yield return new WaitForSeconds(speedFlip);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);

        StartCoroutine(speedFlip.Tweeng((p) => gameObject.transform.localEulerAngles = p, gameObject.transform.localEulerAngles, gameObject.transform.localEulerAngles + new Vector3(0, -90, 0)));
        StartCoroutine(EndMove());

    }
    public IEnumerator EndMove()
    {
        yield return new WaitForSeconds(speedFlip);
        panelWord.curentWord++;
        if(panelWord.curentWord >= panelWord.Word.Count *2)
        {
            panelWord.curentWord = 0;
        }
        panelWord.EvenAwordAnim();
    }
}
