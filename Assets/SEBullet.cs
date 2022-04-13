using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEBullet : MonoBehaviour
{
    Rigidbody2D body;
    [SerializeField] float speed;
    bool isColliding = false;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = Vector2.left * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision bala static con: ");
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Level" || collision.gameObject.name == "Megaman" || collision.gameObject.tag == "Flying enemy")
        {
            speed = 0;
            isColliding = true;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            speed = 0;
            isColliding = true;
            Destroy(gameObject);
        }
    }
}
