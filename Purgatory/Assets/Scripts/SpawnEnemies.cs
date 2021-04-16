using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {
    public GameObject skeletonPrefab;
    public GameObject smagePrefab;
    public GameObject parent;
    GameObject enemy;
    private int enemyNumber;
    private float posX;
    private float posY;
    private int enemySpawn;
    private bool Pos1 = false;
    private bool Pos2 = false;
    private bool Pos3 = false;
    private bool Pos4 = false;
    // Use this for initialization
    void Start () {

        enemyNumber = Random.Range(2, 5);

        for (int i = 0; i < enemyNumber; i++)
        {
            posX = Random.Range(0, 2) == 0 ? -15 : 15;
            posY = Random.Range(0, 2) == 0 ? -15 : 15;

            if(posX == 15 && posY == 15 && Pos1 == false)
            {
                Pos1 = true;
            }
            else if(posX == 15 && posY == 15 && Pos1 == true)
            {
                i--;
                continue;
            }

            if (posX == 15 && posY == -15 && Pos2 == false)
            {
                Pos2 = true;
            }
            else if (posX == 15 && posY == -15 && Pos2 == true)
            {
                i--;
                continue;
            }

            if (posX == -15 && posY == 15 && Pos3 == false)
            {
                Pos3 = true;
            }
            else if (posX == -15 && posY == 15 && Pos3 == true)
            {
                i--;
                continue;
            }

            if (posX == -15 && posY == -15 && Pos4 == false)
            {
                Pos4 = true;
            }
            else if (posX == -15 && posY == -15 && Pos4 == true)
            {
                i--;
                continue;
            }

            Vector2 Pos = new Vector2(this.transform.position.x + (posX * 0.1f), this.transform.position.y + (posY * 0.1f));

            enemySpawn = Random.Range(0, 2);

            if (enemySpawn == 0)
            {
                enemy = Instantiate(skeletonPrefab, Pos, Quaternion.identity);
                enemy.transform.SetParent(parent.transform, true);
            }
            if (enemySpawn == 1)
            {
                enemy = Instantiate(smagePrefab, Pos, Quaternion.identity);
                enemy.transform.SetParent(parent.transform, true);
            }

            if (i == enemyNumber - 1)
            {
                Destroy(this.gameObject);
            }

        }

    }

}
