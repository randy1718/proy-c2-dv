using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D body;
    Animator myAnim;
    GameObject player;
    [SerializeField] float speed;
    [SerializeField] AudioClip audioBoom;
    Vector2 directionBullet;
    bool isColliding = false;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(destroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        bulletDirection();
        RaycastHit2D ray = Physics2D.Raycast(transform.position, directionBullet, 0.3f, LayerMask.GetMask("Ground"));
        if (ray)
        {
            speed = 0;
            myAnim.SetBool("isColliding", true);
            isColliding = true;
        }
        Debug.DrawRay(transform.position, directionBullet * 0.5f, Color.red);
    }

    IEnumerator destroyBullet()
    {
        while (true)
        {

            yield return new WaitForSeconds(0.3f);
            if (isColliding)
            {
                AudioSource.PlayClipAtPoint(audioBoom,transform.position);
                Destroy(gameObject);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Level" || collision.gameObject.tag == "Flying enemy" || collision.gameObject.tag == "Static enemy")
        {
            body.bodyType = RigidbodyType2D.Static;
            myAnim.SetBool("isColliding", true);
            isColliding = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Static enemy")
        {
            body.bodyType = RigidbodyType2D.Static;
            myAnim.SetBool("isColliding", true);
            isColliding = true;
        }
    }

        public void bulletDirection()
    {
        if (player.transform.localScale.x == -1)
        {
            directionBullet = Vector2.left;
            //body.AddForce(directionBullet * speed);
        }
        else
        {
            directionBullet = Vector2.right;
            //body.AddForce(directionBullet * speed);
        }
    }

}
