using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public enum Direction
{
    LEFT, RIGHT,
    UPLEFT, DOWNLEFT,
    UPRIGHT, DOWNRIGHT,
    UP, DOWN
}

public class Ball : MonoBehaviour
{
    int leftPlayerScore = 0;
    int rightPlayerScore = 0;

    [SerializeField] GameObject leftPlayer;
    [SerializeField] GameObject rightPlayer;

    [SerializeField] float moveTime;
    float currentMoveTime;
    [SerializeField] float moveTimeMultiplier;
    [SerializeField] Direction dir;
    float moveTimer;
    [SerializeField] Vector2 startingPosition;
    Vector2 currentPosition;
    Vector2 tempPosition;
    GameObject relevantPlayer;
    GameObject[,] positions;
    [SerializeField] GameObject ballPositions;
    public int courtWidth;
    public int courtHeight;
    Vector2 positionsVec;
    float halfCourt;


    [SerializeField] AudioSource moveSound;
    [SerializeField] AudioSource scoreSound;
    [SerializeField] AudioSource winSound;
    [SerializeField] AudioSource reflectSound;
    [SerializeField] Text leftPlayerScoreText;
    [SerializeField] Text rightPlayerScoreText;

    bool reflected = false;

    // Start is called before the first frame update
    void Start()
    {
        moveTimer = 0;
        positions = new GameObject[courtWidth, courtHeight];
        currentMoveTime = moveTime;
        currentPosition = startingPosition;
        
        int posChildCount = ballPositions.transform.childCount;
        Vector2 temp = Vector2.zero;
        for(int i = 0; i < posChildCount; i++)
        {
            if(temp.y >= courtHeight)
            {
                temp.y = 0;
                temp.x += 1;
            }
            positions[(int)temp.x, (int)temp.y] = ballPositions.transform.GetChild(i).gameObject;
            temp.y += 1;
        }

        gameObject.transform.position = positions[(int)startingPosition.x, (int)startingPosition.y].transform.position;
        positionsVec.x = courtWidth;
        positionsVec.y = courtHeight;
        halfCourt = courtWidth / 2;
    }

    // Update is called once per frame
    void Update()
    {
        leftPlayerScoreText.text = leftPlayerScore.ToString();
        rightPlayerScoreText.text = rightPlayerScore.ToString() ;

        tempPosition = currentPosition;

        if (currentPosition.x < halfCourt)
        {
            relevantPlayer = leftPlayer;
        }
        else
        {
            relevantPlayer = rightPlayer;
        }


        if (currentMoveTime < moveTimer)
        {
            if (currentPosition.x == 0 || currentPosition.x == courtWidth - 1)
            {
                Death();
            }
            else
            {
                switch (dir)
                {
                    case Direction.UP:
                        tempPosition.y += 1;
                        break;
                    case Direction.DOWN:
                        tempPosition.y -= 1;
                        break;
                    case Direction.LEFT:
                        tempPosition.x -= 1;
                        break;
                    case Direction.RIGHT:
                        tempPosition.x += 1;
                        break;
                    case Direction.UPLEFT:
                        tempPosition.x -= 1;
                        tempPosition.y += 1;
                        break;
                    case Direction.UPRIGHT:
                        tempPosition.x += 1;
                        tempPosition.y += 1;
                        break;
                    case Direction.DOWNLEFT:
                        tempPosition.x -= 1;
                        tempPosition.y -= 1;
                        break;
                    case Direction.DOWNRIGHT:
                        tempPosition.x += 1;
                        tempPosition.y -= 1;
                        break;
                }

                if (tempPosition.x == 0 && tempPosition.y == 0)
                {
                    dir = Direction.UPRIGHT;
                }
                else if (tempPosition.x == 0 && tempPosition.y == courtHeight - 1)
                {
                    dir = Direction.DOWNRIGHT;
                }
                else if (tempPosition.x == courtWidth - 1 && tempPosition.y == 0)
                {
                    dir = Direction.UPLEFT;
                }
                else if (tempPosition.x == courtWidth - 1 && tempPosition.y == courtHeight - 1)
                {
                    dir = Direction.DOWNLEFT;
                }
                else if (tempPosition.x < 0 || tempPosition.x >= positionsVec.x ||
                    tempPosition.y < 0 || tempPosition.y >= positionsVec.y)
                {
                    switch (dir)
                    {
                        case Direction.UP:
                            dir = Direction.DOWN;
                            break;
                        case Direction.DOWN:
                            dir = Direction.UP;
                            break;
                        case Direction.LEFT:
                            dir = Direction.RIGHT;
                            break;
                        case Direction.RIGHT:
                            dir = Direction.LEFT;
                            break;
                        case Direction.UPLEFT:
                            if (tempPosition.x == 0 && tempPosition.y <= 0)
                            {
                                    dir = Direction.DOWNRIGHT;
                            }
                            else if (tempPosition.y >= positionsVec.y)
                            {
                                dir = Direction.DOWNLEFT;
                            }
                            break;
                        case Direction.UPRIGHT:
                            if (tempPosition.x >= positionsVec.x && tempPosition.y >= positionsVec.y)
                            {
                                    dir = Direction.DOWNLEFT;
                      
                            }
                            else if (tempPosition.y >= positionsVec.y)
                            {
                                dir = Direction.DOWNRIGHT;
                            }
                            break;
                        case Direction.DOWNLEFT:
                            dir = Direction.UPLEFT;
                            break;
                        case Direction.DOWNRIGHT:
                            if (tempPosition.x >= positionsVec.x&& tempPosition.y <= 0)
                            {
                                    dir = Direction.UPLEFT;
                            }
                            else if (tempPosition.y <= 0)
                            {
                                dir = Direction.UPRIGHT;
                            }
                            break;
                    }
                }
                else if (positions[(int)tempPosition.x, (int)tempPosition.y].GetComponent<PossiblePosition>() != null &&
                    positions[(int)tempPosition.x, (int)tempPosition.y].GetComponent<PossiblePosition>().GetPositionTaken())
                {
                    if (relevantPlayer.GetComponent<Player>().GetStaffPosition() == StaffPosition.UP)
                    {
                        if (dir == Direction.DOWN)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                dir = Direction.RIGHT;
                            }
                            else
                            {
                                dir = Direction.LEFT;
                            }
                            reflected = true;
                        }
                        else if (dir == Direction.LEFT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                dir = Direction.UP;
                                reflected = true;
                            }
                            else
                            {
                                Death();
                            }
                        }
                        else if (dir == Direction.RIGHT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                //Death();
                            }
                            else
                            {
                                dir = Direction.UP;
                                reflected = true;
                            }
                        }
                        else if (dir == Direction.DOWNRIGHT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                //Death();
                            }
                            else
                            {
                                dir = Direction.UPLEFT;
                                reflected = true;
                            }
                        }
                        else if (dir == Direction.DOWNLEFT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                dir = Direction.UPRIGHT;
                                reflected = true;
                            }
                            else
                            {
                                //Death();
                            }
                        }
                        else
                        {
                            Death();
                        }
                    }
                    else if (relevantPlayer.GetComponent<Player>().GetStaffPosition() == StaffPosition.DOWN)
                    {
                        if (dir == Direction.UP)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                dir = Direction.RIGHT;
                                reflected = true;
                            }
                            else
                            {
                                Death();
                            }
                        }
                        else if (dir == Direction.LEFT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                dir = Direction.DOWN;
                                reflected = true;
                            }
                            else
                            {
                                Death();
                            }
                        }
                        else if (dir == Direction.RIGHT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                Death();
                            }
                            else
                            {
                                dir = Direction.DOWN;
                                reflected = true;
                            }
                        }
                        else if (dir == Direction.UPRIGHT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                Death();
                            }
                            else
                            {
                                dir = Direction.DOWNLEFT;
                                reflected = true;
                            }
                        }
                        else if (dir == Direction.DOWNLEFT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                dir = Direction.UPRIGHT;
                                reflected = true;
                            }
                            else
                            {
                                Death();
                            }
                        }
                        else
                        {
                            Death();
                        }
                    }
                    else
                    {
                        if (dir == Direction.LEFT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                dir = Direction.RIGHT;
                                reflected = true;
                            }
                            else
                            {
                                Death();
                            }
                        }
                        else if (dir == Direction.RIGHT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                Death();
                            }
                            else
                            {
                                dir = Direction.LEFT;
                                reflected = true;
                            }
                        }
                        else if (dir == Direction.UPRIGHT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                Death();
                            }
                            else
                            {
                                dir = Direction.DOWNLEFT;
                                reflected = true;
                            }
                        }
                        else if (dir == Direction.UPLEFT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                dir = Direction.UPRIGHT;
                                reflected = true;
                            }
                            else
                            {
                                Death();
                            }
                        }
                        else if (dir == Direction.DOWNLEFT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                dir = Direction.DOWNRIGHT;
                                reflected = true;
                            }
                            else
                            {
                                Death();
                            }
                        }
                        else if (dir == Direction.DOWNRIGHT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                Death();
                            }
                            else
                            {
                                dir = Direction.DOWNLEFT;
                                reflected = true;
                            }
                        }
                        else
                        {
                            Death();
                        }
                    }
                }
                else
                {
                    positions[(int)currentPosition.x, (int)currentPosition.y].GetComponent<PossiblePosition>().ballBlocked = false;
                    if(currentPosition.y > 0)
                    {
                        positions[(int)currentPosition.x, (int)currentPosition.y-1].GetComponent<PossiblePosition>().ballBlocked = false;
                    }
                    currentPosition = tempPosition;
                    positions[(int)currentPosition.x, (int)currentPosition.y].GetComponent<PossiblePosition>().ballBlocked = true;
                    if (currentPosition.y > 0)
                    {
                        positions[(int)currentPosition.x, (int)currentPosition.y - 1].GetComponent<PossiblePosition>().ballBlocked = true;
                    }
                    gameObject.transform.position = positions[(int)currentPosition.x, (int)currentPosition.y].transform.position;
                    moveTimer = 0;
                    if (reflected)
                    {
                        reflectSound.Play();
                    }
                    else
                    {
                        moveSound.Play();
                    }
                    reflected = false;
                }
            }
        }
        moveTimer += Time.deltaTime;
    }

    private void Death()
    {
        if (currentPosition.x < halfCourt)
        {
            leftPlayerScore++;
            scoreSound.Play();
        }
        else
        {
            rightPlayerScore++;
            scoreSound.Play();
        }
        if (leftPlayerScore > 4 || rightPlayerScore > 4)
        {
            leftPlayerScore = 0;
            rightPlayerScore = 0;
            winSound.Play();
        }

        int rand = Random.Range(0, 5);
        dir = (Direction)rand;
        currentMoveTime = moveTime;
        moveTimer = -2;
        Debug.Log("check1");
        currentPosition = startingPosition;
        Debug.Log("check2");
        this.gameObject.transform.position = positions[(int)currentPosition.x,(int)currentPosition.y].transform.position;
        Debug.Log("DIED");
    }

    public Direction getDirection() { return dir; }
}
