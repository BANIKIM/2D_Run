using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer sprit;
    private BoxCollider2D coll;


    [SerializeField] private LayerMask jumpGround; //���̾ �����Ͽ� ���ǿ� �߰� ����

    private float dirX = 0f;
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float jumpForce = 14f;

    private enum MovementState { idle, run, jump, fall, D_jump } //�ִϸ��̼�
    private bool is_D_jump = false;
    [SerializeField] private AudioSource jumpSoundEffect;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprit = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {

        if(Input.GetButton("Jump") && IsGrounded())
        {
           
            jumpSoundEffect.Play();
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            is_D_jump = false;

        }
        else if (Input.GetButton("Jump") && !is_D_jump) //��Ÿ�Ⱑ �ǹ��ȳ�?
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            anim.SetInteger("state", 4);
            is_D_jump = true;
        }
    }

    private void FixedUpdate()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(dirX * moveSpeed, rigid.velocity.y);

        UpdateAnimat();
    }



    //�¿� �ٶ󺸱� - Run�ִϸ��̼� ����
    private void UpdateAnimat()
    {
        MovementState state;

        if (dirX > 0f) // ��
        {
            state = MovementState.run; 
            sprit.flipX = false;
        }
        else if (dirX < 0f) // ��
        {
            state = MovementState.run;
            sprit.flipX = true;
        }
        else //�ȿ����� ��
        {
            state = MovementState.idle;
        }

        if(rigid.velocity.y > .1f) // ����
        {
            state = MovementState.jump;
           
        }
        else if(rigid.velocity.y < -.1f) //����
        {
            state = MovementState.fall;                       
        }

        anim.SetInteger("state", (int)state); //���� �ִ´�

    }


    private bool IsGrounded() //2�� ���� üũ
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpGround);
    }

}
