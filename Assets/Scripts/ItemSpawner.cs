using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] cheapItems;

    [SerializeField] private GameObject[] expensiveItems;

    public int spawnCount = 0;

    public int countTillStoneSpawned = 0;

    public int countNeededForStoneSpawn = 0;

    public float maxSpawnX = 18f;

    public float maxSpawnY = -8f;

    public float minSpawnX = -18f;

    public float minSpawnY = 8f;

    private List<Vector3> spawnLocationsUsed = new();
    private List<GameObject> items = new();

    private void Awake()
    {
        foreach (Vector3 item in spawnLocationsUsed)
            spawnLocationsUsed.Remove(item);
    }

    private void Start()
    {
        SetUpLevel();
        SpawnItems();
    }

    private void SetUpLevel()
    {
        var currentLevel = GameManager.instance.CurrentLevel;
        items.AddRange(cheapItems);
        spawnCount = currentLevel <= 100 ? currentLevel : 100;

        if (currentLevel <= 5)
        {
            GameManager.instance.CountDownTimer = 60;
            countNeededForStoneSpawn = 5;
        }
        else
        {
            items.AddRange(expensiveItems);
            GameManager.instance.CountDownTimer = currentLevel >= 20 ? currentLevel + 60 : 60;
            countNeededForStoneSpawn = Mathf.FloorToInt(spawnCount * 0.5f);
        }
        GameManager.instance.ShowTimer();
    }

    private void SpawnItems()
    {
        for (int i = 0; i <= spawnCount + 10; i++)
        {
            var item = RandomItem();
            RandomPosition(item);
            if (i <= spawnCount) CalculateScoreToWin(item);
        }
        GameManager.instance.DisplayScore(0);
    }

    private void CalculateScoreToWin(GameObject item)
    {
        if (!item.CompareTag("Stone"))
        {
            GameManager.instance.scoreToWin += item.GetComponent<Items>().scoreValue;
        }
    }

    private GameObject RandomItem()
    {
        // Random gold items from index 3 of list cheap items
        var item = items[Random.Range(3, items.Count)];

        // Random stone items from index 0 - 2 of list cheap items 
        if (countTillStoneSpawned == countNeededForStoneSpawn)
        {
            item = items[Random.Range(0, 3)];
            countTillStoneSpawned = 0;
        }
        else
        {
            countTillStoneSpawned++;
        }
        return item;
    }

    private void RandomPosition(GameObject item)
    {
        var itemPosition = new Vector3((int)Random.Range(minSpawnX, maxSpawnX), (int)Random.Range(minSpawnY, maxSpawnY), 0f);
        foreach (var position in spawnLocationsUsed)
        {
            if (itemPosition == position)
            {
                var randomX = Random.Range(minSpawnX + 2f, maxSpawnX - 2f);
                var randomY = Random.Range(minSpawnY + 2f, maxSpawnY - 2f);
                itemPosition = new Vector3(randomX, randomY, 0f);
            }
        }
        spawnLocationsUsed.Add(itemPosition);
        Instantiate(item, itemPosition, transform.rotation);
    }
}
