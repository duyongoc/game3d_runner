using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGlobe : MonoBehaviour
{
    [Header("Load data Enemy 3")]
    public ScriptEnemyGlobe scriptEnemy;

    [Header("Enemy dead explosion")]
    public GameObject explosion;
    public GameObject explosionSpecial;
    public int heath = 100;
    
    [Header("Particle armor")]
    public GameObject particleArmor;

    [Header("Warning the player")]
    public GameObject warningIcon;
    public bool isWarning = false;

    //enemy's target
    public Transform target;
    private Rigidbody2D m_rigidbody2D;
    private float moveSpeed = 0;
    private float distanceWarning;

    //enmey state
    public enum EnemyState{ Moving, Attraction, None }
    public EnemyState currentState = EnemyState.Moving;

    private void LoadData()
    {
        moveSpeed = scriptEnemy.moveSpeed;
        distanceWarning = scriptEnemy.distanceWarning;
    }

    private void Start()
    {
        LoadData();

        m_rigidbody2D = GetComponent<Rigidbody2D>();
        target = MainCharacter.Instance.GetTransform();
    }
    
    void Update()
    {
        if (GameMgr.Instance.IsGameRunning)
        {
            switch(currentState)
            {
                case EnemyState.Moving:
                {
                    EnemyMoving();
                    break;
                }
                case EnemyState.Attraction:
                {
                    //EnenmyHolding();
                    break;
                }
                case EnemyState.None:
                {

                    break;
                }
            }
        }
    }

    private void EnemyMoving()
    {
        if(isWarning)
        {
            GetWarningFromEnemy();
        }

        transform.position = Vector3.MoveTowards(transform.position,target.position, Time.deltaTime * moveSpeed);
    }

    private void GetWarningFromEnemy()
    {
        if(Vector3.SqrMagnitude(transform.position - target.position) <= distanceWarning * distanceWarning)
        {
            warningIcon.SetActive(true);
            StartCoroutine("FinishWarningEnemyGlobe");
        }
    }

    IEnumerator FinishWarningEnemyGlobe()
    {
        yield return new WaitForSeconds(2f);
        warningIcon.SetActive(false);
        isWarning = false;
    }

    public void SetWarning(bool warning)
    {
        isWarning = warning;
    }

    //Collider
    public void TakeDestroy()
    {
        heath -= 50;
        if(heath <= 0)
        {
            Instantiate(explosionSpecial, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Contains("Enemy"))
        {
            var temp = other.GetComponent<IOnDestroy>();
            if(temp != null)
                temp.TakeDestroy();

            Instantiate(explosion, transform.localPosition, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag.Contains("BallPower"))
        {
            Instantiate(explosionSpecial, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
