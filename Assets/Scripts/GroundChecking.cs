using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecking : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(GameTag.Platform.ToString())) return;

        // Nếu đối tượng có thành phần Platform, tham chiếu đến nó sẽ được lưu trữ trong biến platformLanded
        var platformLanded = collision.gameObject.GetComponent<Platform>();

        if (!GameManager.Ins || !GameManager.Ins.player || !platformLanded) return;
        // Thiết lập và thực hiện hành động
        // Gán giá trị platformLanded (đối tượng Platform mà người chơi đã đáp xuống) cho thuộc tính PlatformLanded của người chơi.
        GameManager.Ins.player.PlatformLanded = platformLanded;
        GameManager.Ins.player.Jump();

        // neu plat nay ch dc player nhay len
        // Nếu nền tảng chưa có trong danh sách (chưa được tính điểm), khối mã bên trong if sẽ được thực hiện.
        if (!GameManager.Ins.IsPlatformLanded(platformLanded.Id))
        {
            int randScore = Random.Range(3, 8);
            GameManager.Ins.AddScore(randScore);

            //Plat này sẽ được đánh dấu là đã được tính điểm bằng cách thêm Id của nó vào danh sách PlatformLandedIds.
            //Điều này ngăn việc tính điểm nhiều lần nếu người chơi đáp xuống cùng một nền tảng.
             GameManager.Ins.PlatformLandedIds.Add(platformLanded.Id);
        }
    }
}
