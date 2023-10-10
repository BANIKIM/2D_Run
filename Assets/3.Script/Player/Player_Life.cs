using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anim;
    [SerializeField] private AudioSource DieSoundEffect;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Trap"))
        {
            
            Die();
        }
    }

    private void Die()
    {
        DieSoundEffect.Play();
        rigid.bodyType = RigidbodyType2D.Static; //�ٵ� Type�� Static���� �ٲٸ鼭 ������ �������� ����
        anim.SetTrigger("isDead");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
