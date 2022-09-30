using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashPowerup : MonoBehaviour
{
    float shootDelay = 1, shootTrigger, smashHeigt = 10f;
    bool smashJump;
    public bool switchOff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !smashJump)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 15f, ForceMode.Impulse);
            smashJump = true;
        }
        else if (smashJump && transform.position.y > smashHeigt)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.down * 15f, ForceMode.Impulse);
        }
        if (switchOff && !smashJump)
        {
            switchOff = false;
            enabled = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (smashJump)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            Rigidbody playerRb = GetComponent<Rigidbody>();
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
            smashJump = false;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(30f, transform.position - new Vector3(0, -0.5f, 0), 10f, 0, ForceMode.Impulse);
            }
        }
    }
}
