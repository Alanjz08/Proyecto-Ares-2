using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //Si tenemos contacto con un checkpoint se activa, se guarda su posicion y se prende la animación
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayeRespawn>().reachedCheckPoint(transform.position.x,transform.position.y);
            GetComponent<Animator>().enabled = true;


           
        }
    }
}
