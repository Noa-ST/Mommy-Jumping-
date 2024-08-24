using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  tính toán và trả về kích thước của camera trong không gian 2D khi camera đang ở chế độ chiếu trực giao (orthographic)
public static class Helper
{
    public static Vector2 Get2DCamSize()
    {
    //Camera.main.aspect: Tỷ lệ khung hình của camera, tức là tỉ lệ giữa chiều rộng và chiều cao (width/height).

     //Camera.main.orthographicSize: Khi camera ở chế độ chiếu trực giao, thuộc tính này cho biết nửa chiều cao của camera(tính từ giữa tới mép trên hoặc mép dưới của màn hình).

     //Phương pháp tính kích thước 2D của camera:
     //Chiều cao(height): Trong chế độ chiếu trực giao, chiều cao của camera là 2 * orthographicSize.

     //Chiều rộng(width): Chiều rộng của camera được tính dựa trên chiều cao và tỷ lệ khung hình: width = 2 * aspect * orthographicSize.

     //Kết quả: Phương thức Get2DCamSize trả về một Vector2 chứa kích thước chiều rộng và chiều cao của camera.
        
        
        
        return new Vector2(2f * Camera.main.aspect * Camera.main.orthographicSize, 2f * Camera.main.orthographicSize);
    }
}
