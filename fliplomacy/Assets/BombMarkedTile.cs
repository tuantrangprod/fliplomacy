using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMarkedTile : MonoBehaviour
{
    [HideInInspector] public GameObject BombMarkedSprite;
    public void SetUP()
    {
        Instantiate(BombMarkedSprite, gameObject.transform.position, Quaternion.identity).transform.SetParent(gameObject.transform);
    }
}
