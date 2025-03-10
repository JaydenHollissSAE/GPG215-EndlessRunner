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
    GameManager gameManager;
    private GameObject jumpBar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindFirstObjectByType<GameManager>();
        jumpBar = GameObject.FindGameObjectWithTag("JumpBar");
    }

    void Update()
    {
        jumpBar.transform.localScale = new Vector3((jumpMultiplier - 1f) / 2.3f, 1f, 1f);
        //transform.rotation = Quaternion.identit;
        if ((Input.GetKey(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended) && applyMove))
        { 
            touching = true;
            if (jumpMultiplier <= 2.4f)
            {
                jumpMultiplier += Time.deltaTime * (0.002f / 0.0013f);
            }
            
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
        if (CheckVelocity() < 0.8f)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.left, Time.fixedDeltaTime * gameManager.speed);
        }
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

    float CheckVelocity()
    {
        return Mathf.Max(rb.linearVelocity.y, 0) - Mathf.Min(0, rb.linearVelocity.y);
    }


    void Move()
    {
        if (CheckVelocity() < 1f)
        {
            Debug.Log("Velocity.y " + rb.linearVelocity.y.ToString());
            rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
            jumpMultiplier = 1.0f;
            //moveDirection = Vector2.zero;
        }
        applyMove = true;
    }
}