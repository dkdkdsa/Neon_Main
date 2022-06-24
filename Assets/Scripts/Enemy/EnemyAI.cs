using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float speed;

    private Collider2D range;
    private Vector2 moveDir;
    private Enemy enemy;

    void Awake()
    {
        
        enemy = GetComponent<Enemy>();

    }

    void Update()
    {
        AI();
    }

    //적 플레이어 추적 AI
    private void AI()
    {

        range = Physics2D.OverlapBox(transform.position, boxSize, 0, LayerMask.GetMask("Player"));

        if (range && enemy.IsKnockBacking == false)
        {

            if(transform.position.x < range.transform.position.x)
            {

                moveDir = Vector2.right;

            }
            else if(transform.position.x > range.transform.position.x)
            {

                moveDir = Vector2.left;

            }
            else
            {

                moveDir = Vector2.zero;

            }

            transform.Translate(moveDir * speed * Time.deltaTime);

        }

    }

}
