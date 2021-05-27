using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{

    //
    //= public
    public enum PlaneState { Wait, Move, None }
    public PlaneState curState = PlaneState.Wait;


    //
    //= private 
    private MainCharacter character;
    private Vector3 curTarget;
    private float moveSpeed = 5f;
    private float distance = 5.5f;


    #region UNITY
    private void Start()
    {
        CacheComponent();
    }

    private void Update()
    {
        switch (curState)
        {
            case PlaneState.Wait:
                StateWaitPlane();
                break;

            case PlaneState.Move:
                StateMovePlane();
                break;

            case PlaneState.None:
                StateNone();
                break;
        }
    }
    #endregion


    private void StateWaitPlane()
    {

    }

    private void StateMovePlane()
    {

    }

    private void StateNone()
    {

    }

    // MovetoCharacter();

    private void MovetoCharacter()
    {
        if (Vector3.Distance(transform.position, character.transform.position) > distance)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(character.transform.position.x,
                    5f,
                    character.transform.position.z), moveSpeed * Time.deltaTime);
        }
    }


    private void CacheComponent()
    {
        character = MainCharacter.Instance;
    }

}
