using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverDialog : Dialog
{
    public Text totalScoreTxt;
    public Text bestScoreTxt;


    public override void Show(bool isShow)
    {
        base.Show(isShow);

        if (totalScoreTxt && GameManager.Ins)
            totalScoreTxt.text = GameManager.Ins.Score.ToString();

        if (bestScoreTxt)
            bestScoreTxt.text = Pref.BestScore.ToString();
    }

    public void Replay()
    {
        if (GameManager.Ins)
        {
            // Reset trạng thái trò chơi
            GameManager.Ins.state = GameState.Starting;
            GameManager.Ins.PlatformLandedIds.Clear(); // Xóa danh sách platform đã nhảy lên

            if (GameManager.Ins.player != null)
            {
                // Đặt lại vị trí của người chơi
                GameManager.Ins.player.transform.position = new Vector3(0, -4, 0); // Tùy chỉnh vị trí ban đầu của người chơi
                GameManager.Ins.player.PlatformLanded = null; // Đặt lại platform mà player đang đứng
            }

            // Đặt lại platform cuối cùng đã spawn để khởi tạo lại các platform ban đầu
            GameManager.Ins.LastPlatformSpawned = null;

            // Khởi tạo lại các platform ban đầu
            GameManager.Ins.PlatformInit();

            // Chuyển trò chơi sang trạng thái chơi
            GameManager.Ins.PlayGame();
        }

        SceneManager.sceneLoaded += OnSceneLoadEvent;
        SceneController.Ins.LoadCurrentScene();
    }



    private void OnSceneLoadEvent(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.Ins)
        {
            GameManager.Ins.PlayGame();
        }

        SceneManager.sceneLoaded -= OnSceneLoadEvent;
    }

}


