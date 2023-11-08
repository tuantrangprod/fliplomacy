using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTileGroup : MonoBehaviour
{
    public AnimationCurve Curve;
    public GameObject MovingTileSprite;
    public List<GameObject> allMovingTile = new List<GameObject>();
    public GameObject[] arrayTile = new GameObject[5];
    public int CurrentStep = 1;
    public int UporBack = 1;


    GameObject movingTile;

    public void SetUp()
    {
        for(int i = 0; i< arrayTile.Length; i++)
        {
            if(arrayTile[i] != null)
            {
                //Instantiate(MovingTileSprite, arrayTile[i].transform.position, Quaternion.identity).transform.SetParent(arrayTile[i].transform);
                arrayTile[i].gameObject.SetActive(false);
                if (i == CurrentStep)
                {
                    movingTile = Instantiate(MovingTileSprite, arrayTile[i].transform.position, Quaternion.identity);
                    movingTile.transform.SetParent(gameObject.transform.parent.transform.parent);
                }
            }
        }
    }
    public void MovingTileTravel()
    {
        StartCoroutine(0.2f.Tweeng((p) => movingTile.transform.position = p,
          movingTile.transform.position,
           arrayTile[CurrentStep].transform.position,Curve));
    }
    public void ClearLevel()
    {
        allMovingTile.Clear();
        arrayTile = new GameObject[5];
        CurrentStep = 1;
        UporBack = 1;
        movingTile = null;

    }
}
