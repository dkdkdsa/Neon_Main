using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TTC : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
        if(Physics2D.OverlapBox(transform.position, new Vector2(2, 1),0, LayerMask.GetMask("Player")))
        {

            SceneManager.LoadScene("Start");

        }

    }
}
