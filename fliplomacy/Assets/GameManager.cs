using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public List<Color> WormholeColor = new List<Color>();
    public List<float> WormholeSpeed= new List<float>();
    List<GameObject> WormholeObj = new List<GameObject>();

    public GameObject FlagChaningDoc;
    //List<GameObject> FlagChaningDocObj = new List<GameObject>();

    public GameObject FlagChaningNgang;
    //List<GameObject> FlagChaningNgangObj = new List<GameObject>();

    public GameObject MovingTileSprite;
    public List<MovingTileGroup> movingTileGroups = new List<MovingTileGroup>();
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
        public string typeTile;
        public GameObject ob;

        public AllCell(int x, int y, string typeTile, GameObject ob)
        {
            this.x = x;
            this.y = y;
            this.typeTile = typeTile;
            this.ob = ob;
            //return typeTile;
        }
    }
    List<AllCell> WormholeAllCell = new List<AllCell>();
    List<AllCell> allBomb = new List<AllCell>();
    List<AllCell> allBombMarked = new List<AllCell>();
    List<AllCell> FlagsAllCell = new List<AllCell>();
    List<AllCell> MovingTilesAllCell = new List<AllCell>();

   

    [SerializeField] private AllCell[,] _allCells;
    [SerializeField] private AllCell floppyPosition = new AllCell(0,0,"0",null);

    public GameObject LevelSave;
    void Start()
    {
        _allCells = new AllCell[5, 5];
        var cellsdata = LevelSave.GetComponent<AllCellDate>().cellsData;
        for (int i = 0; i < cellsdata.Count; i++)
        {
            _allCells[cellsdata[i].idX, cellsdata[i].idY].typeTile = cellsdata[i].cellID;
        }
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                _allCells[i, j].x = i;
                _allCells[i, j].y = j;
                
                var tile = Instantiate(aCell, new Vector3(i, j, 0), quaternion.identity);
                tile.transform.SetParent(allCell);

                _allCells[i, j].ob = tile;

                if (_allCells[i, j].typeTile == "1")
                {
                    var tileFlag = tile.AddComponent<FlagTile>();
                    tileFlag.flag = Flags;
                    tileFlag.SetUP();
                    FlagsAllCell.Add(_allCells[i, j]);
                }
                else if (_allCells[i, j].typeTile == "2")
                {
                    var tileDisappearing = tile.AddComponent<DisappearingTile>();
                    tileDisappearing.DisappearingSprite = Disappearing;
                    tileDisappearing.SetUP();
                }
                else if (_allCells[i, j].typeTile[0] == '3')
                {
                    var tileWormhole = tile.AddComponent<WormholeTile>();
                    tileWormhole.WormholeSprite = Wormhole;
                   // tileWormhole.SetUP();
                    WormholeObj.Add(_allCells[i, j].ob);
                    WormholeAllCell.Add(_allCells[i, j]);
                }
                else if (_allCells[i, j].typeTile == "40")
                {
                    var tileFlagChaning = tile.AddComponent<FlagChaningTile>();
                    tileFlagChaning.FlagChaningSprite = FlagChaningDoc;
                    tileFlagChaning.flagChaningType = 1;
                    tileFlagChaning.SetUP();
                    //FlagChaningDocObj.Add(_allCells[i, j].ob);
                }
                else if (_allCells[i, j].typeTile == "41")
                {
                    var tileFlagChaning = tile.AddComponent<FlagChaningTile>();
                    tileFlagChaning.FlagChaningSprite = FlagChaningNgang;
                    tileFlagChaning.flagChaningType = 2;
                    tileFlagChaning.SetUP();
                    //FlagChaningNgangObj.Add(_allCells[i, j].ob);
                }
                else if (_allCells[i, j].typeTile[0] == '5')
                {
                    MovingTilesAllCell.Add(_allCells[i, j]);
                   

                    //var tileMoving = tile.AddComponent<MovingTile>();
                    //tileMoving.MovingTileSprite = MovingTileSprite;
                    //tileMoving.SetUP();
                    //allMovingTiles.Add(_allCells[i, j].ob);
                    //var movingtilegroup = gameObject.GetComponent<MovingTileGroup>();
                    //movingtilegroup.allMovingTile = allMovingTiles;
                    //movingtilegroup.SetUp();
                    //MovingtileGroup();
                    // _allCells[i, j].typeTile = 1;
                    //FlagChaningNgangObj.Add(_allCells[i, j].ob);
                }
                else if (_allCells[i, j].typeTile[0] == '6')
                {
                    var tileBomb = tile.AddComponent<BombTile>();
                    tileBomb.BombSprite = BombTileSprite;
                    //tileFlagChaning.flagChaningType = 2;
                    tileBomb.SetUP();
                    allBomb.Add(_allCells[i, j]);
                    
                    //FlagChaningNgangObj.Add(_allCells[i, j].ob);
                }
                else if (_allCells[i, j].typeTile[0] == '7')
                {
                    
                    var tileBombMarker = tile.AddComponent<BombMarkedTile>();
                    tileBombMarker.BombMarkedSprite = BombMarketSprite;
                    //tileFlagChaning.flagChaningType = 2;
                    tileBombMarker.SetUP();
                    allBombMarked.Add(_allCells[i, j]);
                    //FlagChaningNgangObj.Add(_allCells[i, j].ob);
                }
                else if (_allCells[i, j].typeTile == "20")
                {
                    _allCells[i, j].ob.gameObject.SetActive(false);
                }

            }
        }
       
        

        if (WormholeObj.Count != 0)
        {
            for(int i = 0;i < WormholeAllCell.Count; i++)
            {
                var Wormhole1 = WormholeAllCell[i];
                for (int j = i+1; j < WormholeAllCell.Count; j++)
                {
                    var Wormhole2 = WormholeAllCell[j];
                    if (Wormhole1.typeTile[1] == Wormhole2.typeTile[1])
                    {
                        var w1tile = Wormhole1.ob.GetComponent<WormholeTile>();
                        w1tile.ConnetedObject = Wormhole2.ob;
                        w1tile.mainHole = true;
                        var _color = WormholeColor[UnityEngine.Random.Range(0, WormholeColor.Count)];
                        w1tile.color = _color;
                        WormholeColor.Remove(_color);
                        var whSpeed = UnityEngine.Random.Range(0, WormholeSpeed.Count);
                        w1tile.rotateSpeed = WormholeSpeed[whSpeed];
                        WormholeSpeed.Remove(WormholeSpeed[whSpeed]);


                        var w2tile = Wormhole2.ob.GetComponent<WormholeTile>();
                        w2tile.ConnetedObject = Wormhole1.ob;
                        w2tile.mainHole = false;


                        w2tile.SetUP();
                        w1tile.SetUP();
                        
                    }
                }
            }
            //WormholeObj[0].GetComponent<WormholeTile>().ConnetedObject = WormholeObj[1].gameObject;
            //WormholeObj[1].GetComponent<WormholeTile>().ConnetedObject = WormholeObj[0].gameObject;
        }
        if (MovingTilesAllCell.Count != 0)
        {
            //movingTileGroups
            //int d = Int32.Parse(c);
            for (int i = 0; i < MovingTilesAllCell.Count; i++)
            {
                var movingTileID = Int32.Parse(MovingTilesAllCell[i].typeTile[4].ToString());
                var movingTileStt = Int32.Parse(MovingTilesAllCell[i].typeTile[5].ToString());
                //movingTileGroups[movingTileID].allMovingTile.Add(MovingTilesAllCell[i].ob);
                movingTileGroups[movingTileID].arrayTile[movingTileStt] = MovingTilesAllCell[i].ob;
                movingTileGroups[movingTileID].CurrentStep = Int32.Parse(MovingTilesAllCell[i].typeTile[2].ToString());
                movingTileGroups[movingTileID].UporBack = Int32.Parse(MovingTilesAllCell[i].typeTile[3].ToString());


            }
            for(int i = 0; i< movingTileGroups.Count; i++)
            {
                //movingTileGroups[i].CurrentStep = arrayTile[0]
                movingTileGroups[i].MovingTileSprite = MovingTileSprite;
                movingTileGroups[i].SetUp();
                for(int j = 0; j < movingTileGroups[i].arrayTile.Length; j++)
                {
                    if(movingTileGroups[i].arrayTile[j] != null)
                    {
                       int x = (int)movingTileGroups[i].arrayTile[j].transform.position.x;
                       int y = (int)movingTileGroups[i].arrayTile[j].transform.position.y;
                        if (j != movingTileGroups[i].CurrentStep)
                        {
                            _allCells[x, y].typeTile = 20.ToString();
                        }
                        else
                        {
                            _allCells[x, y].typeTile = 5.ToString();
                        }
                    }
                }
            }
            

            ////550104
            ///string id = "5" + numberOfList.text + (Int32.Parse(ShowPosition.text) - 1).ToString() + MovingDirectionUporBack.value.ToString() + MovingTileID.ToString() + i.ToString();
            //WormholeObj[1].GetComponent<WormholeTile>().ConnetedObject = WormholeObj[0].gameObject;
        }
        List<MovingTileGroup> listOut = (from element in movingTileGroups

                                         where element.arrayTile[0] != null

                                         select (MovingTileGroup)element).ToList();
        movingTileGroups = new List<MovingTileGroup>(listOut);
        if(movingTileGroups.Count > 0)
        {
            Floppy.GetComponent<FloppyControll>().haveMovingTile = true;
        }

        if (allBombMarked.Count != 0)
        {
            for(int i = 0; i < allBombMarked.Count; i++)
            {
                var BombId = Int32.Parse(allBombMarked[i].typeTile[1].ToString());
                for (int j = 0; j < allBomb.Count; j++)
                {
                    var bombBombId = Int32.Parse(allBomb[j].typeTile[1].ToString());
                    if (bombBombId == BombId)
                    {
                        allBomb[j].ob.GetComponent<BombTile>().ConnetedBombMarked.Add(allBombMarked[i].ob);
                    }
                }
            }
            allBombTile[0].GetComponent<BombTile>().ConnetedBombMarked = allBombMarketTile;
        }
    }
    public void MovingtileGroup()
    {

        for (int i = 0; i < movingTileGroups.Count; i++)
        {
            //movingTileGroups[i].MovingTileSprite = MovingTileSprite;
            //movingTileGroups[i].SetUp();
            for (int j = 0; j < movingTileGroups[i].arrayTile.Length; j++)
            {
                if (movingTileGroups[i].arrayTile[j] != null)
                {
                    //movingTileGroups[i].MovingTileTravel();
                    StartCoroutine(WaitFloppyEndJump(movingTileGroups[i]));
                    int x = (int)movingTileGroups[i].arrayTile[j].transform.position.x;
                    int y = (int)movingTileGroups[i].arrayTile[j].transform.position.y;
                    if (j != movingTileGroups[i].CurrentStep)
                    {
                        _allCells[x, y].typeTile = 20.ToString();
                        //movingTileGroups[i].arrayTile[j].gameObject.SetActive(false);
                    }
                    else
                    {
                        _allCells[x, y].typeTile = 5.ToString();
                        //movingTileGroups[i].arrayTile[j].gameObject.SetActive(true);
                    }
                }
            }
        }
    }
    public IEnumerator WaitFloppyEndJump(MovingTileGroup mvt)
    {
        yield return new WaitForSeconds(0.2f);
        mvt.MovingTileTravel();
    }
    public void MovingtileGroupAfterJump()
    {
        for (int i = 0; i < movingTileGroups.Count; i++)
        {
            GameObject[] arrayOut = (from element in movingTileGroups[i].arrayTile

                            where element != null

                              select (GameObject)element).ToArray();
            if (movingTileGroups[i].UporBack == 0) // neu dang tien len
            {
                if(movingTileGroups[i].CurrentStep >= arrayOut.Length-1) // set xem co phai dang o muc lon nhat co the tien len khong
                {
                    movingTileGroups[i].UporBack = 1; // neu co thi phai lui lai
                    movingTileGroups[i].CurrentStep--;
                }
                else
                {
                    movingTileGroups[i].CurrentStep++; // neu khong tiep tuc tien len
                }
            }
            else if (movingTileGroups[i].UporBack == 1) // neu dang lui xuong
            {
                if (movingTileGroups[i].CurrentStep <= 0) // set xem co phai dang o muc nho nhat co the lui xuong khong
                {
                    movingTileGroups[i].UporBack = 0; // neu co thi phai tien len
                    movingTileGroups[i].CurrentStep++;
                }
                else
                {
                    movingTileGroups[i].CurrentStep--; // neu khong tiep tuc lui xuong
                }
            }

        }
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
    [HideInInspector] public string swipeDirection;
    public void OnSwipeLeft() {
        // Debug.Log("Left");
        swipeDirection = "Left";
        FloppyMove(-1,0);
        //ShowText.text = "Left";
    }

    public void OnSwipeRight() {
        //Debug.Log("Right");
        swipeDirection = "Right";
        FloppyMove(1, 0);
        //ShowText.text = "Right";

    }

    public void OnSwipeTop() {
        //Debug.Log("Top");
        swipeDirection = "Top";
        FloppyMove(0,1);
        //ShowText.text = "Top";
    }

    public void OnSwipeBottom() {
        //Debug.Log("Down");
        swipeDirection = "Down";
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
            if(_allCells[nextX, nextY].typeTile == 1.ToString()) // flagtile
            {
               FlagTileFunction(nextX, nextY, flagsInStep, x, y, floppyPosition.x, floppyPosition.x);

            }
        else if (_allCells[nextX, nextY].typeTile == 2.ToString())
        {
            FloppyJump(nextX, nextY);
            _allCells[nextX, nextY].typeTile = 20.ToString();
            var disappearingTileFunc = _allCells[nextX, nextY].ob.GetComponent<DisappearingTile>();
            DisappearingObj.Add(disappearingTileFunc);
        }
        else if (_allCells[nextX, nextY].typeTile == 20.ToString())
        {
            //DisappearingHideTileFunction(nextX, nextY, x, y, floppyPosition.x, floppyPosition.x);
            // cai nay dung trong truong hop muon nhay qua o da bi an
            // con khong thi khong lam gi ca
        }
        else if (_allCells[nextX, nextY].typeTile[0] == '6')
        {
            FloppyJump(nextX, nextY);
            var connetedBombMarked = _allCells[nextX, nextY].ob.GetComponent<BombTile>().ConnetedBombMarked;
            for(int i = 0; i< connetedBombMarked.Count; i++)
            {
                    connetedBombMarked[i].GetComponent<BombMarkedTile>().Hiding();
                int mx = (int) connetedBombMarked[i].transform.position.x ;
                int my = (int)connetedBombMarked[i].transform.position.y;
                _allCells[mx, my].typeTile = 20.ToString();
        
            }
            //DisappearingHideTileFunction(nextX, nextY, x, y, floppyPosition.x, floppyPosition.x);
            // cai nay dung trong truong hop muon nhay qua o da bi an
            // con khong thi khong lam gi ca
        }
        else if (_allCells[nextX, nextY].typeTile == 40.ToString())
        {
            FloppyJump(nextX, nextY);
                
            for (int i = 0; i< FlagsAllCell.Count; i++)
            {
                if(FlagsAllCell[i].x == _allCells[nextX, nextY].x)
                {
                    //Debug.Log(FlagsAllCell);
                    FlagsAllCell[i].ob.GetComponent<FlagTile>().ChangeFlag("Top");
                }
            }
        }
        else if (_allCells[nextX, nextY].typeTile == 41.ToString())
        {
            FloppyJump(nextX, nextY);
            //Debug.Log(_allCells[nextX, nextY].x +"   "+ _allCells[nextX, nextY].y);
            for (int i = 0; i < FlagsAllCell.Count; i++)
            {
                if (FlagsAllCell[i].y == _allCells[nextX, nextY].y)
                {
                    FlagsAllCell[i].ob.GetComponent<FlagTile>().ChangeFlag("Left");
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
        if (_allCells[nextX, nextY].typeTile == 1.ToString()) // flagtile
        {
            flagsInStep.Add(_allCells[nextX, nextY].ob);
            nextX += x;
            nextY += y;
            if(_allCells[nextX, nextY].typeTile == 1.ToString())
            {
                FlagTileFunction(nextX, nextY, flagsInStep, x, y, startFloppyPositionx, startFloppyPositiony);
            }
            else if(_allCells[nextX, nextY].typeTile == 20.ToString())
            {

            }
            else if (_allCells[nextX, nextY].typeTile == 2.ToString())
            {
                FloppyJump(nextX, nextY);
                _allCells[nextX, nextY].typeTile = 20.ToString();
                var disappearingTileFunc = _allCells[nextX, nextY].ob.GetComponent<DisappearingTile>();
                DisappearingObj.Add(disappearingTileFunc);
            }
            else if(nextX < 5 && nextX >= 0 && nextY >= 0 && nextY < 5)
            {
                FloppyJump(nextX, nextY);

                for(int i = 0; i < flagsInStep.Count; i++)
                {
                    var flagFunc = flagsInStep[i].GetComponent<FlagTile>();
                    flagFunc.ChangeFlag(swipeDirection);

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
        if (_allCells[nextX, nextY].typeTile == 20.ToString()) // DisappearingHideTile
        {
            nextX += x;
            nextY += y;
            if (_allCells[nextX, nextY].typeTile == 20.ToString())
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

        if(_allCells[nextX, nextY].typeTile[0] == '3')
        {
            var conneted = _allCells[nextX, nextY].ob.GetComponent<WormholeTile>().ConnetedObject.transform.position;
            
            Floppy.transform.position = new Vector3(conneted.x, conneted.y, -1);
            Floppy.GetComponent<FloppyControll>().TeleAnim(floppyPosition.x, floppyPosition.y);
            Floppy.GetComponent<FloppyControll>().FloppyInWormHole = true;
            floppyPosition.x = (int)conneted.x;
            floppyPosition.y = (int)conneted.y;
        }
        if(movingTileGroups.Count > 0)
        {
            bool inMoving = false;
            var mtg = new MovingTileGroup();
            for (int i = 0; i < movingTileGroups.Count; i++)
            {
                int tileX = (int)movingTileGroups[i].arrayTile[movingTileGroups[i].CurrentStep].transform.position.x;
                int tileY = (int)movingTileGroups[i].arrayTile[movingTileGroups[i].CurrentStep].transform.position.y;

                if (floppyPosition.x == tileX && floppyPosition.y == tileY)
                {
                    inMoving = true;
                    Floppy.GetComponent<FloppyControll>().InMovingTile = true;
                    mtg = movingTileGroups[i];
                    Floppy.GetComponent<FloppyControll>().movingTilePos = new Vector3(mtg.arrayTile[mtg.CurrentStep].transform.position.x, mtg.arrayTile[mtg.CurrentStep].transform.position.y, 0);
                }
            }
            MovingtileGroupAfterJump();
            if (inMoving)
            {
                
                Floppy.transform.position = new Vector3(mtg.arrayTile[mtg.CurrentStep].transform.position.x, mtg.arrayTile[mtg.CurrentStep].transform.position.y, -1);

                
                

                floppyPosition.x = (int)mtg.arrayTile[mtg.CurrentStep].transform.position.x;
                floppyPosition.y = (int)mtg.arrayTile[mtg.CurrentStep].transform.position.y;
            }
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
