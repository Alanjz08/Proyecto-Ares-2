using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VScript : MonoBehaviour
{
    //Zirc volador

    
    public GameObject Personaje;
    public GameObject BalaPrefab;
    private float LastShoot;
    public int Health = 1;
    private float distancex;
    private float distancey;

    public float speed = 0.5f;
    private float waitTime;
    public Transform[] moveSpots;
    public float startWaitTime = 2;
    private int i = 0;
    private Vector2 actualPos;

    

    //Sonidos
    public GameObject SonidoGolpeMetalico;
    public GameObject SonidoLaserGun;
    private Animator animator;

    // Obtenemos el componente del animator y el tiempo de espera para moverse
    void Start()
    {
        animator = GetComponent<Animator>();
        waitTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Se mueve en las posiciones del vector en determinado tiempo
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].transform.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots[i].transform.position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
                {
                    i++;
                }
                else
                {
                    i = 0;
                }
                waitTime = startWaitTime;

            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }


        //Comportamientos();
        if (Personaje == null) return;

        //Ataca cuando detecta un personaje
        distancex = Mathf.Abs(Personaje.transform.position.x - transform.position.x);
        distancey = Mathf.Abs(Personaje.transform.position.y - transform.position.y);
        if (distancex < 0.3f && Time.time > LastShoot + 1f && Health != 0 && distancey < 1.0f)
        {
            animator.SetTrigger("Attack");

            LastShoot = Time.time;
        }

        
    }
    //Se destruye al activar la animación de muerte
    public void Destroy()
    {
        Destroy(gameObject);
    }

    //Instancia una bala para disparar en el vector
    private void Shoot()
    {
        Vector3 direction;
        direction = Vector3.down;
        Instantiate(SonidoLaserGun);
        GameObject bala = Instantiate(BalaPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bala.GetComponent<VBala>().SetDirection(direction);
    }


    //Función recibir daño hasta morir
    public void Hit(int DamageTaken)
    {
        Health = Health - DamageTaken;
        Instantiate(SonidoGolpeMetalico);
        if (Health == 0)
        {
            animator.SetTrigger("Muerte");
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;

        }

    }

    

}
