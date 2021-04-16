using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public GameObject[] wall;
    public GameObject spawner;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            spawner.SetActive(true);

            for (int i = 0; i < wall.Length; i++)
            {
                wall[i].SetActive(true);
            }
            Destroy(this.GetComponent<BoxCollider2D>());
        }
    }

    private void Update()
    {
        if(this.gameObject.transform.childCount <= wall.Length)
        {
            for (int i = 0; i < wall.Length; i++)
            {
                wall[i].SetActive(false);
            }
        }
    }
}
