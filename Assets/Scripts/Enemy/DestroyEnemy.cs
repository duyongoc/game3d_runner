using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public GameObject explosionSpecial;

    public void DestroyWithExplosion()
    {
        Instantiate(explosionSpecial, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
