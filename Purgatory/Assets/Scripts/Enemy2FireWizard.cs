using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2FireWizard : MonoBehaviour
{
    private GameObject player;
    public GameObject fireBallPrefab;
    private GameObject mage;
    GameObject fireBall;
    public float speed;
    public float fbSpeed;
    private float range;
    private Vector2 rayDirection;
    public float wait;
    private float waitT;
    private bool attacking = true;
    private bool running = false;
    private float rWait = 3f;
    private bool cd = false;
    private float cdTimer = 8f;
    public bool knockback = false;
    RaycastHit2D hit;

    private void Start()
    {
        waitT = wait;
        player = GameObject.FindGameObjectWithTag("Player");
        mage = this.gameObject;
    }

    void Update()
    {

        range = Vector2.Distance(transform.position, player.transform.position);

        Vector2 direction = (player.transform.position - transform.position).normalized;
        float angle = Vector2.Angle(direction, Vector2.up);

        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        rayDirection = player.transform.position - transform.position;
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
        hit = Physics2D.Raycast(transform.position, rayDirection, Mathf.Infinity, layerMask);

        if (hit.transform == player.transform)
        {
            this.GetComponent<Pathfinding.AIPath>().endReachedDistance = 2.4f;
            if (range <= 2.4f)
            {
                waitT -= Time.deltaTime;
            }

            if (range <= 2.4f && waitT <= 0 && attacking == true && running == false)
            {
                fireBall = Instantiate(fireBallPrefab, this.transform.position, Quaternion.LookRotation(Vector3.forward, direction));
               // fireBall.transform.rotation = Quaternion.LookRotation(direction);
                fireBall.GetComponent<Rigidbody2D>().velocity = direction * fbSpeed;
                //fireBall.GetComponent<Rigidbody2D>().transform.eulerAngles = new Vector2(0, angle);
                waitT = wait;
            }
        }
        else
        {
           this.GetComponent<Pathfinding.AIPath>().endReachedDistance = 0.1f;
        }

        if (range <= 0.6f && cd == false)
        {
           running = true;
        }

        if(running == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -1 * speed * Time.deltaTime);
            rWait -= Time.deltaTime;
        }

        if(rWait <= 0)
        {
            cd = true;
            running = false;
            rWait = 4f;
        }

        if (cd == true)
        {
            cdTimer -= Time.deltaTime;
            if(cdTimer <= 0)
            {
                cd = false;
                cdTimer = 8f;
            }
        }

        if(knockback == true)
        {
            attacking = false;
        }
        else
        {
            attacking = true;
        }
      
        //this.GetComponent<Pathfinding.AIPath>().endReachedDistance = 0.30f;
    }
}
