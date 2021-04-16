using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        Destroy(this.gameObject);
    }
}
