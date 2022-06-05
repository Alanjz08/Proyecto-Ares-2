using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HealthPoints;
    public int Speed;
    private Animator animator;
    public GameObject SonidoGolpeMetalico;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Hit(int DamageTaken)
    {
        HealthPoints = HealthPoints - DamageTaken;
        animator.SetTrigger("TD");
        Instantiate(SonidoGolpeMetalico);
        if (HealthPoints == 0)
        {
            //    animator.SetTrigger("Muerte");
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;

        }

    }

}
