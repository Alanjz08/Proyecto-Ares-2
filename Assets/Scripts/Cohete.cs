using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohete : MonoBehaviour
{
    public float speed = 0.5f;
    public Transform[] moveSpots;
    private float waitTime;
    private float startWaitTime = 2;
    private int i = 0;
    private Animator animator;
    public float delay;
    GuraMov gura;
    public bool AllCollected = false;
   
    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        animator = GetComponent<Animator>();
      

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].transform.position, speed * Time.deltaTime);
        if (gura != null)
        {
            if (AllCollected==true)
            {
                gura.Stun();
                waitTime -= Time.deltaTime;
                if (waitTime <= 0)
                {
                    animator.SetTrigger("Iniciando");
                }
            }
            else
            {
                Debug.Log("No tienes todas las gasolinas");
            }

        }
        

        if (moveSpots[i] == moveSpots[moveSpots.Length - 1])
        {
            Debug.Log("Ganaste");
        }
    }

    private void Despega()
    {
        animator.SetTrigger("Volando");
        if (Vector2.Distance(transform.position, moveSpots[i].transform.position) < 0.1f)
        {

            if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
            {
                i++;
            }

        }
    }
   public void Got()
    {
        AllCollected = true;
    }
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gura = collision.collider.GetComponent<GuraMov>();

        collision.collider.transform.SetParent(transform);

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(null);
    }
}
