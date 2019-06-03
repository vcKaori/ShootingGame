using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour
{

    public static int points = 0;
    GameObject scoreText;


    void Start()
    {
        this.scoreText = GameObject.Find("Score");
    }

    void Update()
    {
        scoreText.GetComponent<Text>().text = "Score:  " + points.ToString("D5");
    }
}