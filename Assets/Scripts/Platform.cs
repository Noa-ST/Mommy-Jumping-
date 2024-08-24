using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform cSpawnPoint;
    private int id;
    protected Player m_player;
    protected Rigidbody2D m_rb;

    public int Id { get => id; set => id = value; }

    // protected virtual - các lớp khc có thể overrrie lại 
    protected virtual void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        if (!GameManager.Ins) return;

        m_player = GameManager.Ins.player;

        if (cSpawnPoint)
        {
            GameManager.Ins.SpawnCollectable(cSpawnPoint);
        }
    }

    public virtual void PlatformACtion()
    {

    }
}
