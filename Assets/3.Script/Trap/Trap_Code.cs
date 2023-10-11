using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trap_Code : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anim;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어가 들어옴");
            anim.SetTrigger("isPlayer");
            rigid.bodyType = RigidbodyType2D.Dynamic;

        }
    }
}
