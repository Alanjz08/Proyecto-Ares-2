using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gasolina : MonoBehaviour
{
    //Cuando la gasolina tenga contacto con un jugadir desaparece
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
        }
    }
}
