using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{

    //
    //= private 
    private GameMgr gameMgr;
    private Vector3 velocity;
    private Transform target;
    private float smoothFactor = 0f;
    private float timeZoomCamera = 0f;

    private bool hasStart = false;
    private bool hasZoom = false;
    private float moveSpeed = 0f;

    private float originY = 0f;
    private float originZ = 0f;
    private float currentY = 0f;
    private float currentZ = 0f;


    //
    //= properties
    public bool HasStart { get => hasStart; set => hasStart = value; }




    #region UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();

        GameMgr.Instance.EVENT_RESET_INGAME += Reset;
    }

    private void FixedUpdate()
    {
        if (target == null)
            target = MainCharacter.Instance.GetTransform();

        Vector3 newPostion = new Vector3(target.position.x, transform.position.y, target.position.z);
        newPostion.z += currentZ;
        transform.position = Vector3.SmoothDamp(transform.position, newPostion, ref velocity, smoothFactor * Time.deltaTime);

        CheckZoomCamera();
    }
    #endregion

    public void ChangeTarget(Transform tar, float smoother)
    {
        smoothFactor = smoother;
        target = tar;
    }

    private void CheckZoomCamera()
    {
        if (!hasZoom && (gameMgr.GetTimePlay > timeZoomCamera))
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 75, Time.deltaTime * (moveSpeed / 2));
            if (Camera.main.fieldOfView >= 74f)
            {
                hasZoom = true;
            }
        }

    }


    public bool IsSetUpCamera()
    {
        if (!hasStart)
            return false;

        currentY = 20f;
        if (currentZ >= -18f)
            currentZ -= 0.1f;

        Vector3 vecTarget = new Vector3(transform.position.x, currentY, currentZ);
        transform.position = Vector3.MoveTowards(transform.position, vecTarget, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, vecTarget) <= 0.2f)
            return true;

        return false;
    }

    public void MakeCameraShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(transform.position.x + x, originalPos.y, transform.position.z + z);
            elapsed += Time.deltaTime;

            yield return null;
        }
    }

    public void Reset()
    {
        currentY = originY;
        currentZ = originZ;
        hasStart = false;

        Camera.main.fieldOfView = 60f;
        transform.position = new Vector3(transform.position.x, currentY, currentZ);
    }


    private void CacheDefine()
    {
        smoothFactor = gameMgr.CONFIG_GAME.smoothFactor;
        timeZoomCamera = gameMgr.CONFIG_GAME.timeZoomCamera;
        moveSpeed = gameMgr.CONFIG_GAME.moveSpeed;

        currentY = originY = gameMgr.CONFIG_GAME.posOriginY;
        currentZ = originZ = gameMgr.CONFIG_GAME.posOriginZ;
        velocity = Vector3.zero;
    }

    private void CacheComponent()
    {
        gameMgr = GameMgr.Instance;
        target = MainCharacter.Instance.GetTransform();
    }
}
