using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Platform
{
    public float moveSpeed;
    bool m_canMoveRight;
    bool m_canMoveLeft;

    protected override void Start()
    {
        base.Start();

        float randCheck = Random.Range(0f, 1f);
        if (randCheck <= 0.5f)
        {
            m_canMoveLeft = true;
            m_canMoveRight = false;
        } else
        {
            m_canMoveLeft = false;
            m_canMoveRight = true;
        }
    }

    private void FixedUpdate()
    {
        float curSpeed = 0;

        if (!m_rb) return;

        if (m_canMoveLeft)
        {
            curSpeed = -moveSpeed;
        } else if (m_canMoveRight)
        {
            curSpeed = moveSpeed;
        }

        m_rb.velocity = new Vector2(curSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameTag.LeftConner.ToString()))
        {
            m_canMoveLeft = false;
            m_canMoveRight = true;
        } else if (collision.CompareTag(GameTag.RightConner.ToString()))
        {
   
            m_canMoveLeft = true;
            m_canMoveRight = false;
        }
    }
}




//Sự khác biệt giữa Update() và FixedUpdate():
//Update(): Được gọi mỗi khung hình (frame), và phụ thuộc vào tốc độ khung hình của trò chơi. Sử dụng Update() cho các hành động không liên quan đến vật lý như đầu vào người chơi hoặc cập nhật UI.
//FixedUpdate(): Được gọi tại các khoảng thời gian đều đặn, được sử dụng cho các thao tác vật lý như lực, va chạm, hoặc các hành động cần sự chính xác theo thời gian.
//Khi nào nên sử dụng FixedUpdate()?
//Khi bạn muốn đảm bảo tính nhất quán trong việc tính toán vật lý (di chuyển, lực, va chạm) mà không bị ảnh hưởng bởi tốc độ khung hình.
//Khi bạn cần điều khiển hoặc mô phỏng các thành phần vật lý trong game như Rigidbody, Collider, hoặc các thành phần vật lý khác.
