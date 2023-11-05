using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Stage : MonoBehaviour
{
    public int curentLevel;
    public List<Button> btnLevel = new List<Button>();
    public List<Button> btnLevelCanPick = new List<Button>();
    GameObject currentBgBtn;
    public bool canClick = true;
    public void Start()
    {
        btnLevel = GetComponentsInChildren<Button>().ToList();
        for(int i = 0; i< btnLevel.Count; i++)
        {
            btnLevel[i].gameObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text= (i+1).ToString();
            if(i<= curentLevel)
            {
                btnLevelCanPick.Add(btnLevel[i]);
                if(i < curentLevel )
                {
                    btnLevel[i].gameObject.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (i == curentLevel)
                {

                        btnLevel[i].gameObject.transform.GetChild(1).gameObject.SetActive(false);
                        btnLevel[i].gameObject.transform.GetChild(2).gameObject.SetActive(true);
                        btnLevel[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);     
                }
                
                
            }
            else
            {
                btnLevel[i].gameObject.transform.GetChild(1).gameObject.SetActive(false);
                btnLevel[i].gameObject.transform.GetChild(3).gameObject.SetActive(true);
            }
           
        }
        if (curentLevel > btnLevel.Count - 1)
        {
            btnLevel[btnLevel.Count - 1].gameObject.transform.GetChild(1).gameObject.SetActive(true);
            btnLevel[btnLevel.Count - 1].gameObject.transform.GetChild(0).gameObject.SetActive(true);
            curentLevel = btnLevel.Count - 1;
        }

        //= GetComponentsInChildren<Button>();
    }
    public void btnClick()
    {
        if (canClick)
        {
            GameObject ClickButtonName = EventSystem.current.currentSelectedGameObject;
            foreach (Button btn in btnLevelCanPick)
            {
                if (btn.gameObject == ClickButtonName)
                {
                    if (currentBgBtn != null)
                    {
                        currentBgBtn.gameObject.SetActive(false);
                    }
                    currentBgBtn = btn.gameObject.transform.GetChild(0).gameObject;
                    currentBgBtn.gameObject.SetActive(true);
                    break;
                }
            }
        }
       
    }

}
