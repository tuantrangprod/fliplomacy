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
    //List<GameObject> FlagsObj = new List<GameObject>();

    public GameObject Disappearing;
    List<DisappearingTile> DisappearingObj = new List<DisappearingTile>();// dung cho Disappearing tile

    public GameObject Wormhole;
    List<GameObject> WormholeObj = new List<GameObject>();

    public GameObject FlagChaningDoc;
    //List<GameObject> FlagChaningDocObj = new List<GameObject>();

    public GameObject FlagChaningNgang;
    //List<GameObject> FlagChaningNgangObj = new List<GameObject>();

    public GameObject MovingTileSprite;
    List<GameObject> allMovingTiles = new List<GameObject>();

    public GameObject BombTileSprite;
    List<GameObject> allBombTile = new List<GameObject>();

    public GameObject BombMarketSprite;
    List<GameObject> allBombMarketTile = new List<GameObject>();


    [SerializeField]
    public struct AllCell
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
            //return typeTile;
        }
    }
    List<AllCell> allBombMarket = new List<AllCell>();
    List<AllCell> FlagsAllCell = new List<AllCell>();
    List<AllCell> MovingTilesAllCell = new List<AllCell>();
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


                if (_allCells[i, j].x == 0 && _allCells[i, j].y == 1)
                {
                    _allCells[i, j].typeTile = 6;
                }
                if (_allCells[i, j].x == 2 && _allCells[i, j].y == 2)
                {
                    _allCells[i, j].typeTile = 60;
                }
                if (_allCells[i, j].x == 2 && _allCells[i, j].y == 3)
                {
                    _allCells[i, j].typeTile = 60;
                }
                if (_allCells[i, j].x == 3 && _allCells[i, j].y == 2)
                {
                    _allCells[i, j].typeTile = 60;
                }
                if (_allCells[i, j].x == 3 && _allCells[i, j].y == 3)
                {
                    _allCells[i, j].typeTile = 60;
                }



                var tile = Instantiate(aCell, new Vector3(i, j, 0), quaternion.identity);
                tile.transform.SetParent(allCell);

                _allCells[i, j].ob = tile;

                if (_allCells[i, j].typeTile == 1)
                {
                    var tileFlag = tile.AddComponent<FlagTile>();
                    tileFlag.flag = Flags;
                    tileFlag.SetUP();
                    FlagsAllCell.Add(_allCells[i, j]);
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
                else if (_allCells[i, j].typeTile == 41)
                {
                    var tileFlagChaning = tile.AddComponent<FlagChaningTile>();
                    tileFlagChaning.FlagChaningSprite = FlagChaningDoc;
                    tileFlagChaning.flagChaningType = 1;
                    tileFlagChaning.SetUP();
                    //FlagChaningDocObj.Add(_allCells[i, j].ob);
                }
                else if (_allCells[i, j].typeTile == 42)
                {
                    var tileFlagChaning = tile.AddComponent<FlagChaningTile>();
                    tileFlagChaning.FlagChaningSprite = FlagChaningNgang;
                    tileFlagChaning.flagChaningType = 2;
                    tileFlagChaning.SetUP();
                    //FlagChaningNgangObj.Add(_allCells[i, j].ob);
                }
                else if (_allCells[i, j].typeTile == 5)
                {
                    var tileMoving = tile.AddComponent<MovingTile>();
                    tileMoving.MovingTileSprite = MovingTileSprite;
                    tileMoving.SetUP();
                    allMovingTiles.Add(_allCells[i, j].ob);
                    var movingtilegroup = gameObject.GetComponent<MovingTileGroup>();
                    movingtilegroup.allMovingTile = allMovingTiles;
                    movingtilegroup.SetUp();

                    MovingTilesAllCell.Add(_allCells[i, j]);
                    MovingtileGroup();
                   // _allCells[i, j].typeTile = 1;
                    //FlagChaningNgangObj.Add(_allCells[i, j].ob);
                }
                else if (_allCells[i, j].typeTile == 60)
                {
                    var tileBombMarker = tile.AddComponent<BombMarkedTile>();
                    tileBombMarker.BombMarkedSprite = BombMarketSprite;
                    //tileFlagChaning.flagChaningType = 2;
                    tileBombMarker.SetUP();
                    allBombMarketTile.Add(_allCells[i, j].ob);
                    //FlagChaningNgangObj.Add(_allCells[i, j].ob);
                }
                else if (_allCells[i, j].typeTile == 6)
                {
                    var tileBomb = tile.AddComponent<BombTile>();
                    tileBomb.BombSprite = BombTileSprite;
                    //tileFlagChaning.flagChaningType = 2;
                    tileBomb.SetUP();
                    allBombTile.Add(_allCells[i, j].ob);
                    //FlagChaningNgangObj.Add(_allCells[i, j].ob);
                }

            }
        }
        if (WormholeObj.Count != 0)
        {
            WormholeObj[0].GetComponent<WormholeTile>().ConnetedObject = WormholeObj[1].gameObject;
            WormholeObj[1].GetComponent<WormholeTile>().ConnetedObject = WormholeObj[0].gameObject;
        }
        if (allBombTile.Count != 0)
        {
            allBombTile[0].GetComponent<BombTile>().ConnetedBombMarked = allBombMarketTile;
        }
    }
    int CurrentStep = 2;
    bool goBack = false;
    public void MovingtileGroup()
    {

        for(int i = 0; i< MovingTilesAllCell.Count; i++)
        {
            if(i == CurrentStep)
            {
                MovingTilesAllCell[i].ob.SetActive(true);
                int x = MovingTilesAllCell[i].x;
                int y = MovingTilesAllCell[i].y;
                _allCells[x, y].typeTile = 0;
            }
            else
            {
                MovingTilesAllCell[i].ob.SetActive(false);
                int x = MovingTilesAllCell[i].x;
                int y = MovingTilesAllCell[i].y;
                _allCells[x, y].typeTile = 20;
            }
        }
    }
    public void MovingtileGroupAfterJump()
    {
        if (!goBack)
        {
          
            if (CurrentStep >= MovingTilesAllCell.Count - 1)
            {
                goBack = true;
                CurrentStep--;
            }
            else
            {
                CurrentStep++;
            }
            
        }
        else
        {
            if (CurrentStep <=  0)
            {
                goBack = false;
                CurrentStep++;
            }
            else
            {
                CurrentStep--;
            }
        }
        //if(floppyPosition.x == )
            MovingtileGroup();


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
            else if (_allCells[nextX, nextY].typeTile == 6)
            {
                FloppyJump(nextX, nextY);
                for(int i = 0; i< allBombMarketTile.Count; i++)
                {
                    allBombMarketTile[i].gameObject.SetActive(false);
                    int mx = (int) allBombMarketTile[i].transform.position.x ;
                    int my = (int)allBombMarketTile[i].transform.position.y;
                    _allCells[mx, my].typeTile = 20;

                }
                //DisappearingHideTileFunction(nextX, nextY, x, y, floppyPosition.x, floppyPosition.x);
                // cai nay dung trong truong hop muon nhay qua o da bi an
                // con khong thi khong lam gi ca
            }
            else if (_allCells[nextX, nextY].typeTile == 41)
            {
                FloppyJump(nextX, nextY);
                
                for (int i = 0; i< FlagsAllCell.Count; i++)
                {
                    if(FlagsAllCell[i].x == _allCells[nextX, nextY].x)
                    {
                        //Debug.Log(FlagsAllCell);
                        FlagsAllCell[i].ob.GetComponent<FlagTile>().ChangeFlag();
                    }
                }
            }
            else if (_allCells[nextX, nextY].typeTile == 42)
            {
                FloppyJump(nextX, nextY);
                //Debug.Log(_allCells[nextX, nextY].x +"   "+ _allCells[nextX, nextY].y);
                for (int i = 0; i < FlagsAllCell.Count; i++)
                {
                    if (FlagsAllCell[i].y == _allCells[nextX, nextY].y)
                    {
                        FlagsAllCell[i].ob.GetComponent<FlagTile>().ChangeFlag();
                    }
                }
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
        if(MovingTilesAllCell.Count > 0)
        {
            bool inMoving = false;
            if (floppyPosition.x == MovingTilesAllCell[CurrentStep].x && floppyPosition.y == MovingTilesAllCell[CurrentStep].y)
            {
                inMoving = true;
            }
            MovingtileGroupAfterJump();
            if (inMoving)
            {
                Floppy.transform.position = new Vector3(MovingTilesAllCell[CurrentStep].x, MovingTilesAllCell[CurrentStep].y, -1);
                floppyPosition.x = MovingTilesAllCell[CurrentStep].x;
                floppyPosition.y = MovingTilesAllCell[CurrentStep].y;
            }
        }
       
        //if()
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
