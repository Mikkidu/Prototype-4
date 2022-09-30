using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikePowerup : MonoBehaviour
{
    float shootDelay = .25f, shootTrigger;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.realtimeSinceStartup > shootTrigger)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                Vector3 lookAtEnemy = (enemies[i].transform.position - transform.position).normalized;
                Instantiate(bullet, transform.position + lookAtEnemy, Quaternion.LookRotation(lookAtEnemy, Vector3.up));
                shootTrigger = Time.realtimeSinceStartup + shootDelay;
                //Debug.Log($"Player {Quaternion.LookRotation(lookAtEnemy, Vector3.up) * Vector3.forward}");
            } 
        }
    }
}
