using UnityEngine;
using System.Collections;

public class SmallExp : MonoBehaviour
{

    //GameObject Enemy;
    float time = 0.4f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        time -= Time.deltaTime;
        if (time <= 0)
        {
            Debug.Log(gameObject.transform.parent.gameObject.name);

            Destroy(this.gameObject);
        }
    }
}