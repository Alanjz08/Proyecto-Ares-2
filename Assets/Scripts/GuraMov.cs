using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class GuraMov : MonoBehaviour
    {
        //HitBox Meles
        public Transform attackPosS;
        public Transform attackPosB;
        public Transform attackPosG;
        public float attackRangeX;
        public float attackRangeY;
        public float attackRangeXGround;
        public float attackRangeYGround;
        //Pasarle el enemigo
        public LayerMask whatIsEnemies;
        //Nuestro daño
        public int damage;
        //Collider con enemigos
        Collider2D[] enemiesToDamage;


        //PATANLLA DE MUERTE
        public GameObject gameOverImg;

        //Stun
        private float dazedTime;
        public float startDazedTime;

        //Prefab del tridente
        public GameObject TridentePrefab;

        //Salto
        public float JumpForce;
        private bool Grounded;

        public bool Betterjump = false;
        public float fallMultiplier = 0.5f;
        public float lowJumpMultiplier = 1;
        //Movimiento lateral 
        private Rigidbody2D Rigidbody2D;
        private float Horizontal;

        //CD Disparo del tridente
        public float LastShoot;
        //CD Todos los meles
        public float LastHit;

        //Animador
        private Animator animator;

        //Vida
        private int Health = 5;
        public GameObject[] hearts;

        //Sonidos 
        public GameObject SonidoCorte;
        public GameObject SonidoLanzamiento;
        public GameObject SonidoAterrizaje;
        public GameObject SonidoGolpeMetalico;

        private void Start()
        {
            gameOverImg.SetActive(false);
            Rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            //Stun cuando recibimos daño 
            if (dazedTime <= 0)
            {
                //Cambia el sentido del personaje dependiendo si ve a la izquierda o derecha, tambien su prefab
                Horizontal = Input.GetAxisRaw("Horizontal");
                if (Horizontal < 0.0f)
                {
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    TridentePrefab.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                }
                else if (Horizontal > 0.0f)
                {
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    TridentePrefab.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
                animator.SetBool("Running", Horizontal != 0.0f);

            }
            else
            {
                Horizontal = 0;
                dazedTime -= Time.deltaTime;
            }

            //Crea un raycast que toca el suelo para determinar si esta en el piso o no
            if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
            {
                Grounded = true;

            }
            else
            {
                Grounded = false;
            }

            //Si esta en el piso y presionamos w podemos saltar
            if (Input.GetKeyDown(KeyCode.W) && Grounded == true)
            {
                Jump();
            }

            //Si esta en el piso y presiona K lanza el tridente
            if (Input.GetKeyDown(KeyCode.K) && Grounded == true && Time.time > LastShoot + 0.5f)
            {
                Attack();
                Instantiate(SonidoLanzamiento);
                LastShoot = Time.time;
                dazedTime = startDazedTime;
            }

            //Si esta en el piso y presiona j ataca
            if (Input.GetKeyDown(KeyCode.J) && Grounded == true && Time.time > LastHit + 0.5f)
            {
                LastHit = Time.time;
                animator.SetTrigger("GAttack");
            }

            //J y no esta en el piso ataque hacia arriba 

            if (Input.GetKeyDown(KeyCode.J) && Grounded == false && Time.time > LastHit + 0.5f)
            {
                LastHit = Time.time;
                animator.SetTrigger("FlyS");

            }

            //K y no estamos en el piso ataque hacia abajo y su CD

            if (Input.GetKeyDown(KeyCode.K) && Grounded == false && Time.time > LastHit + 0.5f)
            {
                LastHit = Time.time;
                animator.SetTrigger("FlyB");

            }


        }


        //Función saltar
        private void Jump()
        {
        if (Rigidbody2D == null) return;
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
        }

        //Caminar
        private void FixedUpdate()
        {
        if (Rigidbody2D == null) return;
            Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
            if (Betterjump)
            {
                if (Rigidbody2D.velocity.y < 0)
                    Rigidbody2D.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier) * Time.deltaTime;

                if (Rigidbody2D.velocity.y > 0 && !Input.GetKey("w"))
                    Rigidbody2D.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }
        }

        //Función lanzar tridente
        private void Attack()
        {
            Vector3 direction;
            if (transform.localScale.x == 1.0f) direction = Vector3.right;
            else direction = Vector3.left;
            animator.SetTrigger("Attack");
            GameObject tridente = Instantiate(TridentePrefab, transform.position + direction * 0.1f, Quaternion.identity);
            tridente.GetComponent<Tridente>().SetDirection(direction);
        }
        public void GroundAttack()
        {
            Instantiate(SonidoCorte);
            enemiesToDamage = Physics2D.OverlapBoxAll(attackPosG.position, new Vector2(attackRangeXGround, attackRangeYGround), 0, whatIsEnemies);
            if (enemiesToDamage == null) return;
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {

                ZircScript zirc = enemiesToDamage[i].GetComponent<ZircScript>();
                VScript volador = enemiesToDamage[i].GetComponent<VScript>();

                if (zirc != null)
                {
                    enemiesToDamage[i].GetComponent<ZircScript>().Hit(damage);
                    Instantiate(SonidoGolpeMetalico);
                }
                if (volador != null)
                {
                    enemiesToDamage[i].GetComponent<VScript>().Hit(damage);
                    Instantiate(SonidoGolpeMetalico);
                }
            }
        }
        public void FlySAttack()
        {
            Instantiate(SonidoCorte);
            enemiesToDamage = Physics2D.OverlapBoxAll(attackPosS.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
            if (enemiesToDamage == null) return;
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                ZircScript zirc = enemiesToDamage[i].GetComponent<ZircScript>();
                VScript volador = enemiesToDamage[i].GetComponent<VScript>();

                if (zirc != null)
                {
                    enemiesToDamage[i].GetComponent<ZircScript>().Hit(damage);
                    Instantiate(SonidoGolpeMetalico);

                }
                if (volador != null)
                {
                    enemiesToDamage[i].GetComponent<VScript>().Hit(damage);
                    Instantiate(SonidoGolpeMetalico);

                }


            }
        }
        public void FlyBAttack()
        {
            Instantiate(SonidoCorte);
            enemiesToDamage = Physics2D.OverlapBoxAll(attackPosB.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
            if (enemiesToDamage == null) return;
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                ZircScript zirc = enemiesToDamage[i].GetComponent<ZircScript>();
                VScript volador = enemiesToDamage[i].GetComponent<VScript>();

                if (zirc != null)
                {
                    enemiesToDamage[i].GetComponent<ZircScript>().Hit(damage);
                    Instantiate(SonidoGolpeMetalico);
                }
                if (volador != null)
                {
                    enemiesToDamage[i].GetComponent<VScript>().Hit(damage);
                    Instantiate(SonidoGolpeMetalico);
                }
            }
        }

        //Función RECIBIR daño;
        public void Hit(int DamageTaken)
        {
            dazedTime = startDazedTime;
            Health = Health - DamageTaken;
            animator.SetTrigger("TakingDamage");
            if (Health < 1)
            {
                Destroy(hearts[0].gameObject);
                animator.SetTrigger("Muerta");
                //Time.timeScale = 0;
                gameOverImg.SetActive(true);
                Destroy(this);
            }
            else if (Health < 2)
            {
                Destroy(hearts[1].gameObject);
            }
            else if (Health < 3)
            {
                Destroy(hearts[2].gameObject);
            }
            else if (Health < 4)
            {
                Destroy(hearts[3].gameObject);
            }
            else if (Health < 5)
            {
                Destroy(hearts[4].gameObject);
            }


        }
    public void Stun()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(Rigidbody2D);
    }


        //Dibujar hitbox meles
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackPosB.position, new Vector3(attackRangeX, attackRangeY, 1));
            Gizmos.DrawWireCube(attackPosS.position, new Vector3(attackRangeX, attackRangeY, 1));
            Gizmos.DrawWireCube(attackPosG.position, new Vector3(attackRangeXGround, attackRangeYGround, 1));
        }


    }
