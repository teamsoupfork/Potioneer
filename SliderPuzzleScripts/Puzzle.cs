using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
    /// <summary>
    /// Created, referenced and adapted by Chung Ling Kristy from
    /// <ref>https://www.youtube.com/watch?v=cfWAc4O_WrQ</ref>
    /// </summary>
    /// 
    bool wake;

    public Texture2D image;
    public int blocksInRow = 4; /*Can be modified to increase or decrease puzzle difficulty by changing the number of blocks in puzzle*/
    public int shuffleLength;
    public float defaultMoveDuration = .2f; /*Can be modified to increase or decrease the speed of the Block movement*/
    public float shuffleMoveDuration = .1f; /*Can be modified to increase or decrease the speed of the shuffle movements*/

    public GameObject Cont, skip, WakeUp, BG, btn1, btn2;

    enum PuzzleState { Solved, Shuffling, InPlay, End};
    PuzzleState state;

    Block emptyBlock;
    Block[,] blocks;
    Queue<Block> inputs;
    bool blockIsMoving;
    int shuffleMovesRemaining;
    Vector2Int prevShuffleOffset;

    private void Start()
    {
        //finds the button in the scene that will instantiate the puzzle
        Button btn = WakeUp.GetComponent<Button>();
        btn.onClick.AddListener(WakeUpTime);

        shuffleLength = blocksInRow * 5; //shuffleLength is adjusted according to number of puzzle pieces in the puzzle
    }
    private void Update()
    {
        if (wake)
        {
            CreatePuzzle();
            this.transform.position = new Vector3(3.1f, 0, 0);
            StartShuffle();
            TimeManager.Cont = true; //starts timer for multiplier
            wake = false;
            PhoneStart();
        }
    }

    void PhoneStart() //sets Phone Puzzle to true and makes sure that interactables cannot be touched when solving puzzle
    {
        BG.SetActive(true);
        WakeUp.SetActive(false);
        btn1.SetActive(false);
        btn2.SetActive(false);
    }
    void CreatePuzzle() //sets the puzzle in fixed parameters that can be sorted as an array
    {
        blocks = new Block[blocksInRow, blocksInRow];
        Texture2D[,] imageSlices = ImageSlicer.GetSlices(image, blocksInRow);
        for (int y = 0; y< blocksInRow; y++)
        {
            for (int x =0; x< blocksInRow; x++)
            {
                GameObject blockObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                blockObject.transform.position = -Vector2.one * (blocksInRow - 1) * .5f + new Vector2(x, y); //creation of multiple quads that are equidistant to each other
                blockObject.transform.parent = transform;

                Block block = blockObject.AddComponent<Block>();
                block.OnBlockPressed += MoveBlockInput;
                block.OnFinishedMoving += OnBlockFinishedMoving;
                block.Init(new Vector2Int(x, y), imageSlices[x, y]);
                blocks[x, y] = block;

                if(y == 0 && x == blocksInRow - 1)
                {
                    emptyBlock = block;
                }
            } 
        }

        Camera.main.orthographicSize = blocksInRow;
        inputs = new Queue<Block>();
    }

    //Moves the selected block according to available spaces
    void MoveBlockInput(Block blockToMove)
    {
        if (state == PuzzleState.InPlay)
        {
            TimeManager.StartPzzl = true;
            inputs.Enqueue(blockToMove);
            MakeNextPlayerMove();
        }
    }

    void MoveBlock(Block blockToMove, float duration)
    {
        if ((blockToMove.transform.position - emptyBlock.transform.position).sqrMagnitude == 1) //moves into empty blocks position
        {
            blocks[blockToMove.coordinate.x, blockToMove.coordinate.y] = emptyBlock;
            blocks[emptyBlock.coordinate.x, emptyBlock.coordinate.y] = blockToMove;

            Vector2Int targetCoord = emptyBlock.coordinate;
            emptyBlock.coordinate = blockToMove.coordinate;
            blockToMove.coordinate = targetCoord;

            Vector2 targetPosition = emptyBlock.transform.position;
            emptyBlock.transform.position = blockToMove.transform.position;
            blockToMove.MoveToPosition(targetPosition, duration);
            blockIsMoving = true;
        }
    }

    //allows player to move blocks when play state is true
    void MakeNextPlayerMove()
    {
        if (state == PuzzleState.InPlay)
        {
            while (inputs.Count > 0 && !blockIsMoving)
            {
                MoveBlock(inputs.Dequeue(), defaultMoveDuration);
            }
        }

    }
    //checks if the puzzle is solved after every move
    void OnBlockFinishedMoving()
    {
        blockIsMoving = false;
        CheckIfSolved();

        if(state == PuzzleState.InPlay)
        {
            MakeNextPlayerMove();
        }
        else if(state == PuzzleState.Shuffling)
        {
            if (shuffleMovesRemaining > 0)
            {
                Shuffle();
            }
            else
            {
                state = PuzzleState.InPlay;
            }
        }
    }

    //changes the position of all the blocks according to the number of shuffle moves given
    void StartShuffle()
    {
        state = PuzzleState.Shuffling;
        shuffleMovesRemaining = shuffleLength;
        emptyBlock.gameObject.SetActive(false);
        Shuffle();
    }
    void Shuffle()
    {
        Debug.Log("Shuffle");
        Vector2Int[] offsets = { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) };
        int randomIndex = Random.Range(0, offsets.Length);

        for(int i = 0; i < offsets.Length; i++)
        {
            Vector2Int offset = offsets[(randomIndex + i) % offsets.Length]; //loops around arrray
            if (offset != prevShuffleOffset * -1)
            {
                Vector2Int moveBlockCoord = emptyBlock.coordinate + offset;

                if (moveBlockCoord.x >= 0 && moveBlockCoord.x < blocksInRow && moveBlockCoord.y >= 0 && moveBlockCoord.y < blocksInRow)
                {
                    MoveBlock(blocks[moveBlockCoord.x, moveBlockCoord.y], shuffleMoveDuration);
                    shuffleMovesRemaining--;
                    Debug.Log(shuffleMovesRemaining);
                    prevShuffleOffset = offset;
                    break;
                }
            }
        }
    }

    //If puzzle is solved, enables the continue button in the scene and prevents player from skipping scene after solving the puzzle
    void CheckIfSolved()
    {
        foreach(Block block in blocks)
        {
            if (!block.IsAtStartingCoord())
            {
                return;
            }
        }
        state = PuzzleState.End;
        emptyBlock.gameObject.SetActive(true);
        Cont.SetActive(true); //Continue button is set true
        TimeManager.StartPzzl = false; 
        TimeManager.Cont = false; //Stops the timer for the multiplier score
        skip.GetComponent<Button>().enabled = false;
    }

    public void WakeUpTime()
    {
        wake = true;
    }
}
