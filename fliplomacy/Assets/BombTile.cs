using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTile : MonoBehaviour
{
    public GameObject BombSprite;
    public List<GameObject> ConnetedBombMarked = new List<GameObject>();
    public void SetUP()
    {
        Instantiate(BombSprite, gameObject.transform.position, Quaternion.identity).transform.SetParent(gameObject.transform);
    }
}
