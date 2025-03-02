using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool applyMove = true;
    private float jumpEndTime = 0.0f;
    [SerializeField] private float jumpMultiplier = 1.0f;
    private bool touching = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if ((Input.GetKey(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended) && applyMove))
        { 
            touching = true;
            jumpMultiplier += 0.0002f;
        }
        else if ((Input.GetKeyUp(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) && applyMove)
        {
            touching = false;
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
        if (Mathf.Max(rb.linearVelocity.y, 0) - Mathf.Min(0, rb.linearVelocity.y) < 1f)
        {
            Debug.Log("Velocity.y " + rb.linearVelocity.y.ToString());
            rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
            jumpMultiplier = 1.0f;
            //moveDirection = Vector2.zero;
        }
        applyMove = true;
    }
}