using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public UI BackGround;
    public UI Name;
    public UI ButtonPanel;
    public UI PlayButton;
    public UI BackBtn;
    public UI StageManager;

    private void Start()
    {
        BackGround.SetUp();
        Name.SetUp();
        ButtonPanel.SetUp();



        PlayButton.SetUp();
        BackBtn.SetUp();
        StageManager.SetUp();

        PlayButton.TeleToEndPos();
        BackBtn.TeleToEndPos();
        StageManager.TeleToEndPos();
    }
    public void ClickPlayBtn()
    {
        BackGround.MoveToEndPos();
        Name.MoveToEndPos();
        ButtonPanel.MoveToEndPos();
        StartCoroutine(WaitAfterClickPlayBtn());
    }
    public void ClickBackBtn()
    {
        PlayButton.MoveToEndPos();
        BackBtn.MoveToEndPos();
        StageManager.MoveToEndPos();
        StartCoroutine(WaitAfterClickBackBtnn());
    }
    public IEnumerator WaitAfterClickPlayBtn()
    {
        yield return new WaitForSeconds(0.8f);
        PlayButton.BackToStartPos();
        BackBtn.BackToStartPos();
        StageManager.BackToStartPos();
    }
    public IEnumerator WaitAfterClickBackBtnn()
    {
        yield return new WaitForSeconds(0.8f);
        BackGround.BackToStartPos();
        Name.BackToStartPos();
        ButtonPanel.BackToStartPos();
    }
     
    public void ClickLoadGameBtn()
    {
        PlayButton.MoveToEndPos();
        BackBtn.MoveToEndPos();
        StageManager.MoveToEndPos();
    }
}
