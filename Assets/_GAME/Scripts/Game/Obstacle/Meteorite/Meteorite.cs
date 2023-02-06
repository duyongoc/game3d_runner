using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{

    [Header("[Setting]")]
    public GameObject shape;
    public GameObject effect;
    public ParticleSystem particle;
    public float speed = 5;



    #region UNITY
    private void Start()
    {
    }

    private void Update()
    {
        if (shape)
            shape.transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (shape && shape.transform.position.y <= 0)
        {
            particle.gameObject.SetActive(true);
            Camera.main.GetComponent<CameraFollow>().MakeCameraShake(1, 0.2f);

            if (!particle.isPlaying)
                particle.Play();

            Destroy(shape);
            Invoke("OnDestroy", 2);
            //Instantiate(effect, shape.transform.position, Quaternion.identity);
        }
    }
    #endregion


    private void OnDestroy()
    {
        Destroy(this.gameObject);
    }

}
