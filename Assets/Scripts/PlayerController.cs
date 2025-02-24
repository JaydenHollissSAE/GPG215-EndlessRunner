using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool applyMove = true;
    private float jumpEndTime = 0.0f;
    [SerializeField] private float jumpMultiplier = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && applyMove)
        {
            jumpMultiplier += 0.00002f;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && applyMove)
        {
            jumpEndTime = Time.deltaTime + 0.5f;
            ProcessInputs();
        }

    }

    void FixedUpdate()
    {
        //if (Time.time < jumpEndTime && applyMove)
        //{
         //   applyMove = false;
        //    Move();
        //}

    }

    void ProcessInputs()
    {
        //float moveX = Input.GetAxisRaw("Horizontal");
        //float moveY = Input.GetAxisRaw("Vertical");
        float moveX = 0.5f;
        float moveY = 1*jumpMultiplier;
        //Debug.Log(moveY);
        moveDirection = new Vector2(moveX, moveY);
        Move();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        jumpMultiplier = 1.0f;
        applyMove = true;
        //moveDirection = Vector2.zero;
    }
}