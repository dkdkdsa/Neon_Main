using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float slidePower;
    [SerializeField] private float wallJumpPower;
    [SerializeField] private float daleyTime;
    [SerializeField] private KeyCode jumpKeyCode;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 parkourBoxSize;

    private bool isWall;
    private bool isWallJumping;
    private string dir;
    private Collider2D isGround;
    private Collider2D wall;
    private Collider2D moveWall;
    private Rigidbody2D playerRigidbody;
    private string playerDir;
    
    public string PlayerDir => playerDir;

    void Awake()
    {
        
        playerRigidbody = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        //땅 감지
        isGround = Physics2D.OverlapBox(transform.position, boxSize, 0, LayerMask.GetMask("Ground"));

        //벽 감지
        wall = Physics2D.OverlapBox(transform.position, parkourBoxSize, 0, LayerMask.GetMask("Ground"));

        //움직이는 벽 감지
        moveWall = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - 0.35f), new Vector2(0.9f, 0.3f), 0, LayerMask.GetMask("MoveGround"));

        Parkour();
        MoveGround();
        Die();
        Clear();

        if(isWall == false)
        {

            Jump();

        }

    }

    //벽이 뚫리는거 방지
    void FixedUpdate()
    {

        Move();
        
    }

    //움직임
    private void Move()
    {

        float moveX = 0;


        if (isWallJumping == false && isWall == false) moveX = Input.GetAxisRaw("Horizontal");

        playerDir = moveX switch
        {

            1 => "R",

            -1 => "L",

            _ => playerDir,

        };


        if ((isWallJumping == true || isWall == true) && Input.GetKey(KeyCode.LeftArrow) && dir == "L") moveX = -1;
        if ((isWallJumping == true || isWall == true) && Input.GetKey(KeyCode.RightArrow) && dir == "R") moveX = 1;

        transform.Translate(new Vector2(moveX, 0) * speed * Time.deltaTime);        

    }

    //움직이는 바닥에 닿으면 같이 움직이게
    private void MoveGround()
    {

        if(moveWall == true)
        {

            transform.SetParent(moveWall.transform);

        }
        else
        {

            transform.SetParent(null);

        }

    }

    //점프
    private void Jump()
    {

        if((isGround == true || moveWall == true) && Input.GetKeyDown(jumpKeyCode))
        {

            playerRigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        }  

    }

    //파쿠르
    private void Parkour()
    {

        if(wall == true && isWall == false) playerRigidbody.velocity = Vector2.zero;

        if(wall == true && isGround == false)
        {

            isWall = true;
            playerRigidbody.gravityScale = 0;
            transform.Translate(Vector2.down * slidePower * Time.deltaTime);

            if(wall.transform.position.x > transform.position.x) dir = "L";
            else if(wall.transform.position.x < transform.position.x) dir = "R";

            if (Input.GetKeyDown(jumpKeyCode))
            {

                if(wall.transform.position.x > transform.position.x)
                {
                    playerRigidbody.AddForce(new Vector2(-0.5f, 1.7f) * wallJumpPower, ForceMode2D.Impulse);
                    StartCoroutine(delay());


                }
                else if(wall.transform.position.x < transform.position.x)
                {
                    playerRigidbody.AddForce(new Vector2(0.5f, 1.7f) * wallJumpPower, ForceMode2D.Impulse);
                    StartCoroutine(delay());

                }

            }


        }
        else if(wall == true)
        {

            isWall = true;
            if (wall.transform.position.x > transform.position.x) dir = "L";
            else if (wall.transform.position.x < transform.position.x) dir = "R";
        }
        else
        {

            isWall = false;
            playerRigidbody.gravityScale = 2;
        }

    }

    //죽는거
    private void Die()
    {

        if(Physics2D.OverlapBox(transform.position, new Vector2(1, 1), 0, LayerMask.GetMask("Trap")))
        {
            //나중에 죽는거 만들기
            Debug.Log("Die");

        }

    }

    //스테이지 클리어
    private void Clear()
    {

        if(Physics2D.OverlapBox(transform.position, new Vector2(1, 1), 0, LayerMask.GetMask("Clear")))
        {
            //나중에 씬 넘어가는거 만들기
            Debug.Log("Clear");

        }

    }

    //벽점프 움직임 제한 딜레이
    IEnumerator delay()
    {

        isWallJumping = true;
        yield return new WaitForSeconds(daleyTime);
        isWallJumping = false;

    }

}
