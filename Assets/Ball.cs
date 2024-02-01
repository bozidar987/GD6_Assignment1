using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    UP,
    DOWN, LEFT, RIGHT,
    UPLEFT, DOWNLEFT,
    UPRIGHT, DOWNRIGHT
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
        tempPosition = currentPosition;

        //if(currentPosition.x < halfCourt)
        //{
        //    relevantPlayer = leftPlayer;
        //}
        //else
        //{
        //    relevantPlayer = rightPlayer;
        //}


        if (currentMoveTime < moveTimer)
        {
            if (currentPosition.x == 0 || currentPosition.x == courtWidth - 1)
            {
                death();
            }

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
            else if (tempPosition.x == 0 && tempPosition.y == courtHeight-1)
            {
                dir = Direction.DOWNRIGHT;
            }
            else if(tempPosition.x == courtWidth-1 && tempPosition.y == 0)
            {
                dir = Direction.UPLEFT;
            }
            else if(tempPosition.x == courtWidth-1 && tempPosition.y == courtHeight-1)
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
                        if(tempPosition.x == 0)
                        {
                            if (tempPosition.y <= 0)
                            {
                                dir = Direction.DOWNRIGHT;
                            }
                        }
                        else if(tempPosition.y >= positionsVec.y)
                        {
                            dir = Direction.DOWNLEFT;
                        }
                        break;
                    case Direction.UPRIGHT:
                        if (tempPosition.x >= positionsVec.x)
                        {
                            if (tempPosition.y >= positionsVec.y)
                            {
                                dir = Direction.DOWNLEFT;
                            }
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
                        if (tempPosition.x >= positionsVec.x)
                        {
                            if (tempPosition.y <= 0)
                            {
                                dir = Direction.UPLEFT;
                            }
                        }
                        else if (tempPosition.y <= 0)
                        {
                            dir = Direction.UPRIGHT;
                        }
                        break;
                }
            }
            else if (positions[(int)tempPosition.x,(int)tempPosition.y].GetComponent<PossiblePosition>() != null &&
                positions[(int)tempPosition.x, (int)tempPosition.y].GetComponent<PossiblePosition>().GetPositionTaken())
            {
             
                    if (relevantPlayer.GetComponent<Player>().GetStaffPosition() == StaffPosition.UP)
                    {
                        if (dir == Direction.DOWN)
                        {
                            if(tempPosition.x < halfCourt)
                            {
                                dir = Direction.RIGHT;
                            }
                            else
                            {
                                dir = Direction.LEFT;
                            }
                        }
                        else if (dir == Direction.LEFT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                dir = Direction.UP;
                            }
                            else
                            {
                                death();
                            }
                        }
                        else if (dir == Direction.RIGHT)
                        {
                            if (tempPosition.x<halfCourt)
                            {
                                death();
                            }
                            else
                            {
                                dir = Direction.UP;
                            }
                        }
                        else if (dir == Direction.DOWNRIGHT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                death();
                            }
                            else
                            {
                                dir = Direction.UPLEFT;
                            }
                        }
                        else if (dir == Direction.DOWNLEFT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                dir = Direction.UPRIGHT;
                            }
                            else
                            {
                                death();
                            }
                        }
                        else
                        {
                            death();
                        }
                    }
                    else if (relevantPlayer.GetComponent<Player>().GetStaffPosition() == StaffPosition.DOWN)
                    {
                        if (dir == Direction.UP)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                dir = Direction.RIGHT;
                            }
                            else
                            {
                                death();
                            }
                        }
                        else if (dir == Direction.LEFT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                dir = Direction.DOWN;
                            }
                            else
                            {
                                death();
                            }
                        }
                        else if (dir == Direction.RIGHT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                death();
                            }
                            else
                            {
                                dir = Direction.DOWN;
                            }
                        }
                        else if (dir == Direction.UPRIGHT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                death();
                            }
                            else
                            {
                                dir = Direction.DOWNLEFT;
                            }
                        }
                        else if (dir == Direction.DOWNLEFT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                dir = Direction.UPRIGHT;
                            }
                            else
                            {
                                death();
                            }
                        }
                        else
                        {
                            death();
                        }
                    }
                    else
                    {
                        if (dir == Direction.LEFT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                dir = Direction.RIGHT;
                            }
                            else
                            {
                                death();
                            }
                        }
                        else if (dir == Direction.RIGHT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                death();
                            }
                            else
                            {
                                dir = Direction.LEFT;
                            }
                        }
                        else if (dir == Direction.UPRIGHT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                death();
                            }
                            else
                            {
                                dir = Direction.DOWNLEFT;
                            }
                        }
                        else if (dir == Direction.UPLEFT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                dir = Direction.UPRIGHT;
                            }
                            else
                            {
                                death();
                            }
                        }
                        else if (dir == Direction.DOWNLEFT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                dir = Direction.DOWNRIGHT;
                            }
                            else
                            {
                                death();
                            }
                        }
                        else if (dir == Direction.DOWNRIGHT)
                        {
                            if (tempPosition.x < halfCourt)
                            {
                                death();
                            }
                            else
                            {
                                dir = Direction.DOWNLEFT;
                            }
                        }
                        else
                        {
                            death();
                        }
                    }
                
            }
            else
            {
                currentPosition = tempPosition;
                Debug.Log(tempPosition);
                gameObject.transform.position = positions[(int)currentPosition.x,(int)currentPosition.y].transform.position;
                moveTimer = 0;
            }
        }
        moveTimer += Time.deltaTime;
    }

    private void death()
    {
        if(currentPosition.x < halfCourt)
        {
            leftPlayerScore++;
        }
        else
        {
            rightPlayerScore++;
        }
        if(leftPlayerScore > 4 || rightPlayerScore > 4)
        {
            leftPlayerScore = 0;
            rightPlayerScore = 0;
        }
        currentMoveTime = moveTime;
        moveTimer = 0;
        currentPosition = startingPosition;
        gameObject.transform.position = positions[(int)currentPosition.x,(int)currentPosition.y].transform.position;
    }
}
