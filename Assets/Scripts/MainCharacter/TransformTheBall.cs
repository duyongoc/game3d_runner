using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTheBall : MonoBehaviour
{
    public static TransformTheBall s_instance;

    void Awake()
    {
        if(s_instance != null)
            return;
        s_instance = this;
    }

    public static TransformTheBall GetInstance()
    {
        return s_instance;
    }

    public Transform GetTransform()
    {
        return s_instance.gameObject.transform;
    }

}
