using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tridente : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    public float speed;
    private Vector2 Direction;
    private int TridentDamage =1;
   
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.velocity = Direction * speed;
    }
  
    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void Destroy()
    {
        Destroy(gameObject);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        ZircScript zirc = collision.GetComponent<ZircScript>();
        VScript volador = collision.GetComponent<VScript>();
        
        if (zirc != null)
        {
            zirc.Hit(TridentDamage);
            Destroy();
        }
        if (volador != null)
        {
            volador.Hit(TridentDamage);
            Destroy();
        }
    }
    

}
