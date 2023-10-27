using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingTile : MonoBehaviour
{
    [HideInInspector] public GameObject DisappearingSprite;
    public void SetUP()
    {
        Instantiate(DisappearingSprite, gameObject.transform.position, Quaternion.identity).transform.SetParent(gameObject.transform);
    }
    public void HideObject()
    {
        gameObject.SetActive(false);
    }
}
