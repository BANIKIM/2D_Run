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

    private enum MovementState { idle, run, jump, fall, D_jump, W_jump } //애니메이션
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

  
        /* else if (Input.GetButton("Jump") && !is_D_jump) //벽타기가 되버렸네?
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

    //20231011 추가 벽점프
    private void Walljump()
    {
        if(dirX > 0)
        {
            Vector2 forntVec = new Vector2(rigid.position.x + dirX * 0.7f, rigid.position.y);
            Debug.DrawRay(forntVec, Vector3.left, new Color(0, 0, 1));

            RaycastHit2D rayHit = Physics2D.Raycast(forntVec, Vector3.left, 0.7f, LayerMask.GetMask("Wall"));

            if(rayHit)//벽을 만나면
            {
                
                if (Input.GetButton("Jump") && !is_D_jump) //더블점프
                {
                    D_jumpSoundEffect.Play();
                    rigid.velocity = new Vector2(rigid.velocity.x, jumpForce * 1.4f);
                    anim.SetInteger("state", 4);
                    
                    is_D_jump = true;
                }
                else//잡기
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
            if (rayHit)//벽을 만나면
            {
                
                if (Input.GetButton("Jump") && !is_D_jump) //더블점프
                {
                    D_jumpSoundEffect.Play();
                    rigid.velocity = new Vector2(rigid.velocity.x, jumpForce*1.4f);
                    anim.SetInteger("state", 4);
                    is_D_jump = true;

                }
                else // 벽잡기
                {
                    anim.SetInteger("state", 5);
                }
                
            }
        }
    }

    //20231011 점프대만나면 점프
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Jumping_Point"))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce*2f);
        }

      
    }

    private void OnTriggerEnter2D(Collider2D collision)//적을 밟을 때 
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AtteckSoundEffect.Play();
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce * 0.7f);
        }
    }

}
