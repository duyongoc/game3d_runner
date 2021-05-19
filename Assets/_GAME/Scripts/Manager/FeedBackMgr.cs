using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class FeedBackMgr : Singleton<FeedBackMgr>
{
    
    //
    //= inspector 
    public MMFeedbacks ENMEY_JUMP;
    public MMFeedbacks ENMEY_ELASTIC;


    public void PlayFeedBack(MMFeedbacks feedbacks)
    {
        feedbacks.PlayFeedbacks();
    }


}
