using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {

    public GameObject arrow;
    public float speed;
    private GameObject player;
    GameObject Arrow;
    public int nArrows;

    public float reloadseconds;
    private float nextfire;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.RightShift) )
        {
                if (nArrows > 0 && Time.time > nextfire)
                {
                    nextfire = Time.time + reloadseconds;
                    shoot();
                }
        }


    }
    void shoot()
    {

        nArrows--;
        
        if (player.GetComponent<PlayerController>().direction == 1)
        {
            Arrow = Instantiate(arrow, this.transform.position, Quaternion.identity);
            Arrow.GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        }
        if (player.GetComponent<PlayerController>().direction == 0)
        {
            Arrow = Instantiate(arrow, this.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 180f));
            Arrow.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
        }
        if (player.GetComponent<PlayerController>().direction == 2)
        {
            Arrow = Instantiate(arrow, this.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
            Arrow.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
        }
        if (player.GetComponent<PlayerController>().direction == 3)
        {
            Arrow = Instantiate(arrow, this.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, -90f));
            Arrow.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        }
    }
}
