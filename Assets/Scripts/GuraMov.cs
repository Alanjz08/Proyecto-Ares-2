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
        //Nuestro da�o
        public int damage;
        //Collider con enemigos
        Collider2D[] enemiesToDamage;


        //PANTALLA DE MUERTE
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
        public static GuraMov instance;
        //Vida
        public int Health = 5;
        public GameObject[] hearts;

        //Sonidos 
        public GameObject SonidoCorte;
        public GameObject SonidoLanzamiento;
        public GameObject SonidoAterrizaje;
        public GameObject SonidoGolpeMetalico;


        //Al iniciar el c�digo obtenemos los componentes que necesitamos y spawneamos el personaje en  la posici�n que deseamos.
        private void Start()
        {
            gameOverImg.SetActive(false);
            Rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            transform.position = (new Vector2(3.184f, -22.86f));
    }

        private void Update()
        {
            //Stun cuando recibimos da�o 
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


        //Funci�n saltar
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

        //Funci�n lanzar tridente
        private void Attack()
        {
            Vector3 direction;
            if (transform.localScale.x == 1.0f) direction = Vector3.right;
            else direction = Vector3.left;
            animator.SetTrigger("Attack");
            GameObject tridente = Instantiate(TridentePrefab, transform.position + direction * 0.1f, Quaternion.identity);
            tridente.GetComponent<Tridente>().SetDirection(direction);
        }

    //A partir de una hitbox determinamos si hay enemigos y los guardamos en un vector para posteriormente golpearlos a todos
        public void GroundAttack()
        {
            Instantiate(SonidoCorte);
            enemiesToDamage = Physics2D.OverlapBoxAll(attackPosG.position, new Vector2(attackRangeXGround, attackRangeYGround), 0, whatIsEnemies);
            if (enemiesToDamage == null) return;
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {

                ZircScript zirc = enemiesToDamage[i].GetComponent<ZircScript>();
                VScript volador = enemiesToDamage[i].GetComponent<VScript>();
                Enemy boss = enemiesToDamage[i].GetComponent<Enemy>();    

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
                if (boss!=null)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().Hit(damage);
                    Instantiate(SonidoGolpeMetalico);
                }
            }
        }

    //A partir de una hitbox determinamos si hay enemigos y los guardamos en un vector para posteriormente golpearlos a todos
    public void FlySAttack()
        {
            Instantiate(SonidoCorte);
            enemiesToDamage = Physics2D.OverlapBoxAll(attackPosS.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
            if (enemiesToDamage == null) return;
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                ZircScript zirc = enemiesToDamage[i].GetComponent<ZircScript>();
                VScript volador = enemiesToDamage[i].GetComponent<VScript>();
                Enemy boss = enemiesToDamage[i].GetComponent<Enemy>();

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
                if (boss != null)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().Hit(damage);
                    Instantiate(SonidoGolpeMetalico);
                }


        }
        }

    //A partir de una hitbox determinamos si hay enemigos y los guardamos en un vector para posteriormente golpearlos a todos
    public void FlyBAttack()
        {
            Instantiate(SonidoCorte);
            enemiesToDamage = Physics2D.OverlapBoxAll(attackPosB.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
            if (enemiesToDamage == null) return;
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                ZircScript zirc = enemiesToDamage[i].GetComponent<ZircScript>();
                VScript volador = enemiesToDamage[i].GetComponent<VScript>();
                Enemy boss = enemiesToDamage[i].GetComponent<Enemy>();

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
                if (boss != null)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().Hit(damage);
                    Instantiate(SonidoGolpeMetalico);
                }
        }
        }

        //Funci�n RECIBIR da�o;
        public void Hit(int DamageTaken)
        {
            dazedTime = startDazedTime;
            Health = Health - DamageTaken;
            animator.SetTrigger("TakingDamage");
        
            if (Health < 1)
            {
            //Si muere el personaje lo spawneamos en ultimo checkpoint y le devolvemos la vida a 5, volvemos a poner corazones
                hearts[0].gameObject.SetActive(false);
            
            transform.position = (new Vector2(PlayerPrefs.GetFloat("checkPointPositionX"), PlayerPrefs.GetFloat("checkPointPositionY")));
            Health = 5;
            
                for(int i=0; i < 5; i++)
                {
                    hearts[i].gameObject.SetActive(true);
                }
        }
            else if (Health < 2)
            {
                hearts[1].gameObject.SetActive(false);
            
        }
            else if (Health < 3)
            {
                hearts[2].gameObject.SetActive(false);
           
        }
            else if (Health < 4)
            {
                hearts[3].gameObject.SetActive(false);
               
            }
            else if (Health < 5)
            {
                 hearts[4].gameObject.SetActive(false);
               

            }


        }
    



    //Desabilitamos el sprite para hacer que no se mueva el personaje
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
