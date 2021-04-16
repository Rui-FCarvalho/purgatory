using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCounter : MonoBehaviour {

    Image image;
    public Sprite[] pentagram;
    GameObject player;

	// Use this for initialization
	void Start () {
        image = this.gameObject.GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
         image.sprite=pentagram[player.GetComponent<PlayerController>().lifes];
	}
}
