using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedCounter : MonoBehaviour {

    //Text number;
    TextMeshProUGUI textmeshPro;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        //number = this.gameObject.GetComponent<Text>();
        textmeshPro = this.gameObject.GetComponent<TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        textmeshPro.text = player.GetComponent<PlayerController>().speed.ToString();
    }
}
