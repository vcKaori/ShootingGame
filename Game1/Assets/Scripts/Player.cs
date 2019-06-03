using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 変数宣言.
    // 速度.
    public float ypos = 0;
    public float dir = 0;    //0~359°方向に移動.
    public float spd = 0; //速さ1.
    private float timeElapsed = 0f;
    float AttackedCnt = 5;
    public GameObject SExp;
    public GameObject Obj;
    public GameObject Ply;
    // Start is called before the first frame update
    void Start()
    {
        Ply = gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 min = GetWorldMin();    //カメラの左下座標.
        Vector2 max = GetWorldMax();    //カメラの右上座標.

        if (X < min.x || max.x < X)
        {
            VX = 0;   //X移動量を反転.
            ClampScreen();
        }
        if (Y < min.y || max.y < Y)
        {
            VY = 0;
            ClampScreen();
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                ypos = touch.position.y;

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                spd = 0;
                SetVelocity(dir, spd);

            }
            else
            {
                float newYpos = touch.position.y;
                if (newYpos - ypos > 20f)
                {
                    dir = 90;
                    spd = 3;
                    SetVelocity(dir, spd);
                }
                else if (newYpos - ypos < -20f)
                {
                    dir = -90;
                    spd = 3;
                    SetVelocity(dir, spd);
                }
                else
                {
                    spd = 0;
                    SetVelocity(dir, spd);
                }

            }
        }

        timeElapsed -= Time.deltaTime;
        if (timeElapsed <= 0)
        {
            GameObject bullet = (GameObject)Resources.Load("Bullet");
            Instantiate(bullet, new Vector3(X+0.5f, Y, 0), Quaternion.Euler(0, 0, -90));
            timeElapsed = 0.25f;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        AttackedCnt -= 1;

        if (AttackedCnt == 0)
        {
            // プレイヤーを削除
            // 爆発エフェクトを生成する
            GameObject LExplosionPrefab2 = (GameObject)Resources.Load("LExplosion");
            Instantiate(LExplosionPrefab2, transform.position, Quaternion.identity);

            GameOver.finish();

            Destroy(Obj);
            Destroy(gameObject);
        }
        else
        {
            // 爆発エフェクトを生成する
            SExp = (GameObject)Resources.Load("SExplosion");
            Obj = (GameObject)Instantiate(SExp, Ply.transform.position, Quaternion.identity);
            // 作成したオブジェクトを子として登録
            Obj.transform.parent = Ply.transform;
        }

    }

    /// アクセサ.
    /// レンダラー.
    SpriteRenderer _renderer = null;

    public SpriteRenderer Renderer
    {
        get { return _renderer ?? (_renderer = gameObject.GetComponent<SpriteRenderer>()); }
    }
    /// スケール値(X).
    public float ScaleX
    {
        set
        {
            Vector3 scale = transform.localScale;
            scale.x = value;
            transform.localScale = scale;
        }
        get { return transform.localScale.x; }
    }

    /// スケール値(Y).
    public float ScaleY
    {
        set
        {
            Vector3 scale = transform.localScale;
            scale.y = value;
            transform.localScale = scale;
        }
        get { return transform.localScale.y; }
    }
    /// 座標(X).
    public float X
    {
        set
        {
            Vector3 pos = transform.position;
            pos.x = value;
            transform.position = pos;
        }
        get { return transform.position.x; }
    }

    /// 座標(Y).
    public float Y
    {
        set
        {
            Vector3 pos = transform.position;
            pos.y = value;
            transform.position = pos;
        }
        get { return transform.position.y; }
    }

    /// 剛体.
    Rigidbody2D _rigidbody2D = null;

    public Rigidbody2D RigidBody
    {
        get { return _rigidbody2D ?? (_rigidbody2D = gameObject.GetComponent<Rigidbody2D>()); }
    }

    /// 移動量を設定.
    public void SetVelocity(float direction, float speed)
    {
        Vector2 v;
        v.x = Util.CosEx(direction) * speed;
        v.y = Util.SinEx(direction) * speed;
        RigidBody.velocity = v;
    }
    /// 移動量(X).
    public float VX
    {
        get { return RigidBody.velocity.x; }
        set
        {
            Vector2 v = RigidBody.velocity;
            v.x = value;
            RigidBody.velocity = v;
        }
    }

    /// 移動量(Y).
    public float VY
    {
        get { return RigidBody.velocity.y; }
        set
        {
            Vector2 v = RigidBody.velocity;
            v.y = value;
            RigidBody.velocity = v;
        }
    }
    /// サイズを設定.
    float _width = 0.0f;
    float _height = 0.0f;

    public void SetSize(float width, float height)
    {
        _width = width;
        _height = height;
    }

    /// スプライトの幅.
    public float SpriteWidth
    {
        get { return Renderer.bounds.size.x; }
    }

    /// スプライトの高さ.
    public float SpriteHeight
    {
        get { return Renderer.bounds.size.y; }
    }

    public Vector2 GetWorldMin(bool noMergin = false)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(Vector2.zero);
        if (noMergin)
        {
            // そのまま返す.
            return min;
        }

        // 自身のサイズを考慮する.
        min.x += _width;
        min.y += _height;
        return min;
    }

    /// 画面右上のワールド座標を取得する.
    public Vector2 GetWorldMax(bool noMergin = false)
    {
        Vector2 max = Camera.main.ViewportToWorldPoint(Vector2.one);
        if (noMergin)
        {
            // そのまま返す.
            return max;
        }

        // 自身のサイズを考慮する.
        max.x -= _width;
        max.y -= _height;
        return max;
    }

    /// 画面内に収めるようにする.
    public void ClampScreen()
    {
        Vector2 min = GetWorldMin();
        Vector2 max = GetWorldMax();
        Vector2 pos = transform.position;
        // 画面内に収まるように制限をかける.
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        // プレイヤーの座標を反映.
        transform.position = pos;
    }
    /// 画面外に出たかどうか.
    public bool IsOutside()
    {
        Vector2 min = GetWorldMin();
        Vector2 max = GetWorldMax();
        Vector2 pos = transform.position;
        if (pos.x < min.x || pos.y < min.y)
        {
            return true;
        }
        if (pos.x > max.x || pos.y > max.y)
        {
            return true;
        }
        return false;
    }
}
