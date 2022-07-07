using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField] private Image image;
    [SerializeField] private float duration;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private Vector2 moveDir;
    [SerializeField] private Ease ease;
    [SerializeField] AudioSource playSound;

    private bool isClear;
    private float bestClearTime;
    private float time;

    public static bool start;

    // Update is called once per frame
    void Update()
    {

        Debug.Log(time);

        if(start == true)
        {

            if(isClear == false)
            {

                time += Time.deltaTime;

            }

        }
       

    }

    public void TiemSet()
    {
        isClear = true;

        playSound.Pause();

        StageLoader.isLoading = true;

        int i = PlayerPrefs.GetInt("StageNum".ToString());

        bestClearTime = PlayerPrefs.GetFloat(i.ToString());

        loadingText.text = "Loading";

        image.DOFade(1, 0.3f);

        if (bestClearTime == 0)
        {

            bestClearTime = time;
            Debug.Log(1);
            PlayerPrefs.SetFloat("BestClearTime", bestClearTime);
            PlayerPrefs.SetFloat("ThisClearTime", time);
            PlayerPrefs.SetInt("isGameOver", 1);
            Sequence sequence = DOTween.Sequence()
            .Append(loadingText.transform.DOMove(new Vector2(loadingText.transform.position.x - moveDir.x, loadingText.transform.position.y - moveDir.y), duration)).SetEase(ease)
            .OnComplete(() =>
            {

                StartCoroutine(Loading());

            });

        }
        else if(bestClearTime > time)
        {

            bestClearTime = time;
            Debug.Log(2);
            PlayerPrefs.SetFloat("BestClearTime", bestClearTime);
            PlayerPrefs.SetFloat("ThisClearTime", time);
            PlayerPrefs.SetInt("isGameOver", 1);
            Sequence sequence = DOTween.Sequence()
            .Append(loadingText.transform.DOMove(new Vector2(loadingText.transform.position.x - moveDir.x, loadingText.transform.position.y - moveDir.y), duration)).SetEase(ease)
            .OnComplete(() =>
            {

                StartCoroutine(Loading());

            });
        }
        else
        {

            PlayerPrefs.SetFloat("BestClearTime", bestClearTime);
            PlayerPrefs.SetFloat("ThisClearTime", time);
            PlayerPrefs.SetInt("isGameOver", 1);
            Debug.Log(3);
            Sequence sequence = DOTween.Sequence()
            .Append(loadingText.transform.DOMove(new Vector2(loadingText.transform.position.x - moveDir.x, loadingText.transform.position.y - moveDir.y), duration)).SetEase(ease)
            .OnComplete(() =>
            {

                StartCoroutine(Loading());

            });

        }

    }

    public void Die()
    {

        isClear = true;

        StageLoader.isLoading = true;

        PlayerPrefs.SetFloat("ThisClearTime", time);
        PlayerPrefs.SetInt("isGameOver", 0);

        loadingText.text = "Loading";

        image.DOFade(1, 0.3f);

        playSound.Pause();

        Sequence sequence = DOTween.Sequence()
        .Append(loadingText.transform.DOMove(new Vector2(loadingText.transform.position.x - moveDir.x, loadingText.transform.position.y - moveDir.y), duration)).SetEase(ease)
        .OnComplete(() =>
        {

            StartCoroutine(Loading());

        });
    }

    IEnumerator Loading()
    {

        for(int i = 0; i < 1; i++)
        {


            for(int j = 0; j < 3; j++)
            {

                loadingText.text += ".";

                yield return new WaitForSeconds(0.5f);

            }

            loadingText.text = "Loading";

            yield return new WaitForSeconds(0.5f);

        }

        Sequence sequence = DOTween.Sequence()
        .Append(loadingText.transform.DOMove(new Vector2(loadingText.transform.position.x + moveDir.x, loadingText.transform.position.y + moveDir.y), duration)).SetEase(ease)
        .OnComplete(() =>
        {
            start = false;
            SceneManager.LoadScene("Stage_End");

        });

    }

}
