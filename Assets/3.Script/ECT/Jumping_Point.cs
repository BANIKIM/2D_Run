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

    private void OnCollisionEnter2D(Collision2D collision)// ����� �� �ִϸ��̼� ���
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("���� �÷��̾�");
            anim.SetTrigger("isPlayer");        }
    }

}
