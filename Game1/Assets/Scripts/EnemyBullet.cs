using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float dir = 180;    //0°方向に移動.
        float spd = 5; //速さ5.
        SetVelocity(dir, spd);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 min = GetWorldMin();    //カメラの左下座標.
        Vector2 max = GetWorldMax();    //カメラの右上座標.

        if (X < min.x || max.x < X)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        
        // 弾を削除
        Destroy(this.gameObject);

    }

    /// レンダラー.
    SpriteRenderer _renderer = null;

    public SpriteRenderer Renderer
    {
        get { return _renderer ?? (_renderer = gameObject.GetComponent<SpriteRenderer>()); }
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

    /// 画面の左下のワールド座標を取得する.
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
}
