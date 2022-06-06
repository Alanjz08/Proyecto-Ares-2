using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script detecta si el jugador esta dentro del collider y desplega una imagen

    public class Carteles : MonoBehaviour
    {
        public GameObject informacion;

        public bool informacionHabilitada;
        

        void Start()
        {
            informacion.gameObject.SetActive(false);


        }

        
        void Update()
        {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            informacion.gameObject.SetActive(true);
        }
        else
        {
            informacion.gameObject.SetActive(false);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            informacion.gameObject.SetActive(false);
        }
        
    }

    
}

