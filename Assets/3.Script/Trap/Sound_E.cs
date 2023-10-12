using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_E : MonoBehaviour
{
    [SerializeField] private AudioSource SoundEffect;


    private void OnCollisionEnter2D(Collision2D collision)//¶¥¿¡ ºÎµúÈ÷¸é ¼Ò¸® Àç»ý
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            Debug.Log("¶¥¹ÚÈû");
            SoundEffect.Play();
        }
    }
}
