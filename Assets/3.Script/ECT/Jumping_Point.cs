using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping_Point : MonoBehaviour
{
    private Animator anim;
    [SerializeField]private Player_Movement player;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player_Movement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)// 닿았을 때 애니메이션 재생
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("닿음 플레이어");
            anim.SetTrigger("isPlayer");        }
    }

}
