using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMgr : MonoBehaviour
{
    [Header("Score")]
    public int score;
    public int highscore;
    
    private void Start()
    {
        //highscore = PlayerPrefs.GetFloat("highscore", highscore);
    }

    private void Update()
    {
       
    }

    public void Reset()
    {
        score = 0;
    }



}
