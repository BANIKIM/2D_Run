using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_A : MonoBehaviour
{

    [SerializeField] private Player_Movement player;
    void Start()
    {
        player = GetComponent<Player_Movement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("����");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("�Լ�ȣ��");
            //player.JJJ();
        }
    }
    
}
