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

    public GameObject btnRePlay;
    public GameObject bg;
    public GameObject btn;
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
        StartCoroutine(1f.Tweeng((p) => btnRePlay.transform.position = p, btnRePlay.transform.position, btnRePlay.transform.position + new Vector3(0, 350, 0),curve));
    }
    public void rePlayBtn()
    {
        var a = btnRePlay.transform.GetChild(0).GetComponent<Button>();
        Destroy(a);
        StartCoroutine(1f.Tweeng((p) => Camera.main.transform.position = p, Camera.main.transform.position, Camera.main.transform.position + new Vector3(10, 0, 0)));
        StartCoroutine(ReLoadMenu());
    }
    public IEnumerator ReLoadMenu()
    {
        
        yield return new WaitForSeconds(1);
        StartCoroutine(2f.Tweeng((p) => btnRePlay.transform.position = p, btnRePlay.transform.position, btnRePlay.transform.position - new Vector3(0, 350, 0), curve));
        StartCoroutine(2f.Tweeng((p) => btn.transform.position = p, btn.transform.position, btn.transform.position + new Vector3(0, 350, 0), curve));
        StartCoroutine(1f.Tweeng((p) => bg.transform.position = p, bg.transform.position, bg.transform.position + new Vector3(2000, 0, 0)));

        StartCoroutine(ReLoadMainMenu());
    }
    public IEnumerator ReLoadMainMenu()
    {

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(0);
    }
}
