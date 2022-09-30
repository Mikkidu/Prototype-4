//Спаунится усилением StrikePowerup
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody bulletRb;
    float bulletImpulse = 50f;
    // Start is called before the first frame update
    void Start()
    {
        //Маска для работы в плоскости
        Vector3 mask = new Vector3(1, 0, 1);
        bulletRb = GetComponent<Rigidbody>();
        //Force bullet at looking direction
        bulletRb.AddForce((transform.forward * bulletImpulse), ForceMode.Impulse);
        Destroy(gameObject, 1);
    }
}
