using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public List<Stage> stages = new List<Stage>();
    public List<Object> Level = new List<Object>();
    public UIManager uiManager;
    public GameObject backStage;
    public GameObject nextStage;
    public int levelUnlock = 0;
    public int CurrentStage = 0;
    public bool canBack = false;
    public bool canNext = true;
    public bool canClick = true;
    public float speedFlip;
    void Start()
    {
        var temp = Resources.LoadAll("LevelDesign", typeof(GameObject));
        Level = temp.ToList();
        stages = GetComponentsInChildren<Stage>().ToList();
       
        checkCanBackOrNext();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            levelUnlock++;
            LoadLevelData();
        }
       
    }
    void LoadLevelData()
    {
        var sumLevelHavePrefab = Level.Count;
        var numberLevel = 0;

        var sumlevelUnlock = levelUnlock;
        var curentLevel = 0;
        for (int i = 0; i < stages.Count; i++)
        {
            if(sumLevelHavePrefab > 9)
            {
                numberLevel = 9;
                sumLevelHavePrefab -= 9;
            }
            else
            {
                numberLevel = sumLevelHavePrefab;
                sumLevelHavePrefab = 0;
            }
            stages[i].numberLevel = numberLevel;

            if (levelUnlock > 9)
            {
                curentLevel = 9;
                sumlevelUnlock -= 9;
            }
            else
            {
                curentLevel = sumlevelUnlock;
                sumlevelUnlock = 0;
            }
            stages[i].curentLevel = curentLevel;
            stages[i].LoadLevel();
        }
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
        StartCoroutine(speedFlip.Tweeng((p) => stages[CurrentStage + num ].aStage.transform.localEulerAngles = p, stages[CurrentStage + num].aStage.transform.localEulerAngles, stages[CurrentStage + num].aStage.transform.localEulerAngles + new Vector3(-90, 0, 0)));
        stages[CurrentStage].aStage.transform.localEulerAngles += new Vector3(-90, 0, 0);
        yield return new WaitForSeconds(speedFlip);
        stages[CurrentStage + num].aStage.gameObject.SetActive(false);
        stages[CurrentStage + num].aStage.transform.localEulerAngles += new Vector3(90, 0, 0);
        stages[CurrentStage].aStage.gameObject.SetActive(true);
        StartCoroutine(speedFlip.Tweeng((p) => stages[CurrentStage].aStage.transform.localEulerAngles = p, stages[CurrentStage].aStage.transform.localEulerAngles, stages[CurrentStage].aStage.transform.localEulerAngles + new Vector3(90, 0, 0)));
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
    public void clickLoadGameBtn()
    {
        uiManager.ClickLoadGameBtn();
    }
    public void ClickLoadLevelBtn()
    {
        LoadLevelData();
        for (int i = 0; i < stages.Count; i++)
        {
            if (i != CurrentStage)
            {
                stages[i].aStage.gameObject.SetActive(false);
            }
        }
    }
}
