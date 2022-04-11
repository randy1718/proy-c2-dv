using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] GameObject player;
    AIPath MyPath;
    // Start is called before the first frame update
    void Start()
    {
        MyPath = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer(); 
    }

    void ChasePlayer()
    {
        float distancia = Vector2.Distance(transform.position,player.transform.position);
        if (distancia < 8)
        {
            //Debug.Log("Persiguiendo");
        }
        Debug.DrawRay(transform.position,player.transform.position,Color.red);
        Collider2D col = Physics2D.OverlapCircle(transform.position,8f,LayerMask.GetMask("Player"));
        if (col != null)
        {
            MyPath.isStopped = false;
            //Debug.Log(col.gameObject.name);
            //Debug.Log("Persiguiendo");
        }
        else
        {
            MyPath.isStopped = true;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,6f);
    }
}
