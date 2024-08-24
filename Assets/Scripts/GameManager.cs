using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : Singleton<GameManager>
{
    public GameState state;
    public Player player;
    public int startingPlatform;
    public float xSpawnOffset;
    public float minYspawnPos;
    public float maxYspawnPos;
    public Platform[] platformPb;
    public CollectableItem[] collectableItems;

    Platform m_lastPlatformSpawned; // Platform cuoi cung khi ma spawn-(tao ra)
    List<int> m_platformLandedIds; // luu lai cac id cua platform ma player da nhay len
    float m_halfCamSizeX;
    int m_score;

    public Platform LastPlatformSpawned { get => m_lastPlatformSpawned; set => m_lastPlatformSpawned = value; }
    public List<int> PlatformLandedIds { get => m_platformLandedIds; set => m_platformLandedIds = value; }
    public int Score { get => m_score;}

    public override void Awake()
    {
        MakeSingleton(false);
        m_platformLandedIds = new List<int>();
        m_halfCamSizeX = Helper.Get2DCamSize().x / 2;
    }

    public override void Start()
    {
        base.Start();
        state = GameState.Starting;
        // Phương thức Invoke được sử dụng để gọi một phương thức khác sau một khoảng thời gian trì hoãn.
        Invoke("PlatformInit", 0.5f);

        if (AudioController.Ins)
        {
            AudioController.Ins.PlayBackgroundMusic();
        }
    }

    public void PlayGame()
    {
        if (GUIManager.Ins)
        {
            GUIManager.Ins.ShowGamePlay(true);
        }
        Invoke("PlatformIvoke", 1f);
    }

    private void PlatformIvoke()
    {
        state = GameState.Playing;
        if (player)
        {
            player.Jump();
        }
    }

    public void PlatformInit()
    {
        if (player == null || platformPb == null || platformPb.Length == 0)
        {
            return;
        }

        // Đặt lại platform đầu tiên mà player đang đứng
        m_lastPlatformSpawned = player.PlatformLanded;
        if (m_lastPlatformSpawned == null)
        {
            // Tạo platform đầu tiên tại vị trí bắt đầu
            var startPosition = new Vector3(0, -4, 0); // Vị trí bắt đầu của platform đầu tiên
            var defaultPlatform = Instantiate(platformPb[0], startPosition, Quaternion.identity);
            player.PlatformLanded = defaultPlatform.GetComponent<Platform>();
            m_lastPlatformSpawned = player.PlatformLanded;
        }

        // Tạo các platform ban đầu
        for (int i = 0; i < startingPlatform; i++)
        {
            SpawnPlatform();
        }
    }



    // pt nay se check platform nay player da ngay len ch
    public bool IsPlatformLanded(int id)
    {
        if (m_platformLandedIds == null || m_platformLandedIds.Count <= 0) return false;

        return m_platformLandedIds.Contains(id);
    }

    //method tao ra cac platform
    public void SpawnPlatform()
    {
        if (!player || platformPb == null || platformPb.Length <= 0)
        {
            Debug.LogError("Player or platformPrefabs are not set.");
            return;
        }


        float spawnPosX = Random.Range(-(m_halfCamSizeX - xSpawnOffset), (m_halfCamSizeX - xSpawnOffset));
        float distBetweenPlat = Random.Range(minYspawnPos, maxYspawnPos); // cai khoang cach giwa cac plat dc tao ra theo chieu y ngau nhien
        float spawnPosY = m_lastPlatformSpawned.transform.position.y + distBetweenPlat; // toa do y cua plat dc tao ra se bang vị trí y của plat dc tạo ra trc đó + khoang cach giua cac plat dc tính ngẫu nhiên bên tren
        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 0f);

        int randIdx = Random.Range(0, platformPb.Length);
        var platformPreB = platformPb[randIdx];

        if (!platformPreB)
        {
            Debug.LogError("Selected platform prefab is null.");
            return;
        }


        // khuc nay tao ra mot plat clone nè, dựa trên spawnPos
        // sau do id cua plat vừa mới tạo sẽ bằng id của plat trc đó +1
        // và rồi cái plat vừa mới tạo sẽ là cái plat trc đó để làm tiền đề cho cái tip theo
        var platformClone = Instantiate(platformPreB, spawnPos, Quaternion.identity);
        platformClone.Id = m_lastPlatformSpawned.Id + 1;
        m_lastPlatformSpawned = platformClone;
    }

    // tạo các Collectable
    public void SpawnCollectable(Transform spawnPoint)
    {
        if (collectableItems == null || collectableItems.Length <= 0 || state != GameState.Playing) return;

        int randIdx = Random.Range(0, collectableItems.Length);
        var collecItem = collectableItems[randIdx];

        if (collecItem == null) return;

        float randCheck = Random.Range(0f, 1f);

        if (randCheck <= collecItem.spawnRate && collecItem.collectablePb)
        {
            var cClone = Instantiate(collecItem.collectablePb, spawnPoint.position, Quaternion.identity);
            cClone.transform.SetParent(spawnPoint);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        if (state != GameState.Playing) return;

        m_score += scoreToAdd;
        Pref.BestScore = m_score;

        if (GUIManager.Ins)
        {
            GUIManager.Ins.UpdateScore(m_score);
        }
    }


}
