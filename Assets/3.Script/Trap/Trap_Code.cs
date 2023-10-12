using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스파이크 타일 플랫폼 코드

public class Trap_Code : MonoBehaviour
{


    private Rigidbody2D rigid;
    private Animator anim;
    //[SerializeField] private AudioSource SoundEffect;


    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //SoundEffect.Play();
            anim.SetTrigger("isPlayer");
            rigid.bodyType = RigidbodyType2D.Dynamic;

        }
     

    }
    
}
