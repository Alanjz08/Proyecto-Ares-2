using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    //Gas
    public Text totalGas;
    public Text GasCollected;
    private int totalGasInLevel;
    public bool allCollected = false;
    public Cohete cohete;

    private void Start()
    {
        //El total del gas seran la cantidad de hijos del manager en la escena
        totalGasInLevel = transform.childCount;
        cohete = GameObject.FindGameObjectWithTag("Cohete").GetComponent<Cohete>();

    }

    private void Update()
    {
        //Mostramos el total de gasolinas
        totalGas.text = totalGasInLevel.ToString();

        //Al recojer una gasolina se actualiza nuestro contador
        if (transform.childCount==5 )
        {
            GasCollected.text = 0.ToString();
        }
        else if(transform.childCount == 4)
        {
            GasCollected.text = 1.ToString();
        }
        else if (transform.childCount == 3)
        {
            GasCollected.text = 2.ToString();
        }
        else if (transform.childCount == 2)
        {
            GasCollected.text = 3.ToString();
        }
        else if (transform.childCount == 1)
        {
            GasCollected.text = 4.ToString();
        }
        else if (transform.childCount == 0)
        {
            GasCollected.text = 5.ToString();
            cohete.Got();
        }
    }
   
    
}
