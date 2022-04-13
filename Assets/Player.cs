using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D myBody;
    Animator myAnim;
    [SerializeField] float speed;
    [SerializeField] float life;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject bullet;
    bool isDead = false;
    bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        StartCoroutine(ShowTime());

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
    }

    IEnumerator ShowTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Debug.Log(Time.time);
        }
    }

    void Fire()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            myAnim.SetLayerWeight(1,1);
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Instantiate(bullet, new Vector3(transform.position.x + 0.4f, transform.position.y + 0.2f, -1), transform.rotation);
            }
        }
        else
        {
            new WaitForSeconds(4);
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                myAnim.SetBool("IsJumping", true);
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
                isDead = true;
            }
            else
            {
                life--;
            }

        }

        if (collision.gameObject.tag == "Flying enemy")
        {
            myAnim.SetBool("isDead", true);
            isDead = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "SE Bullet(Clone)")
        {
            if (life == 0)
            {
                myAnim.SetBool("isDead", true);
                isDead = true;
            }
            else
            {
                life--;
            }
            
           
        }

        if (collision.gameObject.tag == "Flying enemy")
        {
            myAnim.SetBool("isDead", true);
            isDead = true;
        }
    }
}
