using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    
    //public UI MainMenuPanel;
    public UI BackGround;
    public UI Name;
    public UI ButtonPanel;
    //public UI SelectLevelPanel;
    public UI PlayButton;
    public UI BackBtn;
    public UI StageManager;

    public UI backSelectLevelPanel;
    public UI inGameButonPanel;

    public UI winGameButonPanel;
    public GameManager gameManager;

    private void Start()
    {
        //MainMenuPanel
        BackGround.SetUp();
        Name.SetUp();
        ButtonPanel.SetUp();


        //SelectLevelPanel
        PlayButton.SetUp();
        BackBtn.SetUp();
        StageManager.SetUp();

        PlayButton.TeleToEndPos();
        BackBtn.TeleToEndPos();
        StageManager.TeleToEndPos();

        backSelectLevelPanel.SetUp();
        inGameButonPanel.SetUp();

        backSelectLevelPanel.TeleToEndPos();
        inGameButonPanel.TeleToEndPos();

        winGameButonPanel.SetUp();
        winGameButonPanel.TeleToEndPos();
    }
    public void LoadInGameBtn()
    {
        backSelectLevelPanel.BackToStartPos();
        inGameButonPanel.BackToStartPos();
    }
    public void ClickBackSelectLevelBtn()
    {
        backSelectLevelPanel.MoveToEndPos();
        inGameButonPanel.MoveToEndPos();
        StartCoroutine(WaitAfterClickBackSelectLevelBtn());

        gameManager.ClearLevelHaveAnim();

    }
    public IEnumerator WaitAfterClickBackSelectLevelBtn()
    {
        yield return new WaitForSeconds(0.8f);
        //SelectLevelPanel show
        PlayButton.BackToStartPos();
        BackBtn.BackToStartPos();
        StageManager.BackToStartPos();
    }
    public void ClickToReloadGameScene()
    {
        gameManager.ClearLevelAndReloadSceneHaveAnim();
        backSelectLevelPanel.MoveToEndPos();
        inGameButonPanel.MoveToEndPos();
    }
    public void ClickToReloadGameSceneWhenWinGame()
    {
        gameManager.ClearLevelAndReloadSceneHaveAnim();
        winGameButonPanel.MoveToEndPos();
    }
    public void WinGame()
    {
        backSelectLevelPanel.MoveToEndPos();
        inGameButonPanel.MoveToEndPos();
        StartCoroutine(WaitWinGame());
    }
    public IEnumerator WaitWinGame()
    {
        yield return new WaitForSeconds(0.8f);
        //SelectLevelPanel show
        winGameButonPanel.BackToStartPos();
    }
    public void ClickBackLevelChoseBtn()
    {
        winGameButonPanel.MoveToEndPos();
        StartCoroutine(WaitClickBackLevelChoseBtn());
        gameManager.ClearLevelHaveAnim();

    }
    
    public IEnumerator WaitClickBackLevelChoseBtn()
    {
        yield return new WaitForSeconds(0.8f);
        //SelectLevelPanel show
        PlayButton.BackToStartPos();
        BackBtn.BackToStartPos();
        StageManager.BackToStartPos();

    }


    public void ClickOpenLevelSelectButton()
    {
        //MainMenuPanel hide
        BackGround.MoveToEndPos();
        Name.MoveToEndPos();
        ButtonPanel.MoveToEndPos();
        StartCoroutine(WaitAfterClickOpenLevelSelectButton());
    }
    public IEnumerator WaitAfterClickOpenLevelSelectButton()
    {
        yield return new WaitForSeconds(0.8f);
        //SelectLevelPanel show
        PlayButton.BackToStartPos();
        BackBtn.BackToStartPos();
        StageManager.BackToStartPos();
    }
    public void ClickBackToMainMenuBtn()
    {
        //SelectLevelPanel hide
        PlayButton.MoveToEndPos();
        BackBtn.MoveToEndPos();
        StageManager.MoveToEndPos();
        StartCoroutine(WaitAfterClickBackToMainMenuBtn());
    }
   
    public IEnumerator WaitAfterClickBackToMainMenuBtn()
    {
        yield return new WaitForSeconds(0.8f);
        //MainMenuPanel show
        BackGround.BackToStartPos();
        Name.BackToStartPos();
        ButtonPanel.BackToStartPos();
    }
     
    public void ClickLoadGameBtn()
    {
        //SelectLevelPanel hide
        PlayButton.MoveToEndPos();
        BackBtn.MoveToEndPos();
        StageManager.MoveToEndPos();
    }


    public void LoadLevelScene()
    {
        StartCoroutine(WaitLoadLevelScene());
    }
    public IEnumerator WaitLoadLevelScene()
    {
        yield return new WaitForSeconds(1f);
        PlayButton.BackToStartPos();
        BackBtn.BackToStartPos();
        StageManager.BackToStartPos();
    }
}
