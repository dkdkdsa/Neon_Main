using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class StageLoader : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Ease ease;
    [SerializeField] private Vector2 moveDir;
    [SerializeField] private float delayTime;
    [SerializeField] private float duration;
    [SerializeField] private GameObject player;

    private Stage[] stage;
    private int stageNum;

    public static bool isLoading;

    void Awake()
    {

        isLoading = true;
        stage = GetComponentsInChildren<Stage>();
        stageNum = PlayerPrefs.GetInt("StageNum");

    }

    void Start()
    {

        Debug.Log(stageNum);
        Sequence sequence = DOTween.Sequence()
        .Append(text.transform.DOMove(new Vector2(text.transform.position.x + moveDir.x, text.transform.position.y + moveDir.y), duration).SetEase(ease)
        .SetDelay(delayTime)
        .OnComplete(() =>
        {

            StartCoroutine(Loading());

        }));

    }

    private void Load()
    {

        Sequence sequence = DOTween.Sequence()
        .Append(text.transform.DOMove(new Vector2(text.transform.position.x - moveDir.x, text.transform.position.y - moveDir.y), duration).SetEase(ease)
        .SetDelay(delayTime));

    }

    IEnumerator Loading()
    {

        player.transform.position = stage[stageNum].startPos.position;

        for(int i = 0; i < 2; i++)
        {

            for(int j = 0; j < 3; j++)
            {

                text.text += ".";
                yield return new WaitForSeconds(1);

            }
            text.text = "Loading";
            yield return new WaitForSeconds(1);
        }

        text.text = "Complete";
        yield return new WaitForSeconds(1);

        for(int i = 3; i > 0; i--)
        {

            text.text = $"{i}";
            yield return new WaitForSeconds(1);

        }

        text.text = "Start";

        yield return new WaitForSeconds(0.5f);
        Load();
        image.DOFade(0, 0.3f)
        .OnComplete(() =>
        {

            isLoading = false;

        });

    }

    

}
