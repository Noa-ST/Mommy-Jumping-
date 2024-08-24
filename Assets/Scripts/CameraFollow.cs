using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UDEV.EndlessGame
{
    // người chơi sẽ luôn nhìn thấy nhân vật của mình trong trò chơi, và camera sẽ di chuyển một cách tự nhiên theo hướng mà nhân vật đang di chuyển ( nhưng camera chỉ di chuyển lên cao theo player khong di chuyen theo player khi nó rơi xún
    public class CameraFollow : MonoBehaviour
    {
        public static CameraFollow Ins;

        public Transform target;
        public Vector3 offset;
        [Range(1, 10)]
        public float smoothFactor;

        private void Awake()
        {
            Ins = this;
        }

        private void FixedUpdate()
        {
            Follow();
        }

        private void Follow()
        {
            if (target == null || target.transform.position.y < transform.position.y) return;

            // Tính toán tốc độ của camera dựa trên độ cao của player.
            // Bạn có thể điều chỉnh hằng số `heightMultiplier` để kiểm soát tốc độ tăng lên theo độ cao.
            float heightMultiplier = 0.1f; // Giá trị này có thể điều chỉnh để tăng hoặc giảm mức độ khó
            float adjustedSmoothFactor = smoothFactor + (target.position.y * heightMultiplier);

            // In ra giá trị độ cao của player và tốc độ của camera
            Debug.Log("Player Height: " + target.position.y);
            Debug.Log("Camera Speed: " + adjustedSmoothFactor);

            Vector3 targetPos = new Vector3(0, target.transform.position.y, 0f) + offset;

            Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPos, adjustedSmoothFactor * Time.deltaTime);
            transform.position = new Vector3(
                Mathf.Clamp(smoothedPos.x, 0, smoothedPos.x),
                Mathf.Clamp(smoothedPos.y, 0, smoothedPos.y),
                -10f);
        }
    }
}
