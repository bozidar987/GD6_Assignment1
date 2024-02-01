using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PositionType
{
    HORIZONTALBORDER,
    VERTICALBORDER,
    GOAL,
    NORMAL
}


public class PossiblePosition : MonoBehaviour
{
    bool positionTaken = false;
    [SerializeField] PossiblePosition leftPosition;
    [SerializeField] PossiblePosition rightPosition;
    [SerializeField] PossiblePosition upPosition;
    [SerializeField] PossiblePosition downPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public PossiblePosition GetLeftPosition() { return leftPosition; }
    public PossiblePosition GetRightPosition() { return rightPosition; }
    public PossiblePosition GetUpPosition() { return upPosition; }
    public PossiblePosition GetDownPosition() { return downPosition; }

    public void SetPositionTaken(bool value) { positionTaken = value; }
    public bool GetPositionTaken() { return positionTaken; }

}
