using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossiblePosition : MonoBehaviour
{
    [SerializeField] PossiblePosition leftPosition;
    [SerializeField] PossiblePosition rightPosition;
    [SerializeField] PossiblePosition upPosition;
    [SerializeField] PossiblePosition downPosition;


    [SerializeField] bool isPlayerStartingPos = false;

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
    public bool IsPlayerStartingPos() {  return isPlayerStartingPos; }
}
