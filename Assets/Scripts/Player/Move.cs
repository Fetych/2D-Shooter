using UnityEngine;

public class Move : MonoBehaviour
{
    public Interface Interface;

    float Speed;
    float JumpForce;
    public bool CheckGround = false;

    Animator Animator;
    PlayerControl PlayerControl;
    bool facing = true;
    bool Run;
    int IndexSpeed = 1;
    Vector2 MoveDirection;

    private void Awake()
    {
        PlayerControl = new PlayerControl();
        Animator = GetComponent<Animator>();
        Speed = GetComponent<Stats>().Speed;
        JumpForce = GetComponent<Stats>().JumpForce;
        PlayerControl.Player.Jump.performed += context => Jump();
        PlayerControl.Player.Run.performed += context => ChangeSpeed(true);
        PlayerControl.Player.Run.canceled += context => ChangeSpeed(false);
        PlayerControl.Player.Sit.performed += context => SitDown(true);
        PlayerControl.Player.Sit.canceled += context => SitDown(false);
    }

    private void OnEnable()
    {
        PlayerControl.Enable();
    }
    private void OnDisable()
    {
        PlayerControl.Disable();
    }

    private void Update()
    {
        MoveDirection = PlayerControl.Player.Move.ReadValue<Vector2>();
        HorizontalMove();
    }

    void HorizontalMove()
    {
        if (facing == false && MoveDirection.x > 0)
        {
            Flip();
        }
        else if (facing == true && MoveDirection.x < 0)
        {
            Flip();
        }
        if (MoveDirection.x <= -0.2f)
        {
            if(IndexSpeed == 1)
                Animator.SetBool("Walk", true);
            else
                Animator.SetBool("Run", true);
        }
        else if (MoveDirection.x > 0.2)
        {
            if (IndexSpeed == 1)
                Animator.SetBool("Walk", true);
            else
                Animator.SetBool("Run", true);
        }
        else
        {
             Animator.SetBool("Walk", false);
             Animator.SetBool("Run", false);
        }
        GetComponent<Stats>().Rigidbody.velocity = new Vector2(MoveDirection.x * Speed * IndexSpeed, GetComponent<Stats>().Rigidbody.velocity.y);
    }

    void Jump()
    {
        if (CheckGround)
        {
            GetComponent<Stats>().Rigidbody.velocity = new Vector2(GetComponent<Stats>().Rigidbody.velocity.x, JumpForce);
            Animator.SetTrigger("Jump");
        }
    }

    void Flip()
    {
        facing = !facing;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void ChangeSpeed(bool running)
    {
        if (running)
        {
            IndexSpeed = 2;
        }
        else
        {
            IndexSpeed = 1;
        }
    }
    void SitDown(bool Sit)
    {
        if (Sit)
        {
            Animator.SetBool("Walk", false);
            Animator.SetBool("Run", false);
            Animator.SetBool("Sit", true);
            MoveDirection.x = 0;
        }
        else
        {
            Animator.SetBool("Sit", false);
        }
    }
}
