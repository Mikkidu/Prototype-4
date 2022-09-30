using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPush : MonoBehaviour
{   
     void OnCollisionEnter(Collision collision)
    {
        //Отталкиваем игрока при столкновении
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromEnemy = collision.gameObject.transform.position - transform.position;
            playerRB.AddForce(awayFromEnemy * 5, ForceMode.Impulse);
        }
    }
}
