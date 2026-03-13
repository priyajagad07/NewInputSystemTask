using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;
    public int numberofItems;

    public float minX = -0.9f;
    public float maxX = 0.4f;
    public float minY = 1f;
    public float maxY = 1f;
    public float minZ = -0.21f;
    public float maxZ = 1.2f;

    void Start()
    {
        for (int i = 0; i < numberofItems; i++)
        {
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 SpawnPos = new Vector3(randomX, randomY, randomZ);

        int RandomItem = Random.Range(0, items.Length);
        Instantiate(items[RandomItem], SpawnPos, Quaternion.identity);
    }
}


