using UnityEngine;

public class Move : MonoBehaviour
{
    public Interface Interface;
    public Weapon Weapon;

    float Speed;
    float JumpForce;
    public bool CheckGround = false;

    Animator Animator;
    PlayerControl PlayerControl;
    bool facing = true;
    Vector2 MoveDirection;

    private void Awake()
    {
        PlayerControl = new PlayerControl();
        Animator = GetComponent<Animator>();
        Speed = GetComponent<Stats>().Speed;
        JumpForce = GetComponent<Stats>().JumpForce;
        PlayerControl.Player.Jump.performed += context => Jump();
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
            Animator.SetBool("Walk", true);
        }
        else if (MoveDirection.x > 0.2)
        {
            Animator.SetBool("Walk", true);
        }
        else
        {
            Animator.SetBool("Walk", false);
        }
        GetComponent<Stats>().Rigidbody.velocity = new Vector2(MoveDirection.x * Speed, GetComponent<Stats>().Rigidbody.velocity.y);
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
        if (facing == false)
        {
            Weapon.Offset = 180;
            Weapon.BullIndex = -1;
        }
        else
        {
            Weapon.Offset = 0;
            Weapon.BullIndex = 1;
        }
    }
}
