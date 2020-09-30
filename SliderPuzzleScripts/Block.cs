using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    /// <summary>
    /// Created, referenced and adapted by Chung Ling Kristy from
    /// <ref>https://www.youtube.com/watch?v=cfWAc4O_WrQ</ref>
    /// </summary>
    /// 
    public event System.Action<Block> OnBlockPressed; //similar to a boolean, but with added functionality, it sets of a chain of functions(specifically the movement of this block) that are called upon use of this function
    public event System.Action OnFinishedMoving; //once called, the block will update its current position and stop moving

    public Vector2Int coordinate;
    Vector2Int startCoord;

    public void Init(Vector2Int startCoord, Texture2D image) //upon initialisation, coordinates are set and the image is rendered in
    {
        this.startCoord = startCoord;
        coordinate = startCoord;
        GetComponent<MeshRenderer>().material.shader = Shader.Find("Unlit/Texture");
        GetComponent<MeshRenderer>().material.mainTexture = image;
    }

    public void MoveToPosition(Vector2 target , float duration) //calls the coroutine to move the current block
    {
        StartCoroutine(AnimateMove(target, duration)); 
    }

    private void OnMouseDown()
    {
        OnBlockPressed?.Invoke(this);
    }

    IEnumerator AnimateMove(Vector2 target, float duration)
    {
        Vector2 initalPos = transform.position; //checks original position of the block
        float percent = 0; //sets a percentage of the block moving progress

        while(percent < 1)
        {
            percent += Time.deltaTime / duration;
            transform.position = Vector2.Lerp(initalPos, target, percent); //block position moves smoothly from original position to target position
            yield return null;
        }
        OnFinishedMoving?.Invoke();
    }

    //if the block is at its original position at the start, this boolean is set true 
    public bool IsAtStartingCoord()
    {
        return coordinate == startCoord;
    }
}
