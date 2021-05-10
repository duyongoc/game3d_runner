using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : StateCharacter
{
    [Header("Set Speed up")]
    public GameObject speedUpEffect;
    private float timer = 0;

    [Header("Test")]
    public Transform shape;
    public Transform centerPos1;
    public Transform centerPos2;

    //
    public GameObject ParticleTurning;
    public float timeParTurning = 0.2f;
    private float timerTurning = 0f;

    public GameObject trail;
    GameObject temp = null;
    bool isCreate = false;

    //
    //= private 
    private MainCharacter character;
    private Vector3 vectorMovement;
    private int indexFoot = 0;


    #region UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();
    }

    // private void Update()
    // {
    // }
    #endregion


    #region STATE OF CHARACTER
    public override void StartState()
    {
        base.StartState();

        this.gameObject.SetActive(true);
        speedUpEffect.SetActive(false);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!GameMgr.Instance.IsGameRunning)
            return;

        UpdateVirtualMovement();
        // UpdateTouchMovement();

    }

    public override void EndState()
    {
        base.EndState();

        if (temp != null)
        {
            temp.transform.parent = null;
            Destroy(temp, 2f);
        }
    }
    #endregion


    private void UpdateVirtualMovement()
    {
        float moveHorizontal = character.VirtualMovement.GetDirection.x;
        float moveVertical = character.VirtualMovement.GetDirection.y;
        vectorMovement.Set(moveHorizontal, 0, moveVertical);
        vectorMovement = vectorMovement.normalized * Time.deltaTime * character.GetMoveSpeed;

        character.GetRigidbody.MovePosition(transform.position + vectorMovement);
    }


    private void UpdateTouchMovement()
    {
        if (!character.GetAnimator.GetCurrentAnimatorStateInfo(0).IsName("FastRun"))
            character.GetAnimator.SetBool("Moving", true);

        if (Input.GetMouseButton(0))
        {
            if (!isCreate)
            {
                temp = Instantiate(trail, transform.position, Quaternion.identity);
                temp.transform.parent = transform;
                isCreate = true;
            }

            float moveTurn = Input.mousePosition.x;
            if (moveTurn < Screen.width / 2 && moveTurn > 0)
            {
                transform.Rotate(-Vector3.up, character.GetTurnSpeed * Time.deltaTime, Space.World);

                Vector3 vec = centerPos1.position - transform.position;
                character.GetRigidbody.AddForce(vec.normalized * character.GetEngineForce, ForceMode.Impulse);
                character.GetRigidbody.AddTorque(vec.normalized * character.GetEngineForce * 2, ForceMode.Impulse);

                shape.localRotation = Quaternion.Lerp(shape.localRotation, Quaternion.Euler(0f, 0f, 100f), Time.deltaTime * 5);

            }
            else if (moveTurn > Screen.width / 2 && moveTurn < Screen.width)
            {
                transform.Rotate(Vector3.up, character.GetTurnSpeed * Time.deltaTime, Space.World);

                Vector3 vec = centerPos2.position - transform.position;
                //vec =  new Vector3(vec.x, 0f, vec.z);
                character.GetRigidbody.AddForce(-vec.normalized * character.GetEngineForce, ForceMode.Impulse);
                character.GetRigidbody.AddTorque(-vec.normalized * character.GetEngineForce * 2, ForceMode.Impulse);

                shape.localRotation = Quaternion.Lerp(shape.localRotation, Quaternion.Euler(0f, 0f, -100f), Time.deltaTime * 5);
            }
            timerTurning += Time.deltaTime;
            if (timerTurning > timeParTurning)
            {
                Instantiate(ParticleTurning, transform.position, Quaternion.identity);
                timerTurning = 0;
            }

        }
        else
        {
            if (isCreate)
            {
                if (temp != null)
                {
                    temp.transform.parent = null;
                    Destroy(temp, 2f);
                }
                isCreate = false;
            }
            else
            {
                timer += Time.deltaTime;
                if (timer > character.GetTimeParMoving)
                {
                    //int rand = Random.Range(0, 180);
                    indexFoot = indexFoot == 2 ? 0 : 1;
                    Instantiate(character.GetParticleMoving, character.GetCharacterFeet[indexFoot++].position, Quaternion.Euler(-90, 0, 0));

                    timer = 0;
                }
            }
        }

        if (shape.rotation.z != 0f)
            shape.localRotation = Quaternion.Lerp(shape.localRotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * 5);

        transform.Translate(Vector3.forward * character.GetMoveSpeed * Time.deltaTime);

    }


    private Vector3 Forward()
    {
        return Vector3.forward * Vector3.Dot(character.GetRigidbody.velocity, transform.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TheHole")
        {
            character.ChangeState(character.GetCharacterHolding);
            character.GetCharacterHolding.SetTarget(other.transform);
        }
        else if (other.tag.Contains("Enemy") && this.gameObject.tag != "PlayerAbility")
        {
            character.SetPlayerDead();

            // loading gameover scene;
            // SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_sceneGameOver);
        }
        else if (other.tag == "ItemSpeed")
        {
            speedUpEffect.SetActive(true);
            character.IncreaseSpeed();

            other.gameObject.GetComponent<ItemSpeed>().MakeEffect();
            Invoke("ResetSpeed", character.GetTimerSpeedUp);
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            // loading gameover scene;
            // var mgr = SceneMgr.GetInstance();
            // mgr.ChangeState(mgr.m_sceneGameOver);

            // Instantiate(character.ballExplosion, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    public void ResetSpeed()
    {
        character.ResetSpeed();
        speedUpEffect.SetActive(false);
    }

    public void Reset()
    {
        if (temp != null)
            Destroy(temp);

        character.GetComponent<Collider>().enabled = true;
        shape.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }


    private void CacheDefine()
    {

    }

    private void CacheComponent()
    {
        character = MainCharacter.Instance;
    }
}
