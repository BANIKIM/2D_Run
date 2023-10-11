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

    private enum MovementState { idle, run, jump, fall, D_jump, W_jump } //�ִϸ��̼�
    private bool is_D_jump = false;
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource AtteckSoundEffect;
    [SerializeField] private AudioSource D_jumpSoundEffect;


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

  
        /* else if (Input.GetButton("Jump") && !is_D_jump) //��Ÿ�Ⱑ �ǹ��ȳ�?
         {
             rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
             anim.SetInteger("state", 4);
             is_D_jump = true;
         }*/
        Walljump();

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

    //20231011 �߰� ������
    private void Walljump()
    {
        if(dirX > 0)
        {
            Vector2 forntVec = new Vector2(rigid.position.x + dirX * 0.7f, rigid.position.y);
            Debug.DrawRay(forntVec, Vector3.left, new Color(0, 0, 1));

            RaycastHit2D rayHit = Physics2D.Raycast(forntVec, Vector3.left, 0.7f, LayerMask.GetMask("Wall"));

            if(rayHit)//���� ������
            {
                
                if (Input.GetButton("Jump") && !is_D_jump) //��������
                {
                    D_jumpSoundEffect.Play();
                    rigid.velocity = new Vector2(rigid.velocity.x, jumpForce * 1.4f);
                    anim.SetInteger("state", 4);
                    
                    is_D_jump = true;
                }
                else//���
                {
                    anim.SetInteger("state", 5);
                }
                
            }
        }
        else if(dirX < 0)
        {
            Vector2 forntVec = new Vector2(rigid.position.x + dirX * 0.7f, rigid.position.y);
            Debug.DrawRay(forntVec, Vector3.right, new Color(0, 0, 1));

            RaycastHit2D rayHit = Physics2D.Raycast(forntVec, Vector3.right, 0.7f, LayerMask.GetMask("Wall"));
            if (rayHit)//���� ������
            {
                
                if (Input.GetButton("Jump") && !is_D_jump) //��������
                {
                    D_jumpSoundEffect.Play();
                    rigid.velocity = new Vector2(rigid.velocity.x, jumpForce*1.4f);
                    anim.SetInteger("state", 4);
                    is_D_jump = true;

                }
                else // �����
                {
                    anim.SetInteger("state", 5);
                }
                
            }
        }
    }

    //20231011 �����븸���� ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Jumping_Point"))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce*2f);
        }

      
    }

    private void OnTriggerEnter2D(Collider2D collision)//���� ���� �� 
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AtteckSoundEffect.Play();
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce * 0.7f);
        }
    }

}
