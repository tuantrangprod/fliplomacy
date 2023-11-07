using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class CellsManager : MonoBehaviour
{
    public AnimationCurve curve;
    public FloppyControll floppy;
    public List<GameObject> otherObject;
    public List<GameObject> flagObject;

    public UIManager uIManager;


    public void setUp()
    {
        for (int i = 1; i < gameObject.transform.childCount; i++)
        {
            if(gameObject.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                if (gameObject.transform.GetChild(i).name != "Flag")
                {
                    otherObject.Add(gameObject.transform.GetChild(i).gameObject);
                }
                else
                {
                    flagObject.Add(gameObject.transform.GetChild(i).gameObject);
                }
            }
           
        }
        DoStartAnim();
        
    }
    public void ClearLevel()
    {
        for (int i = 1; i < gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
            otherObject.Clear();
            flagObject.Clear();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene(0);
            //DoStartAnim();
            //Debug.Log(1);
        }
    }
    void DoStartAnim()
    {
        Debug.Log(otherObject.Count);
        for (int i = 0; i < otherObject.Count; i++)
        {

            var cell = otherObject[i].AddComponent<aCellsManager>();
            var time = Random.Range(0.5f, 2f);
            cell.StartAnim(time, curve);
        }
        for (int i = 0; i < flagObject.Count; i++)
        {

            var cell = flagObject[i].AddComponent<aCellsManager>();
            var time = Random.Range(2f, 3f);
            cell.StartAnim(time, curve);
        }
        
        var fcell = floppy.floopySprite.AddComponent<aCellsManager>();
        var ftime = Random.Range(2f, 3f);
        fcell.StartAnim(ftime, curve);
        StartCoroutine(EndStartAnim(ftime + 0.2f));

    }
    IEnumerator EndStartAnim(float time)
    {
        yield return new WaitForSeconds(time);
        uIManager.LoadInGameBtn();
        floppy.StartIdelAnim();
        floppy.canswipe = true;
    }

    public GameManager GameManager;
    //public void CloseGameScene()
    //{
    //    GameManager.ClearLevel();
    //}
    
    //public GameObject a;
    //public Transform ponitA;
    //public Transform pointB;
    //void Start()
    //{

    //}

    //// Update is called once per frame

    //if (Input.GetKeyDown(KeyCode.Y))
    //{
    //    StartCoroutine(TweengMove(1, pointB.position, ponitA.position));
    //}

    //}
    //public IEnumerator TweengMove(float duration, Vector3 aa, Vector3 zz)
    //{
    //    float sT = Time.time;
    //    float eT = sT + duration;

    //    while (Time.time < eT)
    //    {
    //        float t = (Time.time - sT) / duration;
    //        a.transform.position = Vector3.Lerp(aa, zz, curve.Evaluate(Mathf.SmoothStep(0f, 1f, t)));
    //        yield return null;
    //    }

    //    a.transform.position = zz;
    //}
}
