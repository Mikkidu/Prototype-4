using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    float FlashDelay = 5f, flashTrigger, smashHeigt = 10f, moveForce = 500f, bossSpeed = 10f;
    bool smashJump;
    GameObject player, bossParticle;
    Rigidbody bossRb;
    void Start()
    {
        //
        bossRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        //Задаём время первого удара
        flashTrigger = Time.realtimeSinceStartup + FlashDelay;
        //Находим и запускаем визуальные эффекты
        bossParticle = GameObject.FindGameObjectWithTag("BossParticle");
        bossParticle.GetComponent<ParticleSystem>().Play();
    }

    void Update()
    {   
        //Уничтожаем босса если падает и выключаем эффекты
        if (transform.position.y < -10) 
        {
            bossParticle.GetComponent<ParticleSystem>().Stop();
            Destroy(gameObject);
        }
        //ВЫчислияем вектор-напрявление в сторону игрока и обнуляем поиск по Y
        Vector3 lookDirection = player.transform.position - transform.position;
        lookDirection = new Vector3(lookDirection.x, 0, lookDirection.z);
        //вигаем босса в сторону игрока с ограничением скорости
        bossRb.AddForce((lookDirection.normalized - bossRb.velocity / bossSpeed) * moveForce * Time.deltaTime);
        //По таймеру запускаем прыжок
        if (Time.realtimeSinceStartup > flashTrigger)
        {
            bossRb.AddForce(Vector3.up * 30f, ForceMode.Impulse);
            flashTrigger = Time.realtimeSinceStartup + FlashDelay;
            smashJump = true;
        }
        //Если уже прыгнули - отсчитываем высоту и задаём импульс вниз
        else if (smashJump && transform.position.y > smashHeigt)
        {
            bossRb.AddForce(Vector3.down * 15f, ForceMode.Impulse);
        }
        //Управление эффектами босса с запретом прыгать вместе с боссом
        if(transform.position.y > 0)
        {
            bossParticle.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else
        {
            bossParticle.transform.position = transform.position;
        }
    }

    /* void LateUpdate()
    {
        
        //Vector3 futurePosition = player.GetComponent<Rigidbody>().velocity * (lookDirection.magnitude / GetComponent<Rigidbody>().velocity.magnitude); 
        //lookDirection += futurePosition;
        
        //cross.transform.position = player.transform.position + futurePosition;
        
    } */

        void OnCollisionEnter(Collision collision)
    {
        if (smashJump)
        {
            //при призелении сбрасываем скорость и выравниваем позицию
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            Rigidbody bossRb = GetComponent<Rigidbody>();
            bossRb.velocity = new Vector3(bossRb.velocity.x, 0, bossRb.velocity.z);
            smashJump = false;
            //Задаём вызрывной импульс от места удара дл игрока
            player.GetComponent<Rigidbody>().AddExplosionForce(30f, transform.position - new Vector3(0, -0.5f, 0), 10f, 0, ForceMode.Impulse);
            //Спауним усиление
            GameObject.Find("Spawn Manager").GetComponent<SpawnManager>().SpawnPowerupPrefab();
        }
    }
}
