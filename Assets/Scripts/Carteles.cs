using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class Carteles : MonoBehaviour
    {
        public GameObject informacion;

        public bool informacionHabilitada;
        //public bool mostrarInformacionHabilitada;

        //public LayerMask personaje;

        // Start is called before the first frame update
        void Start()
        {
            informacion.gameObject.SetActive(false);


        }

        // Update is called once per frame
        void Update()
        {
        //informacionHabilitada = Physics2D.OverlapCircle(this.transform.position, 1f, personaje);

            //if (informacionHabilitada == true)
            //{
            //    informacion.gameObject.SetActive(true);
            //}
            //if (informacionHabilitada == false)
            //{
            //    informacion.gameObject.SetActive(false);
            //}
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

    //public void Activar()
    //{

    //}
}

