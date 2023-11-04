using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagChaningTile : MonoBehaviour
{
    [HideInInspector] public FloppyControll floppy;
    [HideInInspector] public int flagChaningType = 0;
    public GameObject FlagChaningSprite;
    GameObject flagChaning;
    Vector3 eular1;
    Vector3 eular2;

    public void SetUP()
    {
        floppy.EndJump += OnThingHappened;
        flagChaning = Instantiate(FlagChaningSprite, gameObject.transform.position, Quaternion.identity);
        flagChaning.transform.SetParent(gameObject.transform);
        eular1 = flagChaning.transform.localEulerAngles;
        if (flagChaningType == 1)
        {
            eular2 = flagChaning.transform.localEulerAngles + new Vector3(0, 0, -45);
            flagChaning.transform.localEulerAngles = eular2;
        }
        else
        {
            eular2 = flagChaning.transform.localEulerAngles + new Vector3(0, 0, -125);
            flagChaning.transform.localEulerAngles = eular2;
        }
        CheckDirectionWithFloppyFirstTime();
        
    }
    bool rightdirection = false;
    void OnThingHappened()
    {
        Debug.Log(231);
        float distance = Vector2.Distance(gameObject.transform.position, floppy.gameObject.transform.position);
        //Debug.Log("hehe" + distance);
        if (distance == 1 || distance == 0)
        {
            if (!rightdirection)
            {
                StartCoroutine(0.15f.Tweeng((p) => flagChaning.transform.localEulerAngles = p, eular2, eular1));
                rightdirection = true;
            }

        }
        else
        {
            if(rightdirection)
            {
                StartCoroutine(0.15f.Tweeng((p) => flagChaning.transform.localEulerAngles = p,eular1, eular2));
                rightdirection = false;
            }
            
        }
    }
    public void CheckDirectionWithFloppyFirstTime()
    {
        Debug.Log("Lew lew");
        float distance = Vector2.Distance(gameObject.transform.position, floppy.gameObject.transform.position);
        if (distance == 1 || distance == 0)
        {
            rightdirection = true;
            flagChaning.transform.localEulerAngles = eular1;
            Debug.Log("Lew lew 1");

        }
        else
        {
            rightdirection = false;
            flagChaning.transform.localEulerAngles = eular2;
            Debug.Log("Lew lew 2");
        }
    }
    //private List<Observer> _observers = new List<Observer>();

    //public void RegisterObserver(Observer observer)
    //{
    //    _observers.Add(observer);
    //}

    //public void UnregisterObserver(Observer observer)
    //{
    //    _observers.Remove(observer);
    //}

    //public void NotifyObservers()
    //{
    //    foreach (Observer observer in _observers)
    //    {
    //        observer.OnNotify();
    //    }
    //}
}
