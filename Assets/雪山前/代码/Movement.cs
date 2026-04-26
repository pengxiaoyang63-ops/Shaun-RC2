using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    ParticleSystem PS;
    Rigidbody2D RD2;
    public float speed;
    public float jump;
    public float Walljump;
    public bool onground;
    public bool onwallL;
    public bool onwallR;
    public bool WallJumpBool;
    public int WallJumpCounter;
    public float maxspeed;
    public float Dashcounter;
    public float DashWait;
    public float Timer;
    public bool Dashing;
    public bool FaceRight;
    public int faceCoefficient;
    public int DoubleJump = 0;
    [SerializeField] private InputActionAsset inputActions;
    private InputActionAsset realInputActions;
    private InputAction moveLeft;
    private InputAction moveRight;
    private InputAction jumpAction;
    private InputAction dashAction;
    private InputAction resetAction;
    private bool jumpPressedThisFrame;
    private bool resetPressedThisFrame;
    private bool moveLeftReleasedThisFrame;
    private bool moveRightReleasedThisFrame;
    void Awake()
    {
        realInputActions = inputActions != null ? inputActions : Resources.Load<InputActionAsset>("GameControls");
        var playerMap = realInputActions.FindActionMap("Player");
        moveLeft = playerMap.FindAction("MoveLeft");
        moveRight = playerMap.FindAction("MoveRight");
        jumpAction = playerMap.FindAction("Jump");
        dashAction = playerMap.FindAction("Dash");
        resetAction = playerMap.FindAction("Reset");
    }
    void OnEnable() { realInputActions.Enable(); }
    void OnDisable() { realInputActions.Disable(); }
    void Update()
    {
        jumpPressedThisFrame = jumpAction.WasPressedThisFrame();
        resetPressedThisFrame = resetAction.WasPressedThisFrame();
        moveLeftReleasedThisFrame = moveLeft.WasReleasedThisFrame();
        moveRightReleasedThisFrame = moveRight.WasReleasedThisFrame();
    }
    void Start()
    {
        Time.fixedDeltaTime = 1/500f;
        // Get the Rigidbody2D first before modifying it
        RD2 = GetComponent<Rigidbody2D>();
        if (RD2 != null)
        {
            RD2.bodyType = RigidbodyType2D.Dynamic;
            RD2.isKinematic = false;
        }
        speed = 0.5f;
        jump = 22f;
        Walljump = jump*0.4f;
        onground = false;
        maxspeed = 9.5f;
        RD2.gravityScale = 5f;
    }
    void FixedUpdate()
    {
        Timer += Time.deltaTime;
        DashWait += Time.deltaTime;
        DoubleJumpreset();
        if (Dashing == false)
        {
            locomotion();
            faceupdate();
            Jumping();
            //Wallmotion();
        }
        Reset();
        //Dash();
        ResetY();
    }
    void faceupdate()
    {
        if (FaceRight == true)
        {
            faceCoefficient = 1;
        }
        else
        {
            faceCoefficient = -1;
        }
    }
    void DoubleJumpreset()
    {
        if (onground == true || onwallL == true || onwallR == true)
        {
            Invoke("ResetDoubleJump",0.03f);
            //DoubleJump = 0;
        }
    }
    void ResetDoubleJump()
    {
        //DoubleJump = 0;
    }
    void locomotion()
    {
        if (moveLeft.IsPressed())
        {  
            FaceRight = false;
            if (RD2.velocity.x > -maxspeed)
            {
                RD2.velocity = new Vector2(RD2.velocity.x-1f, RD2.velocity.y);
            }
            else
            {
                RD2.velocity = new Vector2(-maxspeed, RD2.velocity.y);
            }
        }
        else if (moveRight.IsPressed())
        {
            FaceRight = true;
            if (RD2.velocity.x < maxspeed)
            {
                RD2.velocity = new Vector2(RD2.velocity.x+1f, RD2.velocity.y);
            }
            else
            {
                RD2.velocity = new Vector2(maxspeed, RD2.velocity.y);
            }
        }
        else if (moveLeftReleasedThisFrame || moveRightReleasedThisFrame)
        {
            RD2.velocity = new Vector2(0, RD2.velocity.y);
        }
        else if (!moveLeftReleasedThisFrame && !moveRightReleasedThisFrame)
        {
            if (Dashing == false)
            {
                RD2.velocity = new Vector2(0, RD2.velocity.y);   
            }
        }
    }
    void Wallmotion()
    {
        if (onwallR == true && onground == false)
        {
            if (moveRight.IsPressed() && jumpPressedThisFrame)
            {
                WallJumpBool = !WallJumpBool;
                WallJumpCounter = 40;
                RD2.velocity = new Vector2(-WallJumpCounter,Walljump);
            }
        }
        if (onwallL == true && onground == false)
        {
            if (moveLeft.IsPressed() && jumpPressedThisFrame)
            {
                WallJumpBool = !WallJumpBool;
                WallJumpCounter = 40;
                RD2.velocity = new Vector2(WallJumpCounter,Walljump);
            }
        }
        if (WallJumpBool == true&&WallJumpCounter>=-9&&onwallL == false&&onwallR == false)
        {
            if (WallJumpBool&&moveLeft.IsPressed() && jumpAction.IsPressed())
            {
                RD2.velocity = new Vector2(WallJumpCounter,Walljump);
                WallJumpCounter-=2;
            }
            else if (WallJumpBool&&moveRight.IsPressed() && jumpAction.IsPressed())
            {
                RD2.velocity = new Vector2(-WallJumpCounter,Walljump);
                WallJumpCounter-=2;
            }
        }
        else if (WallJumpCounter < 2)
        {
            WallJumpBool = false;
        }
    }
    void Jumping()
    {
        if (jumpPressedThisFrame && onground == true)
        {
            if (Dashing == false)
            {
                RD2.velocity = new Vector2(RD2.velocity.x, jump);
            }
        }
        else if (jumpPressedThisFrame && onground == false && onwallL == false && onwallR == false)
        {
            if (Dashing == false && DoubleJump == 0)
            {
                RD2.velocity = new Vector2(RD2.velocity.x, jump);
                DoubleJump = 1;
            }
        }
    }
    void ResetY()
    {
        if (onground==false&&!jumpAction.IsPressed())
        {
            if (RD2.velocity.y > 0)
            {
                RD2.velocity = new Vector2(RD2.velocity.x,RD2.velocity.y/5);   
            }
        }
    }
    void Dash()
    {
        if (dashAction.IsPressed()&&DashWait>0.5f)
        {
            RD2.velocity = new Vector2(0,0);
            Dashing = true;
            DashWait = 0f;
            RD2.velocity = new Vector2(RD2.velocity.x,0);
            RD2.gravityScale = 0;
            Dashcounter = 0f;
        }
        if(Dashing == true)
        {
            RD2.velocity = new Vector2(35*faceCoefficient,0);
            Dashcounter += Time.deltaTime;
            if (Dashcounter >= 0.2f)
            {
                DashWait = 0f;
                Dashing = false;
                RD2.gravityScale = 5f;
            }
        }
    }
    void Reset()
    {
        if (resetPressedThisFrame)
        {
            RD2.bodyType = RigidbodyType2D.Static;
            RD2.position = new Vector2(0, 0);
            RD2.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    public void GroundFix()
    {
        RD2.velocity = new Vector2(RD2.velocity.x, 1);
    }
}