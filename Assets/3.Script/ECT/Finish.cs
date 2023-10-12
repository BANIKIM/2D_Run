using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private Animator anim;
    private bool levelCompleted = false;
    public int Total=0;
    public int S_Total=0;
    void Start()
    {
        finishSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.gameObject.name=="Player" && !levelCompleted)
        {
            Total += S_Total + PlayerPrefs.GetInt("Sro");
            S_Total = 0;
            finishSound.Play();
            anim.SetTrigger("is_Out");
            anim.SetTrigger("is_In");
            levelCompleted = true;
            Invoke("CompleteLevel", 2f);//2초뒤에 다음 단계
            PlayerPrefs.SetInt("Sro", Total);
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
