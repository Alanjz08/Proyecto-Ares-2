using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivation : MonoBehaviour
{

    public GameObject Boss;

    private void Start()
    {
        Boss.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BossUI.instance.BossActivator();
            Boss.SetActive(true);
        }
    }

    //IEnumerator WaitForBoss()
    //{
        
    //}
    

}