using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool applyMove = true;
    private float jumpEndTime = 0.0f;
    public bool noJump = false;
    [SerializeField] private float jumpMultiplier = 1.0f;
    public float jumpMultiplierMax = 2.4f;
    GameManager gameManager;
    private GameObject jumpBar;
    private GameObject sprite;
    public float moveXBase = 0.5f;
    public float moveYBase = 1f;
    private float velocityAt0 = 0f;
    private float unstick = 0f;

    void Start()
    {
        sprite = transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindFirstObjectByType<GameManager>();
        jumpBar = GameObject.FindGameObjectWithTag("JumpBar");
    }

    void Update()
    {
        if (!noJump)
        {
            Update2();
            moveSpeed = 5f + (gameManager.speed -1f)/5f;
            jumpBar.transform.localScale = new Vector3((jumpMultiplier - 1f) / 2.3f, 1f, 1f);
            sprite.transform.localScale = new Vector3(1f, 1f - (jumpMultiplier - 1f) / 2.3f, 1f);
            sprite.transform.localPosition = new Vector3(0f, -1f * ((jumpMultiplier - 1f) / 5f), 0f);
            if ((Input.GetKey(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended) && applyMove))
            { 
                if (jumpMultiplier <= jumpMultiplierMax)
                {
                    jumpMultiplier += Time.deltaTime * (0.002f / 0.0013f);
                }
            
            }
            else if ((Input.GetKeyUp(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) && applyMove)
            {
                jumpEndTime = Time.deltaTime + 0.5f;
                ProcessInputs();
            }
            //Debug.Log(rb.linearVelocity);            
        }
        else
        {
            noJump = false;
        }


    }

    void Update2()
    {
        if (unstick > 0f) 
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position+Vector3.down, Time.deltaTime * gameManager.speed);
            unstick -= Time.deltaTime;
        }

        else if (CheckVelocity() < 0.8f)
        {
            if (IsGrounded())
            {
                applyMove = true;
                transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.left, Time.deltaTime * gameManager.speed);
                //Debug.Log("Moving with stage");
                rb.AddForce(Vector2.down);
            }
            else if (velocityAt0 > 3f && IsStuck())
            {
                Debug.LogError("Player Stuck");
                unstick = 0.3f;
            }
            else
            {
                velocityAt0 += Time.deltaTime;
                Debug.Log("Add velocityAt0");
            }
        }
        else 
        {
            applyMove = false;
            velocityAt0 = 0f;
        }
        //if (Time.time < jumpEndTime && applyMove)
        //{
        //   applyMove = false;
        //    Move();
        //}
        //Debug.Log(rb.linearVelocity);

    }

    void ProcessInputs()
    {
        //float moveX = Input.GetAxisRaw("Horizontal");
        //float moveY = Input.GetAxisRaw("Vertical");
        float moveX = moveXBase+ jumpMultiplier/100f;
        float moveY = moveYBase*jumpMultiplier;
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
        //bool checking = true;
        float offsetBuffer = transform.localScale.x/2;
        float offset = -1* offsetBuffer;
        bool check = false;
        int checkedCount = 0;
        Vector3 position = transform.position;
        //Vector3 newoffset = new Vector3 (0, -0.2f, 0);
        while (checkedCount < 2)
        {
            
            position.x += offset;
            RaycastHit2D hit2D = Physics2D.Raycast(position + Vector3.down, Vector3.down, 0.6f);
            Debug.DrawRay(position + Vector3.down, Vector2.down);
            if (hit2D && hit2D.transform.gameObject.name.Contains("Block"))
            {
                check = true;
                checkedCount = 2;
            }
            else
            {
                offset *= -2;
            }
            checkedCount++;

        }
        if (check)
        {
            //Debug.Log("Block Hit by Ray");
        }

        //Debug.Log(hit2D.transform.gameObject.name);
        //Debug.Log(check);
        return check;
    }
    bool IsStuck()
    {
        bool check = false;

        RaycastHit2D hit2D = Physics2D.Raycast(transform.position + Vector3.right, Vector3.right, 0.6f);
        Debug.DrawRay(transform.position + Vector3.right, Vector2.right);
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