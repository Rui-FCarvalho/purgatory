using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraArrow : MonoBehaviour {

    public GameObject player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<Bow>().nArrows += 5;

            Destroy(this.gameObject);
        }
    }
}
