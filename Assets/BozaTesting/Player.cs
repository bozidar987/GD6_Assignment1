using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PossiblePosition[] positions;
    PossiblePosition currentPosition;

    [SerializeField] KeyCode up;
    [SerializeField] KeyCode down;
    [SerializeField] KeyCode left;
    [SerializeField] KeyCode right;
    [SerializeField] KeyCode action;

    [SerializeField] float movementTime;
    float movementTimer;

    //enum state { UP, DOWN, LEFT, RIGHT};
    //state s = state.UP;

    //int sw = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = null;
        movementTimer = 0;
        for (int i = 0; i < positions.Length; i++)
        {
            if (positions[i].IsPlayerStartingPos())
            {
                gameObject.transform.position = positions[i].gameObject.transform.position;
                currentPosition = positions[i];
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        if (Input.GetKeyDown(action))
        {

        }
        else
        {
            if (movementTimer > movementTime)
            {
                PossiblePosition newPosition;
                if (Input.GetKey(up))
                {
                    newPosition = currentPosition.GetUpPosition();
                    if (newPosition != null) 
                    {
                        gameObject.transform.position = newPosition.transform.position;
                        currentPosition = newPosition;
                        movementTimer = 0;
                    }
                }
                else if (Input.GetKey(down))
                {
                    newPosition = currentPosition.GetDownPosition();
                    if (newPosition != null)
                    {
                        gameObject.transform.position = newPosition.transform.position;
                        currentPosition = newPosition;
                        movementTimer = 0;
                    }
                }
                else if (Input.GetKey(left))
                {
                    newPosition = currentPosition.GetLeftPosition();
                    if (newPosition != null)
                    {
                        gameObject.transform.position = newPosition.transform.position;
                        currentPosition = newPosition;
                        movementTimer = 0;
                    }
                }
                else if (Input.GetKey(right))
                {
                    newPosition = currentPosition.GetRightPosition();
                    if (newPosition != null)
                    {
                        gameObject.transform.position = newPosition.transform.position;
                        currentPosition = newPosition;
                        movementTimer = 0;
                    }
                }
            }
        }
        movementTimer += Time.deltaTime;
    }
}
