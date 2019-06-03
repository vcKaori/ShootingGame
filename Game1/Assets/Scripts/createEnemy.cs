using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createEnemy : MonoBehaviour
{
    private float timeElapsed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed -= Time.deltaTime;
        if (timeElapsed <= 0)
        {
            timeElapsed = 5f;
            GameObject enemy = (GameObject)Resources.Load("Enemy");
            float height = Random.Range(-3.5f, 3.5f);
            Instantiate(enemy, new Vector3(8.6f, height, 0), Quaternion.Euler(0, 0, 90));
            
        }
        
    }

}
