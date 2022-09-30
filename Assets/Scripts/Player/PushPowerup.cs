using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPowerup : MonoBehaviour
{
     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && GetComponent<PushPowerup>().enabled)
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRB.AddForce(awayFromPlayer * 15, ForceMode.Impulse);
            //Debug.Log($"Collided with {collision.gameObject.name} with powerup set to {hasPowerup}");
        }
    }
}
