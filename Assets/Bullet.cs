using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D body;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        body.velocity = new Vector2(20, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("pruebas");
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Level")
        {
            Destroy(gameObject);
        }
    }

}
