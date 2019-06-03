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
        // transformを取得
       // Transform myTransform = this.transform;

        // objを親として設定
       // myTransform.parent = homingObj.transform;

        time -= Time.deltaTime;
        if (time <= 0)
        {
            Destroy(this.gameObject);
        }
    }

   // public static void Attacked(GameObject c)
   // {
    //    homingObj = c;
   // }
}
