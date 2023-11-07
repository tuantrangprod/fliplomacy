using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class CheckWinCondition : MonoBehaviour
{
    // Start is called before the first frame update
    FloppyControll floppy;
    [HideInInspector] public List<FlagTile> flagList;
    public Color color;
    public AnimationCurve curve;
    public GameObject squareWave;
    public UIManager uIManager;
    void Start()
    {
        floppy = GetComponent<GameManager>().Floppy.GetComponent<FloppyControll>();
      

    }
    public void RegisterEndJump()
    {
        floppy.EndJump += WinCondition;
    }

    void WinCondition()
    {
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
            StartCoroutine("LockFloppySwipe");
            WinAnim();
            StageManager.UnlockNewLevel();
            StageManager.LoadLevelData();

            floppy.EndJump -= WinCondition;
        }
    }
    public IEnumerator LockFloppySwipe()
    {
        yield return new WaitForSeconds(0.05f);
        floppy.canswipe = false;
    }

    public StageManager StageManager;
    
    void WinAnim()
    {
        StartCoroutine("WinGameAnimation");
    }
    public IEnumerator WinGameAnimation()
    {
        var w = true;
        while (w)
        {
            int Condition = 0; 
            for (int i = 0; i < flagList.Count; i++)
            {
                if (!flagList[i].rotatingFlag)
                {
                    Condition++;
                }
            }
            if (Condition == flagList.Count)
            {
                for (int i = 0; i < flagList.Count; i++)
                {
                    flagList[i].WinGameAnin("WinGameAnin" + i, color, curve, squareWave);
                }
                uIManager.WinGame();
                w = false;
            }

            yield return null;
        }
    }
    public void rePlayBtn()
    {
        StartCoroutine(1f.Tweeng((p) => Camera.main.transform.position = p, Camera.main.transform.position, Camera.main.transform.position + new Vector3(10, 0, 0)));
        StartCoroutine(ReLoadMenu());
    }
    public IEnumerator ReLoadMenu()
    {
        
        yield return new WaitForSeconds(1);
        StartCoroutine(ReLoadMainMenu());
    }
    public IEnumerator ReLoadMainMenu()
    {

        yield return new WaitForSeconds(1.5f);

        //SceneManager.LoadScene(0);
    }
}
