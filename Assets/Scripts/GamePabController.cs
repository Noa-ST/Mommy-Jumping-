using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GamePabContro : Singleton<GamePabContro>
{
    public bool isOnMoblile;
    bool m_canMoveLeft;
    bool m_canMoveRight;

    public bool CanMoveRight { get => m_canMoveRight; set => m_canMoveRight = value; }
    public bool CanMoveLeft { get => m_canMoveLeft; set => m_canMoveLeft = value; }

    public override void Awake()
    {
        MakeSingleton(false);
    }

    // check xem ng chs click vao vi tri trai sang phai, de sd cho method movinghandle giup player di chuyen
    private void Update()
    {
        if (isOnMoblile) return;

        m_canMoveLeft = Input.GetAxisRaw("Horizontal") < 0 ? true : false;
        m_canMoveRight = Input.GetAxisRaw("Horizontal") > 0 ? true : false;
    }
}
