using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaScript : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    public float speed;
    private Vector2 Direction;
    private int BalaDamage=1;

    //Obtenemos el componente rigidbody
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // La velocidad de la bala sera igual a la dirección por speed, que podemos modificar
    void Update()
    {
        Rigidbody2D.velocity = Direction * speed;
    }


    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    //Método para destruir la bala
    public void Destroy()
    {
        Destroy(gameObject);

    }
    
    //Si colisiona con el personaje o con un tridente la bala de destruye y llama al método hit del personaje
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
