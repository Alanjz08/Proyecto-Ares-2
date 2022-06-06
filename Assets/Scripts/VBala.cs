using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VBala : MonoBehaviour
{
    //Bala zirc volador
    private Rigidbody2D Rigidbody2D;
    public float speed;
    private Vector2 Direction;
    private int BalaDamage = 1;
    private Animator animator;

    // Obtenemos los componentes del ridigbody2d y del animator
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // La velocidad del rigidbody sera la dirección por la velocidad
    void Update()
    {
        Rigidbody2D.velocity = Direction * speed;
    }
    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    //Se destruye al tocar el tridente o a gura
    public void Destroy()
    {
        Destroy(gameObject);

  
    }
   
    //Si se topa con gura o con un tridente llama a la función de daño de gura y se destruye
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GuraMov gura = collision.GetComponent<GuraMov>();
        Tridente tridente = collision.GetComponent<Tridente>();

        if (gura != null)
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
