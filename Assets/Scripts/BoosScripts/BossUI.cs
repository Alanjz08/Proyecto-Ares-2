using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{

    public GameObject Bosspanel;
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
        //puerta.SetActive(true);
    }

}
