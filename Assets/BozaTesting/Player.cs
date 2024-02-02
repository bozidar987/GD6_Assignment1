using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StaffPosition
{
    NORMAL,
    UP,
    DOWN
}

public enum PlayerType
{
    LEFT,
    RIGHT
}

public class Player : MonoBehaviour
{
    [SerializeField]PlayerType playerType;
    [SerializeField] Ball frogball;
    [SerializeField] PossiblePosition startPosition;
    PossiblePosition currentPosition;

    [SerializeField] KeyCode up;
    [SerializeField] KeyCode down;
    [SerializeField] KeyCode left;
    [SerializeField] KeyCode right;
    [SerializeField] KeyCode action;

    [SerializeField] float movementTime;

    GameObject staffUp;
    GameObject staffDown;
    GameObject staff;
    float movementTimer;

    StaffPosition staffPos;

    [SerializeField] GameObject ballPositions;
    PossiblePosition[,] ballPos;
    // Start is called before the first frame update
    void Start()
    {
        ballPos = new PossiblePosition[20,20];

        int posChildCount = ballPositions.transform.childCount;
        Vector2 temp = Vector2.zero;
        for (int i = 0; i < posChildCount; i++)
        {
            if (temp.y >= frogball.courtHeight)
            {
                temp.y = 0;
                temp.x += 1;
            }
            ballPos[(int)temp.x, (int)temp.y] = ballPositions.transform.GetChild(i).gameObject.GetComponent<PossiblePosition>();
            temp.y += 1;
        }

        currentPosition = null;
        movementTimer = 0;

        gameObject.transform.position = startPosition.gameObject.transform.position;
        currentPosition = startPosition;

        staff = gameObject.transform.Find("Staff").gameObject;
        staffUp = gameObject.transform.Find("StaffUp").gameObject;
        staffDown = gameObject.transform.Find("StaffDown").gameObject;

        staff.SetActive(true);
        staffUp.SetActive(false);
        staffDown.SetActive(false);
        staffPos = StaffPosition.NORMAL;

        for (int i = 0; i < frogball.courtWidth; i++)
        {
            for (int j = 0; j < frogball.courtHeight; j++)
            {
                ballPos[i, j].SetPositionTaken(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        int x = 0;
        int y = 0;
        for(int i =0; i < frogball.courtWidth; i++)
        {
            for (int j = 0; j < frogball.courtHeight; j++)
            {
                if(currentPosition == ballPos[i, j])
                {
                    x = i;
                    y = j;
                    i = frogball.courtWidth;
                    j = frogball.courtHeight;
                }
            }
        }
        currentPosition.SetPositionTaken(true);
        ballPos[x,y+1].SetPositionTaken(true);




        if (Input.GetKey(up))
        {
            staff.SetActive(false);
            staffUp.SetActive(true);
            staffDown.SetActive(false);
            staffPos = StaffPosition.UP;
            if(frogball.getDirection() == Direction.UP || frogball.getDirection() == Direction.DOWN)
            {
                if(playerType==PlayerType.LEFT)
                {
                    ballPos[x + 1, y].SetPositionTaken(true);
                }
                else
                {
                    ballPos[x - 1, y].SetPositionTaken(true);
                }
            }

        }
        else if (Input.GetKey(down))
        {
            staff.SetActive(false);
            staffUp.SetActive(false);
            staffDown.SetActive(true);
            staffPos = StaffPosition.DOWN;
            if (frogball.getDirection() == Direction.UP || frogball.getDirection() == Direction.DOWN)
            {
                if (playerType == PlayerType.LEFT)
                {
                    ballPos[x + 1, y + 1].SetPositionTaken(true);
                }
                else
                {
                    ballPos[x - 1, y + 1].SetPositionTaken(true);
                }
            }
        }
        else
        {
            staff.SetActive(true);
            staffUp.SetActive(false);
            staffDown.SetActive(false);
            staffPos = StaffPosition.NORMAL;
        }
        //movement

        if (!Input.GetKey(action)) 
        {
            if (movementTimer > movementTime)
            {
                PossiblePosition newPosition;
                if (Input.GetKey(up))
                {
                    newPosition = currentPosition.GetUpPosition();
                    if (newPosition != null)
                    {
                        if (!newPosition.ballBlocked)
                        {
                            currentPosition.SetPositionTaken(false);
                            ballPos[x, y + 1].SetPositionTaken(false);
                            if (playerType == PlayerType.LEFT)
                            {
                                ballPos[x + 1, y].SetPositionTaken(false);
                                ballPos[x + 1, y + 1].SetPositionTaken(false);
                            }
                            else
                            {
                                ballPos[x - 1, y].SetPositionTaken(false);
                                ballPos[x - 1, y + 1].SetPositionTaken(false);
                            }
                            gameObject.transform.position = newPosition.transform.position;
                            currentPosition = newPosition;
                            movementTimer = 0;
                        }
                    }
                    staff.SetActive(false);
                    staffUp.SetActive(true);
                    staffDown.SetActive(false);
                    staffPos = StaffPosition.UP;
                }
                else if (Input.GetKey(down))
                {
                    newPosition = currentPosition.GetDownPosition();
                    if (newPosition != null)
                    {
                        if (!newPosition.ballBlocked)
                        {
                            currentPosition.SetPositionTaken(false);
                            ballPos[x, y + 1].SetPositionTaken(false);
                            if (playerType == PlayerType.LEFT)
                            {
                                ballPos[x + 1, y].SetPositionTaken(false);
                                ballPos[x + 1, y + 1].SetPositionTaken(false);
                            }
                            else
                            {
                                ballPos[x - 1, y].SetPositionTaken(false);
                                ballPos[x - 1, y + 1].SetPositionTaken(false);
                            }
                            gameObject.transform.position = newPosition.transform.position;
                            currentPosition = newPosition;
                            movementTimer = 0;
                        }
                    }
                    staff.SetActive(false);
                    staffUp.SetActive(false);
                    staffDown.SetActive(true);
                    staffPos = StaffPosition.DOWN;
                }
                else if (Input.GetKey(left))
                {
                    newPosition = currentPosition.GetLeftPosition();
                    if (newPosition != null)
                    {
                        if (!newPosition.ballBlocked)
                        {
                            currentPosition.SetPositionTaken(false);
                            ballPos[x, y + 1].SetPositionTaken(false);
                            if (playerType == PlayerType.LEFT)
                            {
                                ballPos[x + 1, y].SetPositionTaken(false);
                                ballPos[x + 1, y + 1].SetPositionTaken(false);
                            }
                            else
                            {
                                ballPos[x - 1, y].SetPositionTaken(false);
                                ballPos[x - 1, y + 1].SetPositionTaken(false);
                            }
                            gameObject.transform.position = newPosition.transform.position;
                            currentPosition = newPosition;
                            movementTimer = 0;
                        }
                    }
                }
                else if (Input.GetKey(right))
                {
                    newPosition = currentPosition.GetRightPosition();
                    if (newPosition != null)
                    {
                        if (!newPosition.ballBlocked)
                        {
                            currentPosition.SetPositionTaken(false);
                            ballPos[x, y + 1].SetPositionTaken(false);
                            if (playerType == PlayerType.LEFT)
                            {
                                ballPos[x + 1, y].SetPositionTaken(false);
                                ballPos[x + 1, y + 1].SetPositionTaken(false);
                            }
                            else
                            {
                                ballPos[x - 1, y].SetPositionTaken(false);
                                ballPos[x - 1, y + 1].SetPositionTaken(false);
                            }
                            gameObject.transform.position = newPosition.transform.position;
                            currentPosition = newPosition;
                            movementTimer = 0;
                        }
                    }
                }
            }
        }
        movementTimer += Time.deltaTime;
    }

    public StaffPosition GetStaffPosition() { return staffPos; }
}
