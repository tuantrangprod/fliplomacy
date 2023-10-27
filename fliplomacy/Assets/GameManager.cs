using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public GameObject aCell;
    public Transform allCell;
    public GameObject Floppy;

    public GameObject Flags;

    public GameObject Disappearing;
    List<DisappearingTile> DisappearingObj = new List<DisappearingTile>();// dung cho Disappearing tile

    public GameObject Wormhole;
    List<GameObject> WormholeObj = new List<GameObject>();

    [SerializeField]
    struct AllCell
    {
        public int x;
        public int y;
        public int typeTile;
        public GameObject ob;

        public AllCell(int x, int y, int typeTile, GameObject ob)
        {
            this.x = x;
            this.y = y;
            this.typeTile = typeTile;
            this.ob = ob;
        }
    }

    [SerializeField] private AllCell[,] _allCells;
    [SerializeField] private AllCell floppyPosition = new AllCell(0,0,0,null);
    void Start()
    {
        _allCells = new AllCell[5, 5];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                _allCells[i, j].x = i;
                _allCells[i, j].y = j;
                if (_allCells[i, j].x == 1 && _allCells[i, j].y == 1)
                {
                    _allCells[i, j].typeTile = 1;
                }
                if (_allCells[i, j].x == 1 && _allCells[i, j].y == 2)
                {
                    _allCells[i, j].typeTile = 1;
                }
                //if (_allCells[i, j].x == 4 && _allCells[i, j].y == 0)
                //{
                //    _allCells[i, j].typeTile = 1;
                //}
                //if (_allCells[i, j].x == 3 && _allCells[i, j].y == 1)
                //{
                //    _allCells[i, j].typeTile = 1;
                //}

                if (_allCells[i, j].x == 2 && _allCells[i, j].y == 2)
                {
                    _allCells[i, j].typeTile = 2;
                }

                if (_allCells[i, j].x == 0 && _allCells[i, j].y == 1)
                {
                    _allCells[i, j].typeTile = 3;
                }
                if (_allCells[i, j].x == 3 && _allCells[i, j].y == 3)
                {
                    _allCells[i, j].typeTile = 3;
                }
                var tile = Instantiate(aCell, new Vector3(i, j, 0), quaternion.identity);
                tile.transform.SetParent(allCell);

                _allCells[i, j].ob = tile;

                if (_allCells[i, j].typeTile == 1)
                {
                    var tileFlag = tile.AddComponent<FlagTile>();
                    tileFlag.flag = Flags;
                    tileFlag.SetUP();
                }
                else if (_allCells[i, j].typeTile == 2)
                {
                    var tileDisappearing = tile.AddComponent<DisappearingTile>();
                    tileDisappearing.DisappearingSprite = Disappearing;
                    tileDisappearing.SetUP();
                }
                else if (_allCells[i, j].typeTile == 3)
                {
                    var tileWormhole = tile.AddComponent<WormholeTile>();
                    tileWormhole.WormholeSprite = Wormhole;
                    tileWormhole.SetUP();
                    WormholeObj.Add(_allCells[i, j].ob);
                }
               
            }
        }
        if (WormholeObj.Count != 0)
        {
            WormholeObj[0].GetComponent<WormholeTile>().ConnetedObject = WormholeObj[1].gameObject;
            WormholeObj[1].GetComponent<WormholeTile>().ConnetedObject = WormholeObj[0].gameObject;
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
        List<GameObject> flagsInStep = new List<GameObject>();// dung cho flag tile

        if (nextX < 5 && nextX >= 0 && nextY >= 0 && nextY < 5)// && _allCells[nextX, nextY].typeTile != 20)
        {
            if(_allCells[nextX, nextY].typeTile == 1) // flagtile
            {
                FlagTileFunction(nextX, nextY, flagsInStep, x, y, floppyPosition.x, floppyPosition.x);

            }
            else if (_allCells[nextX, nextY].typeTile == 2)
            {
                FloppyJump(nextX, nextY);
                _allCells[nextX, nextY].typeTile = 20;
                var disappearingTileFunc = _allCells[nextX, nextY].ob.GetComponent<DisappearingTile>();
                DisappearingObj.Add(disappearingTileFunc);
            }
            else if (_allCells[nextX, nextY].typeTile == 20)
            {
                //DisappearingHideTileFunction(nextX, nextY, x, y, floppyPosition.x, floppyPosition.x);
                // cai nay dung trong truong hop muon nhay qua o da bi an
                // con khong thi khong lam gi ca
            }
            else
            {
                FloppyJump(nextX, nextY);
            }
        } 

    }
    public void FlagTileFunction(int nextX, int nextY, List<GameObject> flagsInStep,
        int x, int y, int startFloppyPositionx, int startFloppyPositiony)
    {
        if (_allCells[nextX, nextY].typeTile == 1) // flagtile
        {
            flagsInStep.Add(_allCells[nextX, nextY].ob);
            nextX += x;
            nextY += y;
            if(_allCells[nextX, nextY].typeTile == 1)
            {
                FlagTileFunction(nextX, nextY, flagsInStep, x, y, startFloppyPositionx, startFloppyPositiony);
            }
            else if(_allCells[nextX, nextY].typeTile == 20)
            {

            }
            else if (_allCells[nextX, nextY].typeTile == 2)
            {
                FloppyJump(nextX, nextY);
                _allCells[nextX, nextY].typeTile = 20;
                var disappearingTileFunc = _allCells[nextX, nextY].ob.GetComponent<DisappearingTile>();
                DisappearingObj.Add(disappearingTileFunc);
            }
            else if(nextX < 5 && nextX >= 0 && nextY >= 0 && nextY < 5)
            {
                FloppyJump(nextX, nextY);

                for(int i = 0; i < flagsInStep.Count; i++)
                {
                    var flagFunc = flagsInStep[i].GetComponent<FlagTile>();
                    flagFunc.ChangeFlag();

                }
                //endstep
            }
            else
            {
                FloppyJump(startFloppyPositionx, startFloppyPositiony);
                //endstep
            }
        }
    }
    public void DisappearingHideTileFunction(int nextX, int nextY,
        int x, int y, int startFloppyPositionx, int startFloppyPositiony)
    {
        if (_allCells[nextX, nextY].typeTile == 20) // DisappearingHideTile
        {
            nextX += x;
            nextY += y;
            if (_allCells[nextX, nextY].typeTile == 20)
            {
                DisappearingHideTileFunction(nextX, nextY, x, y, startFloppyPositionx, startFloppyPositiony);
            }
            else if (nextX < 5 && nextX >= 0 && nextY >= 0 && nextY < 5)
            {
                FloppyJump(nextX, nextY);
                //endstep
            }
            else
            {
                FloppyJump(startFloppyPositionx, startFloppyPositiony);
                //endstep
            }
        }
    }
    public void FloppyJump(int nextX, int nextY)
    {
        floppyPosition.x = nextX;
        floppyPosition.y = nextY;
        Floppy.transform.position = new Vector3(floppyPosition.x, floppyPosition.y, -1);
        HideDisappearingTileAfterJumpOut();

        if(_allCells[nextX, nextY].typeTile == 3)
        {
            var conneted = _allCells[nextX, nextY].ob.GetComponent<WormholeTile>().ConnetedObject.transform.position;
            Floppy.transform.position = new Vector3(conneted.x, conneted.y, -1);
            floppyPosition.x = (int)conneted.x;
            floppyPosition.y = (int)conneted.y;

        }
    }

    public void HideDisappearingTileAfterJumpOut()
    {
        if(DisappearingObj.Count > 0)
        {
            DisappearingObj[0].HideObject();
            DisappearingObj.Clear();
        }
    }

}
