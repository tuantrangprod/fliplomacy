using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinCondition : MonoBehaviour
{
    // Start is called before the first frame update
    FloppyControll floppy;
    [HideInInspector] public List<FlagTile> flagList;
    public Color color;
    public AnimationCurve curve;
    public GameObject squareWave;
    void Start()
    {
        floppy = GetComponent<GameManager>().Floppy.GetComponent<FloppyControll>();
        floppy.EndJump += WinCondition;

    }
    public void RegisterEndJump()
    {
        
    }

    void WinCondition()
    {
        Debug.Log(123);
        int Condition = 0;
        for (int i = 0; i < flagList.Count; i++)
        {
            if(flagList[i].flagStatus == 1)
            {
                Condition++;
            }
        }
        if(Condition == flagList.Count)
        {
            Debug.Log("Win");
            floppy.canswipe = false;
            WInAnim();
        }
    }
    void WInAnim()
    {
        for (int i = 0; i < flagList.Count; i++)
        {
            flagList[i].WinGameAnin("WinGameAnin" + i, color, curve, squareWave);
        }
    }
}
