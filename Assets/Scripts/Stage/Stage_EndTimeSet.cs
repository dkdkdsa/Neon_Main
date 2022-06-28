using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stage_EndTimeSet : MonoBehaviour
{

    [SerializeField] private UI_Move_DOTween[] loadObj;
    [SerializeField] private TextMeshProUGUI bestClearTimeText;
    [SerializeField] private TextMeshProUGUI clearTimeText;
    [SerializeField] private TextMeshProUGUI gameOverTimeText;

    private float bestClearTiem;
    private float clearTime;
    private bool isGameOver;
    private int i;

    // Start is called before the first frame update
    void Start()
    {

        i = PlayerPrefs.GetInt("isGameOver");

        isGameOver = i switch
        {

            0 => true,

            1 => false,

            _ => true,

        };

        if(isGameOver)
        {

            GameOver();

        }
        else
        {

            GameClear();

        }

    }

    private void GameOver()
    {

        gameOverTimeText.text = $" TIME : {string.Format("{0:0.#0}", PlayerPrefs.GetFloat("ThisClearTime"))}";
        loadObj[0].Showing();

    }

    private void GameClear()
    {
        int i = PlayerPrefs.GetInt("StageNum");
        PlayerPrefs.SetFloat(i.ToString(), PlayerPrefs.GetFloat("BestClearTime"));

        bestClearTimeText.text = $"BEST TIME : {string.Format("{0:0.#0}", PlayerPrefs.GetFloat(i.ToString()))}";
        clearTimeText.text = $"CLEAR TIME : {string.Format("{0:0.#0}", PlayerPrefs.GetFloat("ThisClearTime"))}";

        Debug.Log(PlayerPrefs.GetInt("StageNum").ToString());

        loadObj[1].Showing();

    }
}
