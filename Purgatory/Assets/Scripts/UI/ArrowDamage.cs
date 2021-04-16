using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrowDamage : MonoBehaviour {

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
        textmeshPro.text = player.GetComponent<PlayerController>().ardmg.ToString();
    }
}
