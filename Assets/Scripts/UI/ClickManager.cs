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

    private AudioSource click;
    public int stageNum;

    void Awake()
    {
        
        click = GameObject.Find("ClickSound").GetComponent<AudioSource>();

    }

    public void ClickQultButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();

        click.Play();

    }

    public void StartButtonClick()
    {

        loadObject[0].Showing();
        loadObject[1].Disguise();
        loadObject[2].Disguise();
        click.Play();

    }

    public void BackButtonClick()
    {

        loadObject[0].Disguise();
        loadObject[1].Showing();
        loadObject[2].Showing();
        click.Play();
    }

    public void StartUI_BackButtonClick()
    {

        loadObject[0].Showing();
        loadObject[1].Disguise();
        click.Play();
    }

    public void StartUI_StartButtonClick()
    {

        loadObject[0].Disguise();
        click.Play();
    }

    public void TutorialButtonClick()
    {

        StartCoroutine(StageLoad());
        click.Play();
    }

    public void StageButtonClick()
    {
        click.Play();
        StageButton stageButton = GetComponent<StageButton>();

        clearTimeText.text = $"CLEAR TIME : {string.Format("{0:0.#0}", stageButton.clearTime)}";
        stegeText.text = $"STAGE{stageButton.stageNum}";
        startButton.stageNum = stageButton.stageNum;

        loadObject[0].Showing();
        loadObject[1].Disguise();

    }

    public void StageStartButtonClick()
    {

        StartCoroutine(GameStart());
        click.Play();
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

    public void RestartButtonClick()
    {

        click.Play();
        StartCoroutine(Restart());

    }
    public void Stage_End_BackButtonClick()
    {
        click.Play();
        StartCoroutine(Loader());

    }

    IEnumerator Restart()
    {

        for (int i = 0; i < loadObject.Length; i++)
        {

            loadObject[i].Disguise();

        }

        yield return new WaitForSeconds(0.7f);

        SceneManager.LoadScene("Stage");

    }

    IEnumerator Loader()
    {

        for (int i = 0; i < loadObject.Length; i++)
        {

            loadObject[i].Disguise();

        }

        yield return new WaitForSeconds(0.7f);

        SceneManager.LoadScene("Start");

    }

}
