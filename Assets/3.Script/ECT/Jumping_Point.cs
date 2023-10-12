using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping_Point : MonoBehaviour
{
    private Animator anim;
    [SerializeField]private AudioSource SoundEffect;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)// 닿았을 때 애니메이션 재생
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundEffect.Play();//사운드 재생
            anim.SetTrigger("isPlayer");        
        }
    }

}
