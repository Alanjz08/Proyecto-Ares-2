using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Funcionalidad del dispoaro del jefe

public class EnergyBall : MonoBehaviour
{
    public float movSpeed;
    Rigidbody2D rb2d;
    Vector2 moveDirection;
    public GuraMov Target;
    public int damage;
    private Vector2 Direction;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        moveDirection = (Target.transform.position - transform.position).normalized * movSpeed;
        rb2d.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GuraMov gura = collision.GetComponent<GuraMov>();
        Tridente tridente = collision.GetComponent<Tridente>();

        if (gura != null)
        {

            gura.Hit(damage);
            Destroy();
        }

        if (tridente != null)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);


    }
}
