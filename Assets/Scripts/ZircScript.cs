using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZircScript : MonoBehaviour
{
    //
    public GameObject Personaje;

    //
    public GameObject BalaPrefab;
    private float LastShoot;
    public int Health = 2;
    private float distancex;
    private float distancey;

    //Sonidos
    public GameObject SonidoGolpeMetalico;
    public GameObject SonidoLaserGun;
    private Animator animator;


    //Obtenemos en el start el componente del animator
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //Si no existe un personaje retorna
        if (Personaje == null) return;

        //Si existe volteamos al zirc en la dirección del personaje, la bala tambien debe cambiar de dirección
        Vector3 direction = Personaje.transform.position - transform.position;
        if (direction.x >= 0.0f) {transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); BalaPrefab.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); }
        else {transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); BalaPrefab.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); }

        //Determinamos la distancia entre el personaje y el zirc con un valor absoluto
        distancex = Mathf.Abs(Personaje.transform.position.x - transform.position.x);
        distancey = Mathf.Abs(Personaje.transform.position.y - transform.position.y);

        //Si esta en el rango activamos la animación de detectado
        if (distancex < 1.2f && distancey < 0.5f)
        {
            animator.SetBool("Detectado", true);
        }
        if (distancex > 1.2f && distancey < 0.5f)
        {
            animator.SetBool("Detectado", false);
        }

        //Si esta aun mas dentro del rango disparamos y dejamos de disparar si morimos
        if (distancex < 1.0f&&Time.time > LastShoot + 1f&&Health!=0 && distancey < 0.3)
        {
            animator.SetTrigger("Attack");
            Shoot();
            LastShoot = Time.time;
        }
          


    }

    //Función destroy, es llamada cuando se activa la animación de muerte
    public void Destroy()
    {
        Destroy(gameObject);
        
    }
  
    //Instanciamos la bala, para ello utilizamos la dirección 
    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;
        Instantiate(SonidoLaserGun);
        GameObject bala = Instantiate(BalaPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bala.GetComponent<BalaScript>().SetDirection(direction);
    }

    //Función para cuando golpean al zirc verde, recibe daño y pierde vida hasta morir
    public void Hit(int DamageTaken)
    {
        Health = Health - DamageTaken;
        animator.SetTrigger("TD");
        Instantiate(SonidoGolpeMetalico);
        if (Health == 0)
        {
            animator.SetTrigger("Muerte");
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;

        }
        
    }

}
