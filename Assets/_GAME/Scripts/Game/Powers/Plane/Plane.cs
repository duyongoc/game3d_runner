using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{


    //
    //= private 
    private MainCharacter character;
    private float moveSpeed = 5f;
    private float distance = 5.5f;


    #region UNITY
    private void Start()
    {
        CacheComponent();
    }

    private void Update()
    {
        MovetoCharacter();
    }
    #endregion

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
