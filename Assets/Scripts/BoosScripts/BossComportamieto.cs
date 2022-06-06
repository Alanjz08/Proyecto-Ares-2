using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Determina el comportamiento del jefe

public class BossComportamieto : MonoBehaviour
{
    public Transform[] transforms;
    public GameObject EnergyBall;
    public GameObject Personaje;
    private Animator animator;

    public float timetoShoot, countdown;
    public float timetoTP, countdownToTp;
    public float Bosshealth, CurrentBossHealth;
    public Image HealthImg;

    private void Start()
    {
        animator = GetComponent<Animator>();
        var initialPosicion = Random.Range(0, transforms.Length);
        transform.position = transforms[initialPosicion].position;
        countdown = timetoShoot;
        countdownToTp = timetoTP;
    }

    public void Update()
    {
        BossScale();
        DamageBoss();

        countdown -= Time.deltaTime;
        countdownToTp -= Time.deltaTime;

        if (countdown <= 0f)
        {
            animator.SetTrigger("BAtaca");
            //ShootPlayer();
            
            
        }

        if (countdownToTp <= 0f)
        {
            Teleport();
            countdownToTp = timetoTP;
        }

       
    }

   

    public void ShootPlayer()
    {
        //Instantiate(SonidoLaserGun);
        GameObject Disparo = Instantiate(EnergyBall, transform.position, Quaternion.identity);
        countdown = timetoShoot;
    }

    public void Teleport()
    {
        var initialPosicion = Random.Range(0, transforms.Length);
        transform.position = transforms[initialPosicion].position;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void DamageBoss()
    {
        CurrentBossHealth = GetComponent<Enemy>().HealthPoints;
        HealthImg.fillAmount = CurrentBossHealth / Bosshealth;

        if (CurrentBossHealth == 0)
        {
            animator.SetTrigger("Muerte");
            BossUI.instance.BossDeactivator();
            //Destroy(gameObject);
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }
    }

    public void BossScale()
    {
        if(transform.position.x > Personaje.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
   
}
