using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // [protected] 
    protected Rigidbody mRigidbody;
    protected Animator mAnimator;
    protected Collider mCollider;
    protected Material marDissolve;
    protected Dictionary<SkinnedMeshRenderer, Material> d_skinedMeshRender;
    protected bool isDead = false;
    protected bool isWarning = false;


    // [properties] 
    public bool IsDead { get => isDead; set => isDead = value; }



    public void SetWarning(bool warning)
    {
        isWarning = warning;
    }


    protected void EnemyAppear()
    {
        mCollider.enabled = false;
        foreach (KeyValuePair<SkinnedMeshRenderer, Material> item in d_skinedMeshRender)
        {
            DissolveEnemy(item.Key, item.Value);
        }
    }


    protected void DissolveEnemy(SkinnedMeshRenderer skin, Material marDefault)
    {
        marDissolve.SetFloat("_processDissolve", 1);
        skin.material = marDissolve;
        mCollider.enabled = false;
        StartCoroutine(DissolveSkin(skin, marDefault));
    }


    private IEnumerator DissolveSkin(SkinnedMeshRenderer skin, Material marDefault)
    {
        float timer = 1;
        float process = 1;
        while (timer >= 0)
        {
            yield return new WaitForSeconds(0.005f);

            timer -= 0.005f;
            process -= 0.005f;
            marDissolve.SetFloat("_processDissolve", process);
        };

        marDissolve.SetFloat("_processDissolve", 0);
        skin.material = marDefault;
        mCollider.enabled = true;
    }

}
