using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMgr : MonoBehaviour
{
    [Header("Score")]
    public float score;
    public int highscore;

    public static ScoreMgr s_instance;
    
    private void Awake()
    {
        if(s_instance != null)
            return;
        s_instance = this;
    }

    public static ScoreMgr GetInstance()
    {
        return s_instance;
    }

    public void PlusScore(int score)
    {
        this.score += score;   
    }

    public void Reset()
    {
        score = 0;
    }



}
