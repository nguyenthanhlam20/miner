using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
	public GameObject[] items;

	public int spawnCount;

	public int countTillStoneSpawned;

	public int countNeededForStoneSpawn;

	public float maxSpawnX = 13f;

	public float maxSpawnY = -8f;

	public float minSpawnX = -13f;

	public float minSpawnY = 8f;

	private List<Vector3> spawnLocationsUsed = new List<Vector3>();

	private void Awake()
	{
		foreach (Vector3 item in spawnLocationsUsed)
		{
			spawnLocationsUsed.Remove(item);
		}
	}

	private void Start()
	{
		spawnCount = Random.Range(10, 15);
		countNeededForStoneSpawn = (int)((double)spawnCount * 0.25);
		SpawnItems();
	}

	private void SpawnItems()
	{
		for (int i = 0; i <= spawnCount; i++)
		{
			var gameObject = items[Random.Range(0, items.Length)];
			if ((bool)gameObject != (base.gameObject.tag == "Stone") && countTillStoneSpawned == countNeededForStoneSpawn)
			{
				gameObject = items[items.Length - 1];
				countTillStoneSpawned = 0;
			}
			else
			{
				countTillStoneSpawned++;
			}
            GameManager.instance.scoreToWin += gameObject.GetComponent<Items>().scoreValue;
            var vector = new Vector3((int)Random.Range(minSpawnX, maxSpawnX), (int)Random.Range(minSpawnY, maxSpawnY), 0f);
			foreach (Vector3 item in spawnLocationsUsed)
			{
				if (vector == item)
				{
					vector = new Vector3(Random.Range(-13, 14), Random.Range(-8, 2), 0f);
				}
			}
			spawnLocationsUsed.Add(vector);
			Object.Instantiate(gameObject, vector, base.transform.rotation);
		}
        GameManager.instance.DisplayScore(0);
    }
}
