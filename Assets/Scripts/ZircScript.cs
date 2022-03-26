using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZircScript : MonoBehaviour
{

    public GameObject Personaje;
    public GameObject BalaPrefab;
    private float LastShoot;
    public int Health = 2;
    private float distancex;
    private float distancey;

    //Sonidos
    public GameObject SonidoGolpeMetalico;
    public GameObject SonidoLaserGun;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Personaje == null) return;

        Vector3 direction = Personaje.transform.position - transform.position;
        if (direction.x >= 0.0f) {transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); BalaPrefab.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); }
        else {transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); BalaPrefab.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); }


        distancex = Mathf.Abs(Personaje.transform.position.x - transform.position.x);
        distancey = Mathf.Abs(Personaje.transform.position.y - transform.position.y);

        if (distancex < 1.2f && distancey < 0.5f)
        {
            animator.SetBool("Detectado", true);
        }
        if (distancex > 1.2f && distancey < 0.5f)
        {
            animator.SetBool("Detectado", false);
        }

        if (distancex < 1.0f&&Time.time > LastShoot + 1f&&Health!=0 && distancey < 0.3)
        {
            animator.SetTrigger("Attack");
            Shoot();
            LastShoot = Time.time;
        }
          


    }

    public void Destroy()
    {
        Destroy(gameObject);
        

    }
  

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;
        Instantiate(SonidoLaserGun);
        GameObject bala = Instantiate(BalaPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bala.GetComponent<BalaScript>().SetDirection(direction);
    }

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
