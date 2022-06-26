using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClickManager : MonoBehaviour
{

    [SerializeField] private UI_Move_DOTween[] loadObject;
    [SerializeField] private TextMeshProUGUI stegeText;
    [SerializeField] private TextMeshProUGUI clearTimeText;
    [SerializeField] private ClickManager startButton;
    public int stageNum;

    public void ClickQultButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
        Debug.Log(1);

    }

    public void StartButtonClick()
    {

        loadObject[0].Showing();
        loadObject[1].Disguise();
        loadObject[2].Disguise();
    }

    public void BackButtonClick()
    {

        loadObject[0].Disguise();
        loadObject[1].Showing();
        loadObject[2].Showing();

    }

    public void StartUI_BackButtonClick()
    {

        loadObject[0].Showing();
        loadObject[1].Disguise();

    }

    public void StartUI_StartButtonClick()
    {

        loadObject[0].Disguise();

    }

    public void TutorialButtonClick()
    {

        StartCoroutine(StageLoad());

    }

    public void StageButtonClick()
    {

        StageButton stageButton = GetComponent<StageButton>();

        clearTimeText.text = $"CLEAR TIME : {stageButton.clearTime}";
        stegeText.text = $"STAGE{stageButton.stageNum}";
        startButton.stageNum = stageButton.stageNum;

        Debug.Log(stageNum);

        loadObject[0].Showing();
        loadObject[1].Disguise();

    }

    public void StageStartButtonClick()
    {

        StartCoroutine(GameStart());

    }

    IEnumerator GameStart()
    {

        for (int i = 0; i < loadObject.Length; i++)
        {
            
            loadObject[i].Disguise();

        }

        yield return new WaitForSeconds(0.7f);

        
        PlayerPrefs.SetInt("StageNum", stageNum);
        SceneManager.LoadScene("Stage");

    }

    IEnumerator StageLoad()
    {

        for(int i = 0; i < loadObject.Length; i++)
        {
            loadObject[i].Disguise();

        }

        yield return new WaitForSeconds(0.7f);

        PlayerPrefs.SetInt("StageNum", 0);
        SceneManager.LoadScene("Stage");


    }

}
