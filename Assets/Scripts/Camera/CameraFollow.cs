using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform m_target;

    Vector3 velocity = Vector3.zero;
    public float smoothFactor = 0.15f;

    [Header("Position origin")]
    public float originY = 15f;
    public float originZ = -10f;
    public float moveSpeed = 5f;
    public bool isStart = false;

    [Header("//Debug")]
    public float currentY = 0f;
    public float currentZ = 0f;

    public bool isFlowCamera = false;

    //private variable
    private Animator m_animator;

    private void OnLoad()
    {
        currentY = originY; 
        currentZ = originZ;
    }

    #region UNITY
    private void Start()
    {
        m_animator = GetComponent<Animator>();
        this.OnLoad();
    }

    private void FixedUpdate()
    {
        if (m_target == null)
        {
            m_target = TransformTheBall.GetInstance().GetTransform();
        }

        Vector3 newPostion = new Vector3(m_target.position.x, transform.position.y, m_target.position.z);
        newPostion.z += currentZ;
        transform.position = Vector3.SmoothDamp(transform.position, newPostion, ref velocity, smoothFactor * Time.deltaTime);
    
        if(isFlowCamera)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 75, Time.deltaTime * (moveSpeed/2));

            if(Camera.main.fieldOfView >= 74f)
                isFlowCamera = false;
        }

    }

    public void ChangeTarget(Transform tar, float smoother)
    {
        smoothFactor = smoother;
        m_target = tar;
    }
    #endregion

    private void ZoomMainCamera(Vector3 origin, Vector3 target, float speed)
    {
        transform.position = Vector3.MoveTowards(origin, target, Time.deltaTime * speed);
    }
    
    public bool IsSetUpCamera()
    {
        if(!isStart)
            return false;

        currentY = 20f;
        if(currentZ >= -18f)
            currentZ -= 0.1f;

        Vector3 vecTarget = new Vector3(transform.position.x, currentY,  currentZ);
        ZoomMainCamera(transform.position, vecTarget, moveSpeed);

        if(Vector3.Distance(transform.position, vecTarget) <= 0.2f)
            return true;
        
        return false;
    }

    public void MakeCameraShake(float duration, float magnitude)
    {
        //m_animator.SetTrigger("Shake");
        StartCoroutine(Shake(duration,magnitude));
    }

    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3
                        (transform.position.x + x, originalPos.y, transform.position.z + z);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        //transform.localPosition = originalPos;
    }

    public void Reset()
    {
        isStart = false;
        this.OnLoad();

        Camera.main.fieldOfView = 60f;
        transform.position = new Vector3(transform.position.x, currentY, currentZ);
    }
}
