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
        //�� ����
        isGround = Physics2D.OverlapBox(transform.position, boxSize, 0, LayerMask.GetMask("Ground"));

        //�� ����
        wall = Physics2D.OverlapBox(transform.position, parkourBoxSize, 0, LayerMask.GetMask("Ground"));

        //�����̴� �� ����
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

    //���� �ո��°� ����
    void FixedUpdate()
    {

        Move();
        
    }

    //������
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

    //�����̴� �ٴڿ� ������ ���� �����̰�
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

    //����
    private void Jump()
    {

        if((isGround == true || moveWall == true) && Input.GetKeyDown(jumpKeyCode))
        {

            playerRigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        }  

    }

    //����
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

    //�״°�
    private void Die()
    {

        if(Physics2D.OverlapBox(transform.position, new Vector2(1, 1), 0, LayerMask.GetMask("Trap")))
        {
            //���߿� �״°� �����
            Debug.Log("Die");

        }

    }

    //�������� Ŭ����
    private void Clear()
    {

        if(Physics2D.OverlapBox(transform.position, new Vector2(1, 1), 0, LayerMask.GetMask("Clear")))
        {
            //���߿� �� �Ѿ�°� �����
            Debug.Log("Clear");

        }

    }

    //������ ������ ���� ������
    IEnumerator delay()
    {

        isWallJumping = true;
        yield return new WaitForSeconds(daleyTime);
        isWallJumping = false;

    }

}
