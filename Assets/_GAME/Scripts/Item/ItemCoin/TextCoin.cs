using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCoin : MonoBehaviour
{
    [Header("Coin parameter")]
    public int numCoin;
    public Text textCoin;
    public float moveSpeed = 5f;

    [Header("Time Destroy")]
    public int timeDestroy;

    private void Start()
    {
        textCoin.text = "+" + numCoin.ToString();
        Destroy(this.gameObject, timeDestroy);
    }   

    private void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }
}
