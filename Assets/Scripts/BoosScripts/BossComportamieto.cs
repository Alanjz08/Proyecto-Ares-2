using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossComportamieto : MonoBehaviour
{
    public Transform[] transforms;
    public GameObject EnergyBall;

    public float timetoShoot, countdown;
    public float timetoTP, countdownToTp;

    public float Bosshealth, CurrentBossHealth;
    public Image HealthImg;

    private void Start()
    {
        var initialPosicion = Random.Range(0, transforms.Length);
        transform.position = transforms[initialPosicion].position;
        countdown = timetoShoot;
        countdownToTp = timetoTP;
    }

    public void Update()
    {
        DamageBoss();

        countdown -= Time.deltaTime;
        countdownToTp -= Time.deltaTime;

        if (countdown <= 0f)
        {
            ShootPlayer();
            countdown = timetoShoot;
            
        }

        if (countdownToTp <= 0f)
        {
            Teleport();
            countdownToTp = timetoTP;
        }
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;
       //Instantiate(SonidoLaserGun);
        GameObject bala = Instantiate(EnergyBall, transform.position + direction * 0.1f, Quaternion.identity);
        bala.GetComponent<BalaScript>().SetDirection(direction);
    }

    public void ShootPlayer()
    {   
        GameObject Disparo = Instantiate(EnergyBall, transform.position, Quaternion.identity);   
    }

    public void Teleport()
    {
        var initialPosicion = Random.Range(0, transforms.Length);
        transform.position = transforms[initialPosicion].position;
    }

    public void DamageBoss()
    {
        CurrentBossHealth = GetComponent<Enemy>().HealthPoints;
        HealthImg.fillAmount = CurrentBossHealth / Bosshealth;
    }

   
}
