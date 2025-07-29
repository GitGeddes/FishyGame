using UnityEngine;

public class SpawnedFish : MonoBehaviour
{
    private FishSpawner parentScript;
    [SerializeField]
    private float m_speed = 4000.0f;
    [SerializeField]
    private float minSpeed = 4000.0f;
    [SerializeField]
    private float maxSpeed = 16000.0f;
    [SerializeField]
    public float m_scale = 1.0f;
    [SerializeField]
    public float minScale = 0.25f;
    [SerializeField]
    public float maxScale = 1.5f;
    //[SerializeField]
    //private int m_direction = 0;
    public float top = 0;
    public float bottom = 0;
    public float left = 0;
    public float right = 0;

    public int scaleScore = 0;
    [SerializeField]
    public int minScore = 1;
    [SerializeField]
    public int maxScore = 1000;

    public Color m_color = Color.white;
    [SerializeField]
    public Color minColor = new(1.0f, 0.62f, 0.333f);
    [SerializeField]
    public Color maxColor = new(0.333f, 0.5f, 1.0f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_speed = Random.Range(minSpeed, maxSpeed);
        SetScale(Random.Range(minScale, maxScale));

        parentScript = GetComponentInParent<FishSpawner>();
        top = parentScript.top + 1.0f;
        bottom = parentScript.bottom - 1.0f;
        left = parentScript.left;
        right = parentScript.right;

        if (transform.position.x < 0)
        {
            //m_direction = 1; // right
        } else
        {
            //m_direction = -1; // left
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if out of bounds
        if (parentScript != null)
        {
            Vector3 pos = transform.position;
            if (pos.x > right || pos.x < left || pos.y > top || pos.y < bottom)
            {
                // Despawn this fish once it's off screen
                parentScript.DespawnFish(this.gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        // Move forward every physics update
        if (TryGetComponent<Rigidbody2D>(out var rigidbody2D))
        {
            Vector2 newV = m_speed * transform.right;
            rigidbody2D.linearVelocity = newV;
        }
        //transform.Translate(m_direction * m_speed * Time.deltaTime * transform.right);
    }

    void SetScale(float newScale)
    {
        m_scale = newScale;
        transform.localScale = new Vector3(newScale, newScale, 1.0f);
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            float scaleDistance = maxScale - minScale;
            float scalePercent = newScale / scaleDistance;
            m_color = Color.Lerp(minColor, maxColor, scalePercent);
            spriteRenderer.color = m_color;

            scaleScore = (int) Mathf.Lerp(minScore, maxScore, scalePercent);
        } else
        {
            Debug.Log("sprite renderer not found");
        }
    }

    public void EatFish()
    {
        if (parentScript != null)
        {
            parentScript.DespawnFish(this.gameObject);
        } else
        {
            Debug.LogError("Spawned fish doesn't have a parent GameObject");
        }
    }
}
