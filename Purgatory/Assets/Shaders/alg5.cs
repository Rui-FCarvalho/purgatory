using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class alg5 : MonoBehaviour
{

    private Material mat;
    //private GameObject player;
    void Awake()
    {
        mat = new Material(Shader.Find("tp2/alg5"));
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

            Graphics.Blit(source, destination, mat);
        
    }

}
