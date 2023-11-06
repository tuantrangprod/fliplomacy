using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Object = UnityEngine.Object;

public class Stage : MonoBehaviour
{
    public int numberLevel;
    public int curentLevel;
   
    public List<Button> btnLevel = new List<Button>();
    public List<Button> btnLevelUnLock = new List<Button>();
    public List<Button> btnLevelCanPick = new List<Button>();

    public List<Object> level;
    
    GameObject currentBgBtn;
    [HideInInspector] public GameObject aStage;
    public bool canClick = true;

    public int CurentSelect = -1;
    public void Start()
    {
        aStage = gameObject.transform.GetChild(0).gameObject;
        btnLevel = GetComponentsInChildren<Button>().ToList();
        for (int i = 0; i < btnLevel.Count; i++)
        {
            btnLevel[i].gameObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
        }
        //object a;
        //GameObject b = a;
        //LoadLevel();
    }
    public void LoadLevel()
    {
        Debug.Log(numberLevel);
        btnLevelUnLock = new List<Button>();
        for (int i = 0; i < btnLevel.Count; i++)
        {
            if (i > numberLevel - 1)
            {
                btnLevel[i].gameObject.transform.GetChild(1).gameObject.SetActive(false);
                btnLevel[i].gameObject.transform.GetChild(4).gameObject.SetActive(false);
                btnLevel[i].gameObject.transform.GetChild(5).gameObject.SetActive(true);
            }
            else
            {
                btnLevelUnLock.Add(btnLevel[i]);
            }
        }
        btnLevelCanPick.Clear();
        for (int i = 0; i < btnLevelUnLock.Count; i++)
        {

            if (i <= curentLevel)
            {
                btnLevelCanPick.Add(btnLevelUnLock[i]);
                if (i < curentLevel)
                {
                    btnLevelUnLock[i].gameObject.transform.GetChild(1).gameObject.SetActive(true);

                    btnLevelUnLock[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    btnLevelUnLock[i].gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    btnLevelUnLock[i].gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    btnLevelUnLock[i].gameObject.transform.GetChild(5).gameObject.SetActive(false);
                }
                else if (i == curentLevel)
                {

                    CurentSelect = i;
                    btnLevelUnLock[i].gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    currentBgBtn = btnLevelUnLock[i].gameObject.transform.GetChild(0).gameObject;
                    currentBgBtn.gameObject.SetActive(true);

                    btnLevelUnLock[i].gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    btnLevelUnLock[i].gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    btnLevelUnLock[i].gameObject.transform.GetChild(5).gameObject.SetActive(false);

                }


            }
            else
            {
                btnLevelUnLock[i].gameObject.transform.GetChild(1).gameObject.SetActive(false);
                btnLevelUnLock[i].gameObject.transform.GetChild(3).gameObject.SetActive(true);


                btnLevelUnLock[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                btnLevelUnLock[i].gameObject.transform.GetChild(2).gameObject.SetActive(false);
                btnLevelUnLock[i].gameObject.transform.GetChild(5).gameObject.SetActive(false);
            }

        }
        if (curentLevel > btnLevelUnLock.Count - 1 && btnLevelUnLock.Count > 0)
        {
            btnLevelUnLock[btnLevelUnLock.Count - 1].gameObject.transform.GetChild(1).gameObject.SetActive(true);
            btnLevelUnLock[btnLevelUnLock.Count - 1].gameObject.transform.GetChild(0).gameObject.SetActive(true);
            btnLevelUnLock[btnLevelUnLock.Count - 1].gameObject.transform.GetChild(2).gameObject.SetActive(false);
            btnLevelUnLock[btnLevelUnLock.Count - 1].gameObject.transform.GetChild(3).gameObject.SetActive(false);
            btnLevelUnLock[btnLevelUnLock.Count - 1].gameObject.transform.GetChild(5).gameObject.SetActive(false);

            curentLevel = btnLevelUnLock.Count - 1;
        }

        //= GetComponentsInChildren<Button>();
    }
    public void btnClick()
    {
        if (canClick)
        {
            GameObject ClickButtonName = EventSystem.current.currentSelectedGameObject;
            for (int i = 0; i < btnLevelCanPick.Count; i++)
            {
                if (btnLevelCanPick[i].gameObject == ClickButtonName)
                {
                    if (currentBgBtn != null)
                    {
                        currentBgBtn.gameObject.SetActive(false);
                    }

                    CurentSelect = i;
                    currentBgBtn = btnLevelCanPick[i].gameObject.transform.GetChild(0).gameObject;
                    currentBgBtn.gameObject.SetActive(true);
                    break;
                }
            }
            // foreach (Button btn in btnLevelCanPick)
            // {
            //     if (btn.gameObject == ClickButtonName)
            //     {
            //        
            //     }
            // }
        }
       
    }

}
