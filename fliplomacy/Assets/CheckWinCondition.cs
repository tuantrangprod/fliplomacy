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

    public StageManager StageManager;
    
    void WInAnim()
    {
        //var allcell = GetComponent<GameManager>().allCell;
        for (int i = 0; i < flagList.Count; i++)
        {
            flagList[i].WinGameAnin("WinGameAnin" + i, color, curve, squareWave);
        }
        uIManager.WinGame();

        StageManager.UnlockNewLevel();
        StageManager.LoadLevelData();
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
