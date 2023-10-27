using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormholeTile : MonoBehaviour
{
    [HideInInspector] public GameObject WormholeSprite;
    public GameObject ConnetedObject;
    public void SetUP()
    {
        Instantiate(WormholeSprite, gameObject.transform.position, Quaternion.identity).transform.SetParent(gameObject.transform);
    }
}
