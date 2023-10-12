using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_E : MonoBehaviour
{
    [SerializeField] private AudioSource SoundEffect;


    private void OnCollisionEnter2D(Collision2D collision)//顶俊 何碟洒搁 家府 犁积
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            SoundEffect.Play();
        }
    }
}
