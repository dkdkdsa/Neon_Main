using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed;

    void Update()
    {
        
        //총알 움직임
        transform.Translate(new Vector2(1, 0) * speed * Time.deltaTime);

        Range();

    }

    //총알 사라지는 조건
    private void Range()
    {

        if(Physics2D.OverlapCircle(transform.position, 0.5f, LayerMask.GetMask("Ground")))
        {

            Destroy(gameObject);

        }

        if (Physics2D.OverlapCircle(transform.position, 0.5f, LayerMask.GetMask("MoveGround")))
        {

            Destroy(gameObject);

        }

    }

}
