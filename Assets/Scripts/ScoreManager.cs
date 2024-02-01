using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    public Text scorep1Text;
    public Text scorep2Text;
    
    int p1score = 0;
    int p2score = 0;

    private void Awake()
    {
        instance = this;
        //add ScoreManager.instance.AddPointP1(); where applicable in bozicode for player 1
        //add ScoreManager.instance.AddPointP2(); where applicable in bozicode for player 2
        //I think this should work according to the tutorial: //https://www.youtube.com/watch?v=YUcvy9PHeXs
    }


    // Start is called before the first frame update
    void Start()
    {
        scorep1Text.text = p1score.ToString() + " POINTS";
        scorep2Text.text = p2score.ToString() + " POINTS";
    }

    public void AddPointP1()
    {
        p1score += 1;
        scorep1Text.text = p1score.ToString() + " POINTS";
    }

    public void AddPointP2()
    {
        p2score += 1;
        scorep2Text.text = p2score.ToString() + " POINTS";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
