using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    float speed = 500f;
    Transform player;
    Rigidbody enemyRb;
    Vector3 lookDirection;
    // Start is called before the first frame update
    void Start()
    {
        //Находим игрока
        player = GameObject.Find("Player").transform;
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //ВЫчисляем вектор-направление вв строну игрока
        lookDirection = (player.position - transform.position).normalized;
        //Двигаем врага в сторону игрока
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);
        //Уничтожаем объект, если враг упал с острова
        if (transform.position.y < -10) Destroy(gameObject);
    }

}
