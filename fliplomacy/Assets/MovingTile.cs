using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTile : MonoBehaviour
{
    public GameObject MovingTileSprite;
    public int MovingTileGroupId = 0;

    public void SetUP()
    {
        Instantiate(MovingTileSprite, gameObject.transform.position, Quaternion.identity).transform.SetParent(gameObject.transform);
    }
}
