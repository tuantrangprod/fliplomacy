using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public List<Stage> stages = new List<Stage>();
    public GameObject backStage;
    public GameObject nextStage;
    public int CurrentStage = 0;
    public bool canBack = false;
    public bool canNext = true;
    public bool canClick = true;
    public float speedFlip;
    void Start()
    {
        stages = GetComponentsInChildren<Stage>().ToList();
        for(int i = 0; i< stages.Count; i++)
        {
            if(i != CurrentStage)
            {
                stages[i].gameObject.SetActive(false);
            }
        }
        checkCanBackOrNext();
    }

    public void backStageCick()
    {
        if (canBack && canClick)
        {
            CurrentStage--;
            checkCanBackOrNext();
            DoAnim(1);
        }
    }
    public void nextStageCick()
    {
        if (canNext && canClick)
        {
            CurrentStage++;
            checkCanBackOrNext();
            DoAnim(-1);
        }
    }
    public void checkCanBackOrNext()
    {
        if(CurrentStage == 0)
        {
             canBack = false;
             canNext = true;

            backStage.transform.GetChild(0).gameObject.SetActive(false);
            backStage.transform.GetChild(1).gameObject.SetActive(true);

            nextStage.transform.GetChild(0).gameObject.SetActive(true);
            nextStage.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if(CurrentStage == stages.Count -1)
        {
            canBack = true;
            canNext = false;

            backStage.transform.GetChild(0).gameObject.SetActive(true);
            backStage.transform.GetChild(1).gameObject.SetActive(false);

            nextStage.transform.GetChild(0).gameObject.SetActive(false);
            nextStage.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            canBack = true;
            canNext = true;

            backStage.transform.GetChild(0).gameObject.SetActive(true);
            backStage.transform.GetChild(1).gameObject.SetActive(false);

            nextStage.transform.GetChild(0).gameObject.SetActive(true);
            nextStage.transform.GetChild(1).gameObject.SetActive(false);
        }
        
    }
    public void DoAnim( int num)
    {
        foreach (Stage stage in stages)
        {
            stage.canClick = false;
        }
        canClick = false;
        StartCoroutine(DoFlipAnim(num));
    }
    public IEnumerator DoFlipAnim(int num)
    {
        StartCoroutine(speedFlip.Tweeng((p) => stages[CurrentStage + num ].transform.localEulerAngles = p, stages[CurrentStage + num].transform.localEulerAngles, stages[CurrentStage + num].transform.localEulerAngles + new Vector3(-90, 0, 0)));
        stages[CurrentStage].transform.localEulerAngles += new Vector3(-90, 0, 0);
        yield return new WaitForSeconds(speedFlip);
        stages[CurrentStage + num].gameObject.SetActive(false);
        stages[CurrentStage + num].transform.localEulerAngles += new Vector3(90, 0, 0);
        stages[CurrentStage].gameObject.SetActive(true);
        StartCoroutine(speedFlip.Tweeng((p) => stages[CurrentStage].transform.localEulerAngles = p, stages[CurrentStage].transform.localEulerAngles, stages[CurrentStage].transform.localEulerAngles + new Vector3(90, 0, 0)));
        StartCoroutine(WaitFlipEnd());
    }
    public IEnumerator WaitFlipEnd()
    {
        yield return new WaitForSeconds(speedFlip);
        foreach (Stage stage in stages)
        {
            stage.canClick = true;
        }
        canClick = true;
    }
}
