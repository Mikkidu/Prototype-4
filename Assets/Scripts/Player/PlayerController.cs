using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRB;
    public Transform focalPoint;
    public GameObject powerupGroup;
    public GameObject[] powerupIndicator;
    float speed = 500f;
    public bool hasPowerup;
    int choosePowerup;
    
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        //Find camera box object
        focalPoint = GameObject.Find("Focal Point").transform;
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        //Move player with force from arrows and rotation from camera box
        playerRB.AddForce(focalPoint.forward * speed * forwardInput * Time.deltaTime);
    }

    void LateUpdate()
    {
        //Move box with indicators
        if(transform.position.y > 0)
        {
            powerupGroup.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else
        {
            powerupGroup.transform.position = transform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //choose and switch on powerup and indicator
        if      (other.CompareTag("PowerupPush"))  
        {
            choosePowerup = 1;
            GetComponent<PushPowerup>().enabled = true;
        }
        else if (other.CompareTag("PowerupStrike"))  
        {
            choosePowerup = 2;
            GetComponent<StrikePowerup>().enabled = true;
        }
        else if (other.CompareTag("PowerupSmash"))  
        {
            choosePowerup = 3;
            GetComponent<SmashPowerup>().enabled = true;
        }
        Destroy(other.gameObject);
        StartCoroutine(PowerupCountdownRoutine(choosePowerup));
    }
    
    IEnumerator PowerupCountdownRoutine(int powerup)
    {
        powerupIndicator[powerup - 1].SetActive(true);
        yield return new WaitForSeconds(4f);
        //switch off powerup
        choosePowerup = 0;
        powerupIndicator[powerup - 1].SetActive(false);
        switch(powerup)
        {
            case 1:
            GetComponent<PushPowerup>().enabled = false;
            break;
            case 2:
            GetComponent<StrikePowerup>().enabled = false;
            break;
            case 3:
            GetComponent<SmashPowerup>().switchOff = true;
            break;
        }
    }
}
