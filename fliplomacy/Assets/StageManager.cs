using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class StageManager : MonoBehaviour
{
    public List<Stage> stages = new List<Stage>();
    public List<Object> Level = new List<Object>();
    public UIManager uiManager;
    public GameObject backStage;
    public GameObject nextStage;
    public string levelUnlock;
    public int CurrentStage = 0;
    public bool canBack = false;
    public bool canNext = true;
    public bool canClick = true;
    public float speedFlip;
    public Object levelSelect;
    void Start()
    {
        levelUnlock = PlayerPrefs.GetString("UnlockLevel");
        try
        {
            var a = Int32.Parse(levelUnlock[0].ToString());
        }
        catch
        {
            levelUnlock = "00";
        }
        //levelUnlock = "00";
        //PlayerPrefs.SetString("UnlockLevel", levelUnlock);
        var temp = Resources.LoadAll("LevelDesign", typeof(GameObject));
        Level = temp.ToList();
        stages = GetComponentsInChildren<Stage>().ToList();
       
        checkCanBackOrNext();
        LevelUnlock();
        
        var sumLevelHavePrefab = Level.Count;
        var numberLevel = 0;
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
            for (int j = 0; j < numberLevel; j++)
            {
                stages[i].level.Add(Level[0]);
                Level.Remove(Level[0]);
            }
            stages[i].LoadLevel();
        }

        CheckHaveUnlock();
    }
    public int stageId;
    public int stageLevelUnlock;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UnlockNewLevel();
           
            LoadLevelData();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            CheckHaveUnlock();
        }
    }

    public void LevelUnlock()
    {
        stageId = Int32.Parse(levelUnlock[0].ToString());
        try
        {
            stageLevelUnlock = Int32.Parse(levelUnlock[1].ToString()) * 10 + Int32.Parse(levelUnlock[2].ToString());
        }
        catch
        {
            stageLevelUnlock = Int32.Parse(levelUnlock[1].ToString()); 
        }
    }

    public void UnlockNewLevel()
    {
        if (UnlockIfWin)
        {
            stageLevelUnlock++;
            if (stageLevelUnlock >= stages[stageId].btnLevel.Count)
            {
                stageId++;
                stageLevelUnlock = 0;

            }
            levelUnlock = stageId.ToString() + stageLevelUnlock.ToString();
            PlayerPrefs.SetString("UnlockLevel", levelUnlock);
        }
      

    }
    public void LoadLevelData()
    {
        LevelUnlock();
        for (int i = 0; i < stages.Count; i++)
        {
            if (i == stageId)
            {
                stages[i].curentLevel = stageLevelUnlock;
            }
            else if(i < stageId)
            {
                stages[i].curentLevel = stages[i].btnLevel.Count;
            }
            else
            {
                stages[i].curentLevel = -1;
            }
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

    public GameManager gameManager;
    public bool UnlockIfWin = true;
    public void LoadLevelToScene()
    {
        if (stages[CurrentStage].CurentSelect >= 0)
        {
            levelSelect = stages[CurrentStage].level[stages[CurrentStage].CurentSelect];
            Debug.Log(stages[CurrentStage].level[stages[CurrentStage].CurentSelect].GetComponent<AllCellDate>());
            gameManager.LevelSave = levelSelect;
            gameManager.CreateLevel();
            CheckHaveUnlock();
            uiManager.ClickLoadGameBtn();

        }
    }

    public void CheckHaveUnlock()
    {
        if (stageId == CurrentStage)
        {
            Debug.Log("WInCanUnLock" + "  "+ stageLevelUnlock + "  "+ stages[CurrentStage].CurentSelect);
            
            if (stageLevelUnlock == stages[CurrentStage].CurentSelect)
            {
                UnlockIfWin = true;
            }
            else
            {
                UnlockIfWin = false;
            }
        }
        else
        {
            UnlockIfWin = false;
        }
    }
}
