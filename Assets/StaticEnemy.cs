using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    [SerializeField] float life;
    [SerializeField] GameObject SE_bullet;
    [SerializeField] float intervaloSeg = 1;
    Animator myAnim;
    float timer;
    bool isBroken = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
        myAnim = GetComponent<Animator>();
        StartCoroutine(destroyEnemy());
    }

    IEnumerator destroyEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (isBroken)
            {
                Debug.Log("muertoooo!!!!!!!!!!!!!!!!!!!");
                Destroy(gameObject);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.left, 7f, LayerMask.GetMask("Player"));
        if (ray)
        {
            myAnim.SetBool("isNear", true);
            if (Time.time > timer)
            {
                timer = Time.time + intervaloSeg;
                Instantiate(SE_bullet, new Vector3(transform.position.x + 0.4f, transform.position.y + 0.1f, -1), transform.rotation);
            }
        }
        else
        {
            myAnim.SetBool("isNear", false);
        }
        Debug.DrawRay(transform.position, Vector2.left * 7f, Color.red);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            if (life == 0)
            {
                myAnim.SetBool("isBroken", true);
                isBroken = true;
            }
            else
            {
                life--;
            }
        }
    }
}
