using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerRow", menuName = "Scriptable Objects/SpawnerRow")]
public class SpawnerRow : ScriptableObject
{
    private int maxFish;
    private int fishCount = 0;
    [SerializeReference]
    public GameObject fishTemplate;
    private float start;
    private float end;
    [SerializeField]
    public float startDelay = 0.5f;
    [SerializeField]
    public float endDelay = 10.0f;

    public SpawnerRow(int maxFish, float start, float end, GameObject fishTemplate)
    {
        this.maxFish = maxFish;
        this.start = start;
        this.end = end;
        this.fishTemplate = fishTemplate;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fishTemplate != null && fishCount < maxFish)
        {
            fishCount++;
            float spawnInterval = Random.Range(startDelay, endDelay);
            //Invoke(nameof(SpawnFish), spawnInterval);
        }
    }

    private void SpawnFish()
    {
        Vector3 randomPos = new Vector3(0f, start, 0f);
        Instantiate(fishTemplate, randomPos, (start - end > 0) ? Quaternion.identity : Quaternion.AngleAxis(180.0f, Vector3.forward));
    }

    public void DespawnFish(GameObject fish)
    {
        fishCount--;
        Destroy(fish);
    }
}
