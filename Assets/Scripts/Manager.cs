using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Text totalFruits;
    public Text FruitsCollected;
    private int totalFruitInLevel;
    public bool allCollected = false;
    public Cohete cohete;

    private void Start()
    {
       
        totalFruitInLevel = transform.childCount;
        cohete = GameObject.FindGameObjectWithTag("Cohete").GetComponent<Cohete>();

    }

    private void Update()
    {
        totalFruits.text = totalFruitInLevel.ToString();

        if(transform.childCount==5 )
        {
            FruitsCollected.text = 0.ToString();
        }
        else if(transform.childCount == 4)
        {
            FruitsCollected.text = 1.ToString();
        }
        else if (transform.childCount == 3)
        {
            FruitsCollected.text = 2.ToString();
        }
        else if (transform.childCount == 2)
        {
            FruitsCollected.text = 3.ToString();
        }
        else if (transform.childCount == 1)
        {
            FruitsCollected.text = 4.ToString();
        }
        else if (transform.childCount == 0)
        {
            FruitsCollected.text = 5.ToString();
            cohete.Got();
        }
    }
   
    
}
