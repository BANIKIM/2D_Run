using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_E : MonoBehaviour
{
    [SerializeField] private AudioSource SoundEffect;


    private void OnCollisionEnter2D(Collision2D collision)//���� �ε����� �Ҹ� ���
    {
        if (collision.gameObject.CompareTag("Tile"))
        {
            Debug.Log("������");
            SoundEffect.Play();
        }
    }
}
