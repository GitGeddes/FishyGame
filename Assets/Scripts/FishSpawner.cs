using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeReference]
    public GameObject fishTemplate;
    [SerializeReference]
    public GameObject playerFish;
    [SerializeReference]
    private SpawnerRow spawnerRow;
    [SerializeField]
    public float top = 4.5f;
    [SerializeField]
    public float bottom = -4.5f;
    private float distance = 0f;
    [SerializeField]
    public float left = -12f;
    [SerializeField]
    public float right = 12f;
    [SerializeField]
    public int maxFish = 20;
    [SerializeField]
    public int fishCount = 0;
    [SerializeField]
    public float spacing = 2.0f;
    public int rowCount = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distance = top - bottom;
        //for (int i = 0; i < rowCount; i++)
        //{
        //    MakeSpawnerRow(i);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (fishTemplate != null && fishCount < maxFish)
        {
            fishCount++;
            float spawnInterval = Random.Range(0.5f, 10.0f);
            Invoke(nameof(SpawnFish), spawnInterval);
        }
    }

    private void MakeSpawnerRow(int index)
    {
        SpawnerRow row;
        if (index % 2 == 0)
        {
            row = new SpawnerRow(maxFish / 10, right, left, fishTemplate);
        } else
        {
            row = new SpawnerRow(maxFish / 10, left, right, fishTemplate);
        }
        Vector3 pos = new Vector3(
            0f,
            (distance / rowCount) * index - bottom,
            0f
        );
        Instantiate(row, pos, (index % 2 == 0) ? Quaternion.identity : Quaternion.AngleAxis(180.0f, Vector3.forward), transform);
    }

    private void SpawnFish()
    {
        bool randomSide = Random.Range(0.0f, 1.0f) > 0.5;
        int count = Mathf.FloorToInt(distance / spacing);
        float newSpacing = distance / (count + 1);
        Vector3 randomPos = new Vector3(
            0f,
            //(Random.Range(-count, count) * newSpacing) + (randomSide ? 0.0f : 1.0f),
            Random.Range(bottom, top),
            0f
        ) + new Vector3(
            randomSide ? left : right,
            0.0f,
            0.0f
        );
        Instantiate(fishTemplate, randomPos, (randomSide) ? Quaternion.identity : Quaternion.AngleAxis(180.0f, Vector3.up), transform);
    }

    public void DespawnFish(GameObject fish)
    {
        fishCount--;
        Destroy(fish);
    }
}
