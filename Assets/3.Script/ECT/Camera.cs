using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    [SerializeField]private Transform player;

    // Update is called once per frame
    void Update()
    {
        //카메라의 포지션은(=) 플레이어으 x와 y의 값과 카메라의 z의 값이다.
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
