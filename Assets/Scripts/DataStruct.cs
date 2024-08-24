using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Các enum (liệt kê) có mục đích chính là làm cho mã dễ đọc, dễ bảo trì và giảm thiểu lỗi liên quan đến việc sử dụng các hằng số hoặc chuỗi
public enum GameState
{
    Starting,
    Playing,
    Gameover
}

public enum GameTag
{
    Platform,
    Player,
    LeftConner,
    RightConner,
    Collectable
}

public enum PrefKey
{
    BestScore
}

[System.Serializable]
public class CollectableItem
{
    public Collectable collectablePb;

    [Range(0f, 1f)]
    public float spawnRate; // Khả năng xuất hiện 
}
