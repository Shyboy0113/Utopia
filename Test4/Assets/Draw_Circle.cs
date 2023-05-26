using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //save파일 저장

public class Draw_Circle : MonoBehaviour
{
    int screenWidth = 1920; // Set the desired width.
    int screenHeight = 1080; // Set the desired height.

    int canvasWidth;
    int canvasHeight;
    
    int pixel_correction = 4; //캔버스 크기의 배율. 값이 높을수록 크기가 작아진다

    Texture2D texture;

    Vector2 previousMousePosition;

    void Start()
    {
        canvasWidth = screenWidth / pixel_correction;
        canvasHeight = screenHeight / pixel_correction;

        texture = new Texture2D(canvasWidth, canvasHeight);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, canvasWidth, canvasHeight), Vector2.one * 0.5f);
        GetComponent<SpriteRenderer>().sprite = sprite;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetCanvasMousePosition());
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = GetCanvasMousePosition();
        }
        if (Input.GetMouseButton(0))
        {
            Draw();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SaveTextureAsPNG();
        }
    }
    

    void DrawLine(int x0, int y0, int x1, int y1, Color color)
    {
        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            texture.SetPixel(x0, y0, color);

            if (x0 == x1 && y0 == y1)
                break;

            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                x0 += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                y0 += sy;
            }
        }
    }

    void Draw()
    {
        Vector2 mousePosition = GetCanvasMousePosition();
        int x =(int)mousePosition.x;
        int y = (int)mousePosition.y;

        // Adjust the x and y values based on the desired drawing area, if needed.

        if (previousMousePosition == Vector2.zero)
        {
            // Start of drawing, set the pixel color at the current position.
            texture.SetPixel(x, y, Color.red);
        }
        else
        {
            // Draw a line between the previous and current positions.
            int startX = (int)previousMousePosition.x;
            int startY = (int)previousMousePosition.y;
            int endX = x;
            int endY = y;
            DrawLine(startX, startY, endX, endY, Color.red);
        }

        previousMousePosition = mousePosition;
        texture.Apply();
    }


    void SaveTextureAsPNG()
    {
        // Encode the texture as PNG.
        byte[] pngData = texture.EncodeToPNG();

        // Generate a unique filename based on the current timestamp.
        string fileName = "magic_circle_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

        // Define the path to save the PNG file (in this example, it is saved in the project's root folder).
        string filePath = Path.Combine(Application.dataPath, fileName);

        // Save the PNG file.
        File.WriteAllBytes(filePath, pngData);

        Debug.Log("Magic circle saved as PNG: " + fileName);
    }

    /// <summary>
    /// 마우스 좌표를 캔버스에 최적화 하여 Vector2로 반환합니다.
    /// </summary>
    /// <returns></returns>
    Vector2 GetCanvasMousePosition()
    {
        var originalPosition = Input.mousePosition;

        int startX = (screenWidth - canvasWidth) / 2;
        int startY = (screenHeight - canvasHeight) / 2;

        var result = new Vector2(originalPosition.x - startX, originalPosition.y - startY);
        result.x = Mathf.Clamp(result.x, 0, canvasWidth);
        result.y = Mathf.Clamp(result.y, 0, canvasHeight);

        return result;
    }
}