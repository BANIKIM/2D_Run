using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Die : MonoBehaviour
{
   

    private void Start()
    {
       gameObject.GetComponentsInParent<Enemy_Move>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player_A"))
        {
            Debug.Log("����");            
            Destroy(transform.parent.gameObject);//�θ� ������Ʈ �ı�
            
        }
    }
}
