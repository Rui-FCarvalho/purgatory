using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static void gameOver()
    {
        SceneManager.LoadScene(4);
    }
    public static void dungeon()
    {
        SceneManager.LoadScene(1);
    }

    public static void dungeon2()
    {
        SceneManager.LoadScene("Dungeon Level 2");
    }

    public static void bossbattle()
    {
        SceneManager.LoadScene("Boss");
    }
}