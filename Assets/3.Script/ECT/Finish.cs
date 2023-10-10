using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private Animator anim;
    private bool levelCompleted = false;


    void Start()
    {
        finishSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name=="Player" && !levelCompleted)
        {
            finishSound.Play();
            anim.SetTrigger("is_Out");
            anim.SetTrigger("is_In");
            levelCompleted = true;
            Invoke("CompleteLevel", 2f);
            //CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
