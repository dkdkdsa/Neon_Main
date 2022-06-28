using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButton : MonoBehaviour
{

    public float clearTime;

    public int stageNum;

    public void ClearTimeSet()
    {

        string a = stageNum.ToString();

        clearTime = PlayerPrefs.GetFloat(a);

    }


}
