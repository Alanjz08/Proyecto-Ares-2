using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Activa el panel de la barra de vida del jefe y reproduce la musica

public class BossUI : MonoBehaviour
{

    public GameObject Bosspanel;
    public GameObject Musica;
    //Objeto de la puerta que se va a abrir cuando exista
    //public GameObject puerta;

    public static BossUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Bosspanel.SetActive(false);
        //puerta.SetActive(false);
    }

    public void BossActivator()
    {
        Bosspanel.SetActive(true);
        Instantiate(Musica);
        //puerta.SetActive(true);
    }

    public void BossDeactivator()
    {
        Bosspanel.SetActive(false);
        //puerta.SetActive(false)
    }
}
