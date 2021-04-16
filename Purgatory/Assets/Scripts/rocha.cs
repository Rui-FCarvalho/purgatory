using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocha : MonoBehaviour {

    public GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {
            player.transform.position = new Vector2(-24, 1);
            SceneManager.LoadScene("Boss");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (player.GetComponent<PlayerController>().nxtlevel == 0)
            {

                player.transform.position = new Vector2(-21, 1);
                SceneManager.LoadScene("Dungeon2");
                player.GetComponent<PlayerController>().nxtlevel++;
            }
            else
            {
                player.transform.position = new Vector2(-24, 1);
                SceneManager.LoadScene("Boss");
            }
        }
    }
}
