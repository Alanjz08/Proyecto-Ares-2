using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tridente : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    public float speed;
    private Vector2 Direction;
    private int TridentDamage =1;
   
    // Obtenemos el componente del rigibody
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // La velocida del rigidbody sera igual a la dirección por la velocidad (modificable)
    void Update()
    {
        Rigidbody2D.velocity = Direction * speed;
    }
  
    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    //Destruimos el tridente
    public void Destroy()
    {
        Destroy(gameObject);
        
    }

    //Si colisionamos con un zirc verde, volador o el enemigo entonces llamamos a su función
    //Hit respectiva, en caso de que choque, se eliminará el tridente.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        ZircScript zirc = collision.GetComponent<ZircScript>();
        VScript volador = collision.GetComponent<VScript>();
        Enemy boss = collision.GetComponent<Enemy>();

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
        if (boss != null)
        {
            boss.Hit(TridentDamage);
            Destroy();
        }

    }
    

}
