using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagTile : MonoBehaviour
{
    [HideInInspector] public GameObject flag;
    int flagStatus = 0;
    public void SetUP()
    {
        Instantiate(flag, gameObject.transform.position, Quaternion.identity).transform.SetParent(gameObject.transform);
    }
    public void ChangeFlag()
    {
        var flag = gameObject.transform.GetChild(1);
        if (flagStatus == 0)
        {
            flagStatus = 1;
            flag.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            flag.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            flagStatus = 0;
            flag.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            flag.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
