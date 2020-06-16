using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShape2 : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    private Enemy2 ene;

    private void Start()
    {
        ene = GetComponentInParent<Enemy2>();
    }


    private void Update()
    {
        switch(ene.currentState)
        {
            case Enemy2.EnemyState.Moving:
            {   
                transform.Rotate(Vector3.up, 300 * Time.deltaTime);
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);    

                break;
            }
            case Enemy2.EnemyState.Attraction:
            {
                transform.localPosition = new Vector3(0f, 0f, 0f);

                break;
            }
            
        }
        // transform.position = Vector3.MoveTowards(transform.position, nextPos, Time.deltaTime * moveSpeed);
        // if (Vector3.Distance(transform.position, nextPos) <= 0.05f)
        // {
        //     nextPos = nextPos != pos1.position ? pos1.position : pos2.position;
        // }
    }
}
