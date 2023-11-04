using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject WordPanel;
    public GameObject BtnPanel;
    public void ClickPlayBtn()
    {
        StartCoroutine(1f.Tweeng((p) => WordPanel.transform.position = p, WordPanel.transform.position, WordPanel.transform.position + new Vector3(-2000, 0, 0)));
        StartCoroutine(1f.Tweeng((p) => BtnPanel.transform.position = p, BtnPanel.transform.position, BtnPanel.transform.position + new Vector3(0, -1000, 0)));
        StartCoroutine(LoadNewGame());
    }
    public IEnumerator LoadNewGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
        
    }
}
