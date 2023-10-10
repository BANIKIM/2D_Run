using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer sprit;
    private BoxCollider2D coll;


    [SerializeField] private LayerMask jumpGround; //레이어를 선택하여 조건에 추가 가능

    private float dirX = 0f;
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float jumpForce = 14f;

    private enum MovementState { idle, run, jump, fall, D_jump } //애니메이션
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
        else if (Input.GetButton("Jump") && !is_D_jump) //벽타기가 되버렸네?
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



    //좌우 바라보기 - Run애니메이션 실행
    private void UpdateAnimat()
    {
        MovementState state;

        if (dirX > 0f) // 좌
        {
            state = MovementState.run; 
            sprit.flipX = false;
        }
        else if (dirX < 0f) // 우
        {
            state = MovementState.run;
            sprit.flipX = true;
        }
        else //안움직일 때
        {
            state = MovementState.idle;
        }

        if(rigid.velocity.y > .1f) // 점프
        {
            state = MovementState.jump;
           
        }
        else if(rigid.velocity.y < -.1f) //착지
        {
            state = MovementState.fall;                       
        }

        anim.SetInteger("state", (int)state); //값을 넣는다

    }


    private bool IsGrounded() //2중 점프 체크
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpGround);
    }

}
