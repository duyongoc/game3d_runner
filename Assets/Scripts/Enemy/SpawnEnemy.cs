using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
	[Header("Enemies will be create")]
    public GameObject[] enemyPrefab;

	[Header("Transform to create")]
	public Transform[] transformPrefab;

	[Header("Spawn enemy")]
	private float timerProcess = 0;
	public float timeToSpawn = 3.0f;
	

	public List<GameObject> enemyWasCreated;


	private void Start () 
	{
		
	}
	
	private void Update () 
	{
		timerProcess += Time.deltaTime;
		if (timerProcess >= timeToSpawn) 
		{
			SpawEnemy();
			
			timerProcess = 0;
		}	
	}

	private void SpawEnemy()
	{
		int randEne = Random.Range(0, enemyPrefab.Length);
		int randTran = Random.Range(0, transformPrefab.Length);

		GameObject obj = Instantiate(enemyPrefab[randEne], transformPrefab[randTran].position, Quaternion.identity);
		enemyWasCreated.Add(obj);
	}

	public void Reset()
	{
		foreach(GameObject obj in enemyWasCreated)
		{
			if(obj != null)
				Destroy(obj);
		}

	}
}
