using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{

    //
    //= inspector
    [SerializeField] private Transform[] arrayGround;


    //
    //= private 
    private Transform[,] array2D = new Transform[3, 3];



    #region UNITY
    private void Start()
    {
        Init2dArray();
        ArrangeMap(Vector3.zero);
    }

    // private void Update()
    // {
    // }
    #endregion

    public void ArrangeMap(Vector3 originPos)
    {
        float x = originPos.x - 150;
        float z = originPos.z + 150;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                array2D[i, j].position = new Vector3(x, 0f, z);
                x += 150;
            }
            x = originPos.x - 150;
            z -= 150;
        }
    }


    private void Init2dArray()
    {
        int i = 0;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                array2D[x, y] = arrayGround[i++];
            }
        }
    }


}
