using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public GameObject aCell;
    public Transform allCell;
    public GameObject Floppy;
    
    
    struct AllCell
    {
        public int x;
        public int y;
        public bool haveFlag;

        public AllCell(int x, int y,bool haveFlag)
        {
            this.x = x;
            this.y = y;
            this.haveFlag = haveFlag;
        }
    }

    private AllCell[,] _allCells;
    private AllCell floppyPosition = new AllCell(0,0, false);
    void Start()
    {
        _allCells = new AllCell[5, 5];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                _allCells[i, j].x = i;
                _allCells[i, j].y = j;
                Instantiate(aCell, new Vector3(i, j, 0), quaternion.identity).transform.SetParent(allCell);
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnSwipeLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnSwipeRight();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnSwipeTop();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnSwipeBottom();
        }
    }
    private void OnSwipeLeft() {
        Debug.Log("Left");
        FloppyMove(-1,0);
        //ShowText.text = "Left";
    }

    private void OnSwipeRight() {
        Debug.Log("Right");
        FloppyMove(1, 0);
        //ShowText.text = "Right";

    }

    private void OnSwipeTop() {
        Debug.Log("Top");
        FloppyMove(0,1);
        //ShowText.text = "Top";
    }

    private void OnSwipeBottom() {
        Debug.Log("Down");
        FloppyMove(0,-1);
        //ShowText.text = "Botton";
    }

    private void FloppyMove(int x, int y)
    {
        var nextX = floppyPosition.x + x;
        var nextY = floppyPosition.y + y;
        if (nextX < 5 && nextX >= 0 && nextY >= 0 && nextY < 5)
        {
            floppyPosition.x = nextX;
            floppyPosition.y = nextY;
            Floppy.transform.position = new Vector3(floppyPosition.x, floppyPosition.y, -1);//-1 la vi tri de floppy luon o tren
        } 

    }

}
