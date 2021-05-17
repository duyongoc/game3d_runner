using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{

    //
    //= public
    public float smoothFactor = 0.15f;

    [Header("Position origin")]
    public float originY = 15f;
    public float originZ = -10f;
    public float moveSpeed = 5f;
    public bool isStart = false;


    //
    //= private 
    private Vector3 velocity;
    private Transform target;

    private bool isFlowCamera = false;
    private float currentY = 0f;
    private float currentZ = 0f;


    //
    //= properties
    public bool IsFlowCamera { get => isFlowCamera; set => isFlowCamera = value; }



    #region UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();
    }

    private void FixedUpdate()
    {
        if (target == null)
            target = MainCharacter.Instance.GetTransform();

        Vector3 newPostion = new Vector3(target.position.x, transform.position.y, target.position.z);
        newPostion.z += currentZ;
        transform.position = Vector3.SmoothDamp(transform.position, newPostion, ref velocity, smoothFactor * Time.deltaTime);

        if (isFlowCamera)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 75, Time.deltaTime * (moveSpeed / 2));

            if (Camera.main.fieldOfView >= 74f)
                isFlowCamera = false;
        }

    }
    #endregion

    public void ChangeTarget(Transform tar, float smoother)
    {
        smoothFactor = smoother;
        target = tar;
    }

    private void ZoomMainCamera(Vector3 origin, Vector3 target, float speed)
    {
        transform.position = Vector3.MoveTowards(origin, target, Time.deltaTime * speed);
    }


    public bool IsSetUpCamera()
    {
        if (!isStart)
            return false;

        currentY = 20f;
        if (currentZ >= -18f)
            currentZ -= 0.1f;

        Vector3 vecTarget = new Vector3(transform.position.x, currentY, currentZ);
        ZoomMainCamera(transform.position, vecTarget, moveSpeed);

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
        isStart = false;

        Camera.main.fieldOfView = 60f;
        transform.position = new Vector3(transform.position.x, currentY, currentZ);
    }


    private void CacheDefine()
    {
        velocity = Vector3.zero;
        currentY = originY;
        currentZ = originZ;
    }

    private void CacheComponent()
    {
        target = MainCharacter.Instance.GetTransform();
    }
}
