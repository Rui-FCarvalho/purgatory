using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour {

    public GameObject[] drops;
    public int droprate;

    public void Drop()
    {
        int rng = Random.Range(1, 100);

        if (rng <= droprate) 
        {
            int rand = Random.Range(0, drops.Length);
            Instantiate(drops[rand], transform.position, Quaternion.identity);
        }
    }
}