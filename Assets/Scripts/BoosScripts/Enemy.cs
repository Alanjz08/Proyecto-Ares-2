using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Le da los atributos de enemigo al jefe

public class Enemy : MonoBehaviour
{
    public int HealthPoints;
    public int Speed;
   //private Animator animator;
    public GameObject SonidoGolpeMetalico;

    private void Start()
    {

    }

    public void Hit(int DamageTaken)
    {
        HealthPoints = HealthPoints - DamageTaken;
        Instantiate(SonidoGolpeMetalico);
        if (HealthPoints == 0)
        {
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
           
        }

    }

}
