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

    private void OnCollisionEnter2D(Collision2D collision)// ����� �� �ִϸ��̼� ���
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundEffect.Play();//���� ���
            anim.SetTrigger("isPlayer");        
        }
    }

}
