using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestTimeSetter : MonoBehaviour
{
    private StageButton[] stageButton;

    void Awake()
    {
        
        stageButton = GetComponentsInChildren<StageButton>();

    }

    void Start()
    {
        
        for(int i = 0; i < stageButton.Length; i++)
        {

            stageButton[i].ClearTimeSet();

        }

    }

}
