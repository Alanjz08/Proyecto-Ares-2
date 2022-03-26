using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    private int Damage = 1;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            GuraMov gura = collision.collider.GetComponent<GuraMov>();
            if (gura != null)
            {
                gura.Hit(Damage);
            }
        }
    }
}
