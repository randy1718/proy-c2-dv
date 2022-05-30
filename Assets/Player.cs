using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D myBody;
    Animator myAnim;
    GameObject enemiesCount;
    [SerializeField] float speed;
    [SerializeField] float life;
    [SerializeField] float speedBullet;
    [SerializeField] float jumpForce;
    [SerializeField] AudioClip audioBoom;
    [SerializeField] GameObject bullet;
    [SerializeField] float intervaloSeg;
    [SerializeField] AudioClip audioDeath;
    [SerializeField] AudioClip audioJump;
    float timer;
    float animationLayerCooldown;
    bool isDead;
    bool isDeadSound;
    bool isGrounded = false;
    bool fShoot = false;
    Vector2 directionBullet;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        isDeadSound = false;
        timer = Time.time;
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        StartCoroutine(FinishingShoot());
        enemiesCount = GameObject.FindGameObjectWithTag("EnemiesNumber");
        //StartCoroutine(ReloadGame());
        //StartCoroutine(PlaySoundDeath());
        Time.timeScale = 1;
        directionBullet = Vector2.right;

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position,Vector2.down,1.2f,LayerMask.GetMask("Ground"));
        //Debug.Log("Colisionando con: " + ray.collider.gameObject.name);
        Debug.DrawRay(transform.position,Vector2.down * 0.9f,Color.red);
        isGrounded = ray.collider != null;
        Jump();
        Fire();
        if (!myAnim.GetBool("IsJumping") && !isGrounded)
        {
            myAnim.SetBool("IsFalling", true);
        }
        else if (isGrounded) 
        {
            myAnim.SetBool("IsFalling", false);
        }

        if (transform.localScale.x == 1)
        {
            directionBullet = Vector2.right;
        }
        else if (transform.localScale.x == -1)
        {
            directionBullet = Vector2.left;
        }

        Debug.Log(enemiesCount.GetInstanceID());
    }

    IEnumerator FinishingShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if (fShoot == true )
            {
                myAnim.SetLayerWeight(1, 0);
                fShoot = false;
            }
        }
    }

    IEnumerator ReloadGame()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(2f);
            if (isDead == true)
            {
                SceneManager.LoadScene(2);
                isDead = false;
            }
        }
    }
    IEnumerator PlaySoundDeath()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            if (isDeadSound == true)
            {
                isDead = true;
                StartCoroutine(ReloadGame());
                isDeadSound = false;
            }
        }
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Time.time > timer && fShoot == false)
        {
            myAnim.SetLayerWeight(1,1);
            GameObject bulletI = Instantiate(bullet, new Vector3(transform.position.x + 0.4f, transform.position.y + 0.2f, -1), transform.rotation);
            Rigidbody2D rb = bulletI.GetComponent<Rigidbody2D>();
            AudioSource.PlayClipAtPoint(audioBoom, transform.position);
            rb.AddForce(directionBullet * speedBullet,ForceMode2D.Force);
            timer = Time.time + intervaloSeg;
            animationLayerCooldown = timer;
            //fShoot = true;
        }
        else if (Time.time > animationLayerCooldown)
        {
            myAnim.SetLayerWeight(1, 0);
        }
    }

    void FinishRun()
    {
        Debug.Log("Termino de correr!"); 
    }

    void Jump()
    {
        if (isGrounded && !myAnim.GetBool("IsJumping"))
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Space))
            {
                myBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                myAnim.SetBool("IsJumping", true);
                AudioSource.PlayClipAtPoint(audioJump, transform.position);
            }
        }

        if (myBody.velocity.y < 5 && isGrounded)
        {
            myAnim.SetBool("IsJumping", false);
        }
    }

    private void FixedUpdate()
    {
        float dirH = Input.GetAxis("Horizontal");

        if (dirH != 0)
        {
            myAnim.SetBool("IsRunning",true);
            if (dirH < 0)
            {
                transform.localScale = new Vector2(-1,1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
        }
        else
        {
            myAnim.SetBool("IsRunning", false);
        }
        myBody.velocity = new Vector2(dirH*speed,myBody.velocity.y);

       /* if (Input.GetKeyDown(KeyCode.Space))
        {
            myBody.AddForce(new Vector2(0,20f), ForceMode2D.Impulse);
        }*/
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "SE Bullet(Clone)")
        {
            if (life == 0)
            {
                myAnim.SetBool("isDead", true);
                isDeadSound = true;
                AudioSource.PlayClipAtPoint(audioDeath, transform.position);
                StartCoroutine(PlaySoundDeath());
                Invoke("StopGame", 0.4f);
            }
            else
            {
                life--;
            }

        }

        if (collision.gameObject.tag == "Flying enemy")
        {
            myAnim.SetBool("isDead", true);
            Invoke("StopGame", 0.7f);
            isDeadSound = true;
            AudioSource.PlayClipAtPoint(audioDeath, transform.position);
            StartCoroutine(PlaySoundDeath());

        }
    }

    void StopGame()
    {
        Time.timeScale = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "SE Bullet(Clone)")
        {
            if (life == 0)
            {
                myAnim.SetBool("isDead", true);
                isDeadSound = true;
                AudioSource.PlayClipAtPoint(audioDeath, transform.position);
                StartCoroutine(PlaySoundDeath());
                Invoke("StopGame", 0.4f);
            }
            else
            {
                life--;
            }
            
           
        }

        if (collision.gameObject.tag == "Flying enemy")
        {
            myAnim.SetBool("isDead", true);
            Invoke("StopGame", 0.7f);
            AudioSource.PlayClipAtPoint(audioDeath, transform.position);
            StartCoroutine(PlaySoundDeath());
            isDeadSound = true;
        }
    }
}
