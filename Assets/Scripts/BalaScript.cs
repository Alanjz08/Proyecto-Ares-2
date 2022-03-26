using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaScript : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    public float speed;
    private Vector2 Direction;
    private int BalaDamage=1;

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
        GuraMov gura=collision.GetComponent<GuraMov>();
        Tridente tridente = collision.GetComponent<Tridente>();
        
        if(gura!=null)
        {

            gura.Hit(BalaDamage);
            Destroy();
        }

        if (tridente != null)
        {
            Destroy();
        }
    }
    
}
