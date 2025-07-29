using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFish : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    private Rigidbody2D rb;

    [SerializeField]
    private float m_speed = 1000.0f;
    private float m_direction = -1;
    
    private float m_scale = 0.4f;
    private float startingScale = 0.4f;
    private float winningScale = 5.0f;

    [SerializeField]
    private float scoreMultiplier = 0.1f;

    private InputAction moveAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        m_scale = startingScale;
        transform.localScale = new Vector2(m_scale, m_scale);

        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // Player movement
        if (moveAction != null)
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            if (rb != null)
            {
                rb.AddForce(m_speed * Time.deltaTime * moveValue);
            }
            else
            {
                Debug.Log("player rigidbody not found");
            }

            // Flip the sprite based on the direction
            if (moveValue.x > 0 && m_direction == -1)
            {
                m_direction = 1;
                transform.localScale = new Vector2(transform.localScale.x * -m_direction, transform.localScale.y);
            }
            else if (moveValue.x < 0 && m_direction == 1)
            {
                m_direction = -1;
                transform.localScale = new Vector2(transform.localScale.x * m_direction, transform.localScale.y);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var layerMask = collision.gameObject.layer;
        if (layerMask == LayerMask.NameToLayer("TransparentFX"))
        {
            // We collided with a fish
            float otherScale = collision.gameObject.transform.localScale.x;
            if (m_scale > otherScale)
            {
                // Eat the smaller fish
                m_scale += otherScale * scoreMultiplier;
                transform.localScale = new Vector2((-m_direction) * m_scale, m_scale);

                if (collision.gameObject.TryGetComponent<SpawnedFish>(out var spawnedFish))
                {
                    spawnedFish.EatFish();
                    gameController.AddScore(spawnedFish.scaleScore);
                    gameController.AddFishEaten(1);
                }

                if (m_scale >= winningScale)
                {
                    gameController.Win();
                }
            }
            else
            {
                gameController.Lose();
            }
        }
    }
}
