using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTransformPlayer : MonoBehaviour
{
    public static GetTransformPlayer getTransformPlayer;

    void Awake()
    {
        if(getTransformPlayer != null)
            return;
        getTransformPlayer = this;
    }

    public Transform GetTransform()
    {
        return getTransformPlayer.gameObject.transform;
    }
    

}
