using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTile : MonoBehaviour
{
    [HideInInspector] public GameObject BombSprite;
    [HideInInspector] public List<GameObject> ConnetedBombMarked = new List<GameObject>();
    public void SetUP()
    {
        Instantiate(BombSprite, gameObject.transform.position, Quaternion.identity).transform.SetParent(gameObject.transform);
    }
}
