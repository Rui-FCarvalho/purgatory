using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescan : MonoBehaviour {

    GameObject gameObj;
    private float wait = 2.0f;
    private bool timer = true;
	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update()
    {
        if (timer == true)
        {
            wait -= Time.deltaTime;
        }
        if (wait <= 0 && timer == true)
        {
            AstarPath.active.Scan();
            timer = false;
        }
    }
}
