using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private float coolTime;
    [SerializeField] private KeyCode fireKey;
    [SerializeField] private GameObject bullet;

    private bool isFireCoolTime;
    private Quaternion bulletRotate;
    private Player player;

    void Awake()
    {
        
        player = GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {

        Fire();

    }

    //플레이어 총알 발사(Q키)
    private void Fire()
    {

        if (Input.GetKey(fireKey) && isFireCoolTime == false)
        {

            bulletRotate = player.PlayerDir switch
            {

                "R" => Quaternion.Euler(new Vector3(0, 0, 0)),

                "L" => Quaternion.Euler(new Vector3(0, 0, 180)),

                _ => Quaternion.Euler(new Vector3(0, 0, 0)),

            };

            Instantiate(bullet, transform.position, bulletRotate);

            StartCoroutine(CoolTime());

        }

        IEnumerator CoolTime()
        {

            isFireCoolTime = true;

            yield return new WaitForSeconds(coolTime);

            isFireCoolTime = false;

        }

    }

}
