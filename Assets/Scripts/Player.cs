using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce; // luc nhay
    public float moveSpeed; // tocdo di chuyen
    Platform m_platformLanded; // platform ma player da nhay len
    float m_MovingLimixX; // xac dinh gioi han player theo truc x

    Rigidbody2D m_rb;

    public Platform PlatformLanded { get => m_platformLanded; set => m_platformLanded = value; }
    public float MovingLimixX { get => m_MovingLimixX; }

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovingHandle();
    }

    public void Jump()
    {
        if (!GameManager.Ins || GameManager.Ins.state != GameState.Playing) return;

        if (!m_rb || m_rb.velocity.y > 0 || !m_platformLanded) return;

        if (m_platformLanded is BreakablePlatform)
        {
            m_platformLanded.PlatformACtion();
        }

        m_rb.velocity = new Vector2(m_rb.velocity.x, jumpForce);

        if (AudioController.Ins)
        {
            AudioController.Ins.PlaySound(AudioController.Ins.jump);
        }
    }


    // method di chuyen trai phai
    private void MovingHandle()
    {
        if (!GamePabContro.Ins || !m_rb || !GameManager.Ins || GameManager.Ins.state != GameState.Playing) return;
        
        if (GamePabContro.Ins.CanMoveLeft)
        {
            m_rb.velocity = new Vector2(-moveSpeed, m_rb.velocity.y);
        } else if (GamePabContro.Ins.CanMoveRight)
        {
            m_rb.velocity = new Vector2(moveSpeed, m_rb.velocity.y);
        } else
        {
            m_rb.velocity = new Vector2(0, m_rb.velocity.y);
        }

        // Điều này đảm bảo rằng vị trí X của đối tượng không thể vượt quá giới hạn đã tính ở bước trước, giữ đối tượng luôn nằm trong phạm vi của màn hình camera.
        m_MovingLimixX = Helper.Get2DCamSize().x / 2;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -m_MovingLimixX, m_MovingLimixX), transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameTag.Collectable.ToString()))
        {
            var collectable = collision.GetComponent<Collectable>();
            if (collectable)
            {
                collectable.Trigger();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Platform platform = collision.gameObject.GetComponent<Platform>();
        if (platform != null)
        {
            PlatformLanded = platform;
        }
    }

}
