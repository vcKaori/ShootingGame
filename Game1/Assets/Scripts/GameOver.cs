using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour
{
    
    public static GameObject gameOver;


    void Start()
    {
        gameOver = GameObject.Find("GameOver");
    }

    void Update()
    {
        
    }

    public static void finish()
    {
        gameOver.GetComponent<Text>().text = "Game Over";
    }
}