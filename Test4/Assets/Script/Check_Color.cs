using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Color : MonoBehaviour
{
    Texture2D texture;
    void Start()
    {
        // Load the PNG file from the specified path
        string imagePath = "path/to/your/image.png";
        byte[] imageData = System.IO.File.ReadAllBytes(imagePath); // Read the image data from file

        // Create a new Texture2D object and load the image data into it
        texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);

        // Get the pixel color at a specific coordinate
        int x = 100; // X coordinate of the pixel
        int y = 200; // Y coordinate of the pixel
        Color pixelColor = texture.GetPixel(x, y);

        Debug.Log("Pixel Color: " + pixelColor);
    }
}
