using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMove : MonoBehaviour
{
    [SerializeField] private float delayTime;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 one, two;

    private Vector2 dir;

    void Start()
    {

        StartCoroutine(DirChange());

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(dir * speed * Time.deltaTime);

    }

    IEnumerator DirChange()
    {

        while (true)
        {

            dir = one;
            yield return new WaitForSeconds(delayTime);
            dir = Vector2.zero;
            yield return new WaitForSeconds(1f);
            dir = two;
            yield return new WaitForSeconds(delayTime);
            dir = Vector2.zero;
            yield return new WaitForSeconds(1f);

        }

    }

}
