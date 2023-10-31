using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCellDate : MonoBehaviour
{
    public List<ACell> cellsData = new List<ACell>();
    
    public void Save()
    {
        cellsData.Clear();
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            cellsData.Add(gameObject.transform.GetChild(i).gameObject.GetComponent<ACell>());
        }
       
    }
    //if (_allCells[i, j].x == 1 && _allCells[i, j].y == 1)
    //{
    //    _allCells[i, j].typeTile = 1;
    //}
    //if (_allCells[i, j].x == 1 && _allCells[i, j].y == 2)
    //{
    //    _allCells[i, j].typeTile = 1;
    //}
    //if (_allCells[i, j].x == 4 && _allCells[i, j].y == 0)
    //{
    //    _allCells[i, j].typeTile = 1;
    //}
    //if (_allCells[i, j].x == 3 && _allCells[i, j].y == 1)
    //{
    //    _allCells[i, j].typeTile = 1;
    //}


    //if (_allCells[i, j].x == 1 && _allCells[i, j].y == 4)
    //{
    //    _allCells[i, j].typeTile = 41;
    //}
    //if (_allCells[i, j].x == 2 && _allCells[i, j].y == 0)
    //{
    //    _allCells[i, j].typeTile = 42;
    //}



    //if (_allCells[i, j].x == 2 && _allCells[i, j].y == 2)
    //{
    //    _allCells[i, j].typeTile = 2;
    //}


    //if (_allCells[i, j].x == 0 && _allCells[i, j].y == 1)
    //{
    //    _allCells[i, j].typeTile = 3;
    //}
    //if (_allCells[i, j].x == 3 && _allCells[i, j].y == 3)
    //{
    //    _allCells[i, j].typeTile = 3;
    //}

    //if (_allCells[i, j].x == 1 && _allCells[i, j].y == 2)
    //{
    //    _allCells[i, j].typeTile = 5;
    //}
    //if (_allCells[i, j].x == 2 && _allCells[i, j].y == 2)
    //{
    //    _allCells[i, j].typeTile = 5;
    //}
    //if (_allCells[i, j].x == 3 && _allCells[i, j].y == 2)
    //{
    //    _allCells[i, j].typeTile = 5;
    //}
    //if (_allCells[i, j].x == 4 && _allCells[i, j].y == 2)
    //{
    //    _allCells[i, j].typeTile = 5;
    //}


    //if (_allCells[i, j].x == 0 && _allCells[i, j].y == 1)
    //{
    //    _allCells[i, j].typeTile = 6;
    //}
    //if (_allCells[i, j].x == 2 && _allCells[i, j].y == 2)
    //{
    //    _allCells[i, j].typeTile = 60;
    //}
    //if (_allCells[i, j].x == 2 && _allCells[i, j].y == 3)
    //{
    //    _allCells[i, j].typeTile = 60;
    //}
    //if (_allCells[i, j].x == 3 && _allCells[i, j].y == 2)
    //{
    //    _allCells[i, j].typeTile = 60;
    //}
    //if (_allCells[i, j].x == 3 && _allCells[i, j].y == 3)
    //{
    //    _allCells[i, j].typeTile = 60;
    //}


}
