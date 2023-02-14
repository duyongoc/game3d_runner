using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{


    public enum PlaneState
    {
        Wait,
        Move,
        None
    }


    [Header("[Param]")]
    [SerializeField] private float distance = 5.5f;
    [SerializeField] private float moveSpeed = 5f;
    public PlaneState curState = PlaneState.Wait;
    public List<Entity> enemies;

    [Header("[Shooting]")]
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private GameObject prefabBulletExplosion;
    [SerializeField] private float timeShoot = 1f;


    // [private] 
    private float timeRemain = 0f;
    private MainCharacter character;
    private Vector3 curTarget;

    private Entity currentEnemy;
    private Entity entity;


    // [properties]
    public Entity GetCurrentEnemy { get => currentEnemy; }
    public Entity GetEntity { get => entity; }



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
                StateWaitPlane(); break;

            case PlaneState.Move:
                StateMovePlane(); break;

            case PlaneState.None:
                StateNone(); break;
        }

        OnUpdateEnemy();
        OnUpdateAttack();
    }
    #endregion



    private void StateWaitPlane()
    {
        if (Vector3.Distance(transform.position, character.transform.position) > distance)
        {
            ChangeState(PlaneState.Move);
        }
    }


    private void StateMovePlane()
    {
        transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(character.transform.position.x, 5f, character.transform.position.z),
                moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, character.transform.position) > distance - 2f)
        {
            ChangeState(PlaneState.Wait);
        }
    }


    private void StateNone()
    {
    }


    private void ChangeState(PlaneState newState)
    {
        curState = newState;
    }


    private void OnUpdateAttack()
    {
        if (currentEnemy != null)
        {
            timeRemain += Time.deltaTime;
            if (timeRemain > timeShoot)
            {
                if (IsCurrentEnemyDied())
                    return;

                var bullet = prefabBullet.SpawnToGarbage(transform.position, Quaternion.identity);
                bullet.GetComponent<PlaneBullet>().Init(currentEnemy.transform.position, 20, prefabBulletExplosion);
                timeRemain = 0;
            }
        }
    }


    private void OnUpdateEnemy()
    {
        enemies.Clear();
        foreach (Entity ene_detected in entity.detected)
        {
            if (ene_detected != null)
            {
                if (ene_detected.tag.Contains("Enemy") || ene_detected.tag == "Tornado")
                {
                    enemies.Add(ene_detected);
                }
            }
        }
        if (enemies.Count > 0)
        {
            currentEnemy = enemies[0];
            foreach (var enemy in enemies)
            {
                float dist1 = Vector3.Distance(transform.position, currentEnemy.transform.position);
                float dist2 = Vector3.Distance(transform.position, enemy.transform.position);

                if (dist2 < dist1)
                    currentEnemy = enemy;
            }
        }
    }


    private bool IsCurrentEnemyDied()
    {
        return currentEnemy == null ? true : currentEnemy.GetComponent<Enemy>().IsDead;
    }


    private void CacheComponent()
    {
        character = MainCharacter.Instance;
        entity = GetComponent<Entity>();
    }

}
