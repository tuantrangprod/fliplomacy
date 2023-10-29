using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagChaningTile : MonoBehaviour
{
    [HideInInspector] public int flagChaningType = 0;
    public GameObject FlagChaningSprite;

    public void SetUP()
    {
        Instantiate(FlagChaningSprite, gameObject.transform.position, Quaternion.identity).transform.SetParent(gameObject.transform);
    }
}
