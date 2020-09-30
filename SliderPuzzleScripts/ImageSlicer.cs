using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlicer
{
    /// <summary>
    /// Created, referenced and adapted by Chung Ling Kristy from
    /// <ref>https://www.youtube.com/watch?v=cfWAc4O_WrQ</ref>
    /// Divides the selected picture to desired number of pieces to place in quads
    /// </summary>
    /// 
    public static Texture2D[,] GetSlices(Texture2D image, int blocksinRow)
    {
        int imageSize = Mathf.Min(image.width, image.height);
        int blockSize = imageSize / blocksinRow;

        Texture2D[,] blocks = new Texture2D[blocksinRow, blocksinRow];

        for(int y = 0;y < blocksinRow; y++)
        {
            for (int x = 0; x < blocksinRow; x++)
            {
                Texture2D block = new Texture2D(blockSize, blockSize);
                block.wrapMode = TextureWrapMode.Clamp;
                block.SetPixels(image.GetPixels(x * blockSize, y * blockSize, blockSize, blockSize));
                block.Apply();
                blocks[x, y] = block;
            }
        }
        return blocks;
    }

}
