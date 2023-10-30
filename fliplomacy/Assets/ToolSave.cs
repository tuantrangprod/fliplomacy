using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ToolSave : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject allCell;
    public List<ACell> cells = new List<ACell>();

    public GameObject Flags;
    public GameObject Disappearing;
    public GameObject Wormhole;
    public GameObject FlagChaningDoc;
    public GameObject FlagChaningNgang;
    public GameObject MovingTileSprite;
    public GameObject BombTileSprite;
    public GameObject BombMarketSprite;

    public GameObject selectedPanel;
    private Camera cam;


    void Start()
    {
        cam = Camera.main;
        //print(cam.name);
        for (int i = 0; i < allCell.transform.childCount; i++)
        {
            cells.Add((allCell.transform.GetChild(i).gameObject.GetComponent<ACell>()));
        }
    }

    private GameObject objInRay1;
    private GameObject objInRay2;

    public Color color1;
    public Color color2;
    public Color color3;
    public GameObject selectedCell;
    public GameObject selectedCellTemp;
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(cam.transform.position, mousePos - cam.transform.position, Color.blue);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool rayCastDown = Physics.Raycast(ray, out hit, 100);
        if (rayCastDown)
        {
           objInRay1 = hit.transform.gameObject;
          
            if (objInRay1 != objInRay2) //|| (objInRay1 == null && objInRay2 == null))
            {
                if (objInRay1 != selectedCell)
                {
                    objInRay1.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color2;
                }
                

                
                if (objInRay2 != null && objInRay2 != selectedCell)
                {
                    objInRay2.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color1;
                }

                objInRay2 = objInRay1;
            }
        }
        else
        {
            if (objInRay1 != null && objInRay1 != selectedCell)
            {
                objInRay1.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color1;
            }

            objInRay1 = null;

            if (objInRay2 != null && objInRay2 != selectedCell)
            {
                objInRay2.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color1;
            }

            objInRay2 = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //selectedCell = null;
            if (objInRay1 != null)
            {
                selectedCell = objInRay1;
            }
            
            if (selectedCell != null)
            {
                selectedCell.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color3;
                if (selectedCellTemp != selectedCell)
                {
                    if (selectedCellTemp != null)
                    {
                        selectedCellTemp.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color1;
 
                    }
                    selectedCellTemp = selectedCell;
                }
            }
            selectedPanel.gameObject.SetActive(true);
        }
    }

    public void OnFlagCilck()
    {
        selectedCell.GetComponent<ACell>().cellID = 1;
        if (selectedCell.transform.childCount > 1)
        {
            Destroy(selectedCell.transform.GetChild(1).gameObject);
            Instantiate(Flags,selectedCell.transform.position,quaternion.identity ).transform.SetParent(selectedCell.transform);
        }
        else
        {
            Instantiate(Flags,selectedCell.transform.position,quaternion.identity ).transform.SetParent(selectedCell.transform);
        }
    }
    public void OnDisappearingCilck()
    {
        selectedCell.GetComponent<ACell>().cellID = 2;
        if (selectedCell.transform.childCount > 1)
        {
            Destroy(selectedCell.transform.GetChild(1).gameObject);
            Instantiate(Disappearing,selectedCell.transform.position,quaternion.identity ).transform.SetParent(selectedCell.transform);
        }
        else
        {
            Instantiate(Disappearing,selectedCell.transform.position,quaternion.identity ).transform.SetParent(selectedCell.transform);
        }
    }
    public void OnWormholeCilck()
    {
        selectedCell.GetComponent<ACell>().cellID = 3;
        if (selectedCell.transform.childCount > 1)
        {
            Destroy(selectedCell.transform.GetChild(1).gameObject);
            Instantiate(Wormhole,selectedCell.transform.position,quaternion.identity ).transform.SetParent(selectedCell.transform);
        }
        else
        {
            Instantiate(Wormhole,selectedCell.transform.position,quaternion.identity ).transform.SetParent(selectedCell.transform);
        }
    }
    public void OnFlafChangingCilck()
    {
        selectedCell.GetComponent<ACell>().cellID = 41;
        if (selectedCell.transform.childCount > 1)
        {
            Destroy(selectedCell.transform.GetChild(1).gameObject);
            Instantiate(FlagChaningDoc,selectedCell.transform.position,quaternion.identity ).transform.SetParent(selectedCell.transform);
        }
        else
        {
            Instantiate(FlagChaningNgang,selectedCell.transform.position,quaternion.identity ).transform.SetParent(selectedCell.transform);
        }
    }
    public void OnMovingCilck()
    {
        selectedCell.GetComponent<ACell>().cellID = 5;
        if (selectedCell.transform.childCount > 1)
        {
            Destroy(selectedCell.transform.GetChild(1).gameObject);
            Instantiate(MovingTileSprite,selectedCell.transform.position,quaternion.identity ).transform.SetParent(selectedCell.transform);
        }
        else
        {
            Instantiate(MovingTileSprite,selectedCell.transform.position,quaternion.identity ).transform.SetParent(selectedCell.transform);
        }
    }
    public void OnBombCilck()
    {
        selectedCell.GetComponent<ACell>().cellID = 6;
        if (selectedCell.transform.childCount > 1)
        {
            Destroy(selectedCell.transform.GetChild(1).gameObject);
            Instantiate(BombTileSprite,selectedCell.transform.position,quaternion.identity ).transform.SetParent(selectedCell.transform);
        }
        else
        {
            Instantiate(BombTileSprite,selectedCell.transform.position,quaternion.identity ).transform.SetParent(selectedCell.transform);
        }
    }
    public void OnBombMarkedCilck()
    {
        selectedCell.GetComponent<ACell>().cellID = 60;
        if (selectedCell.transform.childCount > 1)
        {
            Destroy(selectedCell.transform.GetChild(1).gameObject);
            Instantiate(BombMarketSprite,selectedCell.transform.position,quaternion.identity ).transform.SetParent(selectedCell.transform);
        }
        else
        {
            Instantiate(BombMarketSprite,selectedCell.transform.position,quaternion.identity ).transform.SetParent(selectedCell.transform);
        }
    }

    public void OnNoneTileClick()
    {
        
    }
}
