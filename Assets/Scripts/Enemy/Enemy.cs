using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 knockBackBoxSize;
    [SerializeField] private float knockBackPower;
    [SerializeField] private float delayTime;

    private int enemtHP = 2;
    private Rigidbody2D playerRigidbody;
    private Collider2D knockBackBox;
    private bool isKnockBacking;

    public bool IsKnockBacking => isKnockBacking;

    void Awake()
    {

        playerRigidbody = GetComponent<Rigidbody2D>();    

    }

    void Update()
    {

        EnemyHP();
        EnemyTrap();
        KnockBack();

    }

    //적이 함정에 닿았을때
    private void EnemyTrap()
    {

        if(Physics2D.OverlapBox(transform.position, new Vector2(1, 1), 0, LayerMask.GetMask("Trap")))
        {

            Destroy(gameObject);

        }

    }

    //적 넉백
    private void KnockBack()
    {

        knockBackBox = Physics2D.OverlapBox(transform.position, knockBackBoxSize, 0, LayerMask.GetMask("Bullet"));

        if(knockBackBox == true)
        {
            enemtHP--;
            if(knockBackBox.transform.position.x > transform.position.x && isKnockBacking == false)
            {
                isKnockBacking = true;
                playerRigidbody.AddForce(new Vector2(-2, 3) * knockBackPower, ForceMode2D.Impulse);
                Destroy(knockBackBox.gameObject);
                StartCoroutine(delay());

            }
            else if(knockBackBox.transform.position.x < transform.position.x && isKnockBacking == false)
            {
                isKnockBacking = true;
                playerRigidbody.AddForce(new Vector2(2, 3) * knockBackPower, ForceMode2D.Impulse);
                Destroy(knockBackBox.gameObject);
                StartCoroutine(delay());

            }

            Destroy(knockBackBox.gameObject);

        }

    }

    //적 HP
    private void EnemyHP()
    {
        
        if(enemtHP <= 0)
        {

            Destroy(gameObject);

        }

    }

    //연속넉백을 막기위한 코루틴(지연시간)
    IEnumerator delay()
    {

        yield return new WaitForSeconds(delayTime);

        isKnockBacking = false;

    }

}
