using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 1; i < 10; i++)
        {

            string k = i.ToString();

            PlayerPrefs.SetFloat(k, 0);

        }

    }

}
