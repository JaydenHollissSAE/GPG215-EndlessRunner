using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool applyMove = true;
    private float jumpEndTime = 0.0f;
    [SerializeField] private float jumpMultiplier = 1.0f;
    GameManager gameManager;
    private GameObject jumpBar;
    private GameObject sprite;

    void Start()
    {
        sprite = transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindFirstObjectByType<GameManager>();
        jumpBar = GameObject.FindGameObjectWithTag("JumpBar");
    }

    void Update()
    {
        moveSpeed = 5f + (gameManager.speed -1f)/5f;
        jumpBar.transform.localScale = new Vector3((jumpMultiplier - 1f) / 2.3f, 1f, 1f);
        sprite.transform.localScale = new Vector3(1f, 1f - (jumpMultiplier - 1f) / 2.3f, 1f);
        sprite.transform.localPosition = new Vector3(0f, -1f * ((jumpMultiplier - 1f) / 5f), 0f);
        if ((Input.GetKey(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended) && applyMove))
        { 
            if (jumpMultiplier <= 2.4f)
            {
                jumpMultiplier += Time.deltaTime * (0.002f / 0.0013f);
            }
            
        }
        else if ((Input.GetKeyUp(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) && applyMove)
        {
            jumpEndTime = Time.deltaTime + 0.5f;
            ProcessInputs();
        }

    }

    void FixedUpdate()
    {
        if (CheckVelocity() < 0.8f && IsGrounded())
        {
            applyMove = true;
            transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.left, Time.fixedDeltaTime * gameManager.speed);
        }
        else 
        {
            applyMove = false;
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
        float moveX = 0.5f + jumpMultiplier/100f;
        float moveY = 1*jumpMultiplier;
        //Debug.Log(moveY);
        moveDirection = new Vector2(moveX, moveY);
        Move();
    }

    float CheckVelocity()
    {
        return Mathf.Max(rb.linearVelocity.y, 0) - Mathf.Min(0, rb.linearVelocity.y);
    }

    bool IsGrounded()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position+ Vector3.down, Vector2.down, 1.00f);
        bool check = false;
        if (hit2D && hit2D.transform.gameObject.name.Contains("Block"))
        {
            check = true;
        }
        //Debug.Log(hit2D.transform.gameObject.name);
        //Debug.Log(check);
        return check;
    }

    void Move()
    {
        if (CheckVelocity() < 0.9f)
        {
            //Debug.Log("Velocity.y " + rb.linearVelocity.y.ToString());
            rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * 5f);
            jumpMultiplier = 1.0f;
            //moveDirection = Vector2.zero;
        }
        applyMove = true;
    }
}