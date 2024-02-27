using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] Health Health;
    [SerializeField] private WeaponEnemy WeaponEnemy;
    [SerializeField] List<Vector2> Offset, Size;

    public float TimeStay, TimeToRevert;
    public float currentState;
    public bool Check;
    public Animator Animator;
    public bool Target;

    private const float IDLE_STATE = 0;
    private const float WALK_STATE = 1;
    private const float REVERT_STATE = 2;
    private const float ATTACK_STATE = 3;
    private const float PURSUE_STATE = 4;

    float Speed;
    public List<GameObject> Opponent;

    private void Start()
    {
        Animator = GetComponent<Animator>();
        Health = GetComponent<Health>();
        currentState =1;
        TimeStay = 0;
        Speed = GetComponent<Stats>().Speed;
        if (Offset.Count != 0)
        {
            GetComponent<CapsuleCollider2D>().offset = Offset[0];
            GetComponent<CapsuleCollider2D>().size = Size[0];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent (out DirectionTrigger directionTrigger) && directionTrigger.EnemyTrigger == EnemyTrigger.DirectionTrigger)
        {
            Check = true;
            currentState = 0;
        }
        if(collision.GetComponent<Stats>() != null && collision.GetComponent<Stats>().LivingOrganisms == LivingOrganisms.Player)
        {
            Opponent.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Opponent.Count != 0 && collision.GetComponent<Stats>() != null && collision.GetComponent<Stats>().LivingOrganisms == LivingOrganisms.Player)
        {
            Animator.SetBool("Attack", false);
            Opponent.Remove(collision.gameObject);
        }
    }

    private void Update()
    {
        if (TimeStay >= TimeToRevert)
        {
            TimeStay = 0;
            currentState = 2;
        }
        if (GetComponent<Health>().Dead)
        {
            currentState = 5;
        }
        switch (currentState)
        {
            case 0:
                Target = false;
                TimeStay += Time.deltaTime;
                Animator.SetBool("Walk", false);
                Animator.SetBool("Run", false);
                if (Offset.Count != 0)
                {
                    GetComponent<CapsuleCollider2D>().offset = Offset[0];
                    GetComponent<CapsuleCollider2D>().size = Size[0];
                }
                break;
            case 1:
                Animator.SetBool("Walk", true);
                Animator.SetBool("Run", false);
                Animator.SetBool("Attack", false);
                if (Offset.Count != 0)
                {
                    GetComponent<CapsuleCollider2D>().offset = Offset[0];
                    GetComponent<CapsuleCollider2D>().size = Size[0];
                }
                GetComponent<Stats>().Rigidbody.velocity = new Vector2(Speed, GetComponent<Stats>().Rigidbody.velocity.y);
                break;
            case 2:
                Flip();
                currentState = 1;
                Check = false;
                break;
            case 3:                             
                Animator.SetBool("Attack", true);
                Animator.SetBool("Walk", false);
                Animator.SetBool("Run", false);
                if (Offset.Count != 0)
                {
                    GetComponent<CapsuleCollider2D>().offset = Offset[1];
                    GetComponent<CapsuleCollider2D>().size = Size[1];
                }
                break;
            case 4:
                Animator.SetBool("Run", true);
                Animator.SetBool("Attack", false);
                Animator.SetBool("Walk", false);
                GetComponent<Stats>().Rigidbody.velocity = new Vector2(Speed * 1.5f, GetComponent<Stats>().Rigidbody.velocity.y);
                if (Offset.Count != 0)
                {
                    GetComponent<CapsuleCollider2D>().offset = Offset[1];
                    GetComponent<CapsuleCollider2D>().size = Size[1];
                }

                break;
        }
    }

    void Flip()
    {
        Speed *= -1;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
        Vector3 canvasScaler = Health.HealthCanvas.transform.localScale;
        canvasScaler.x *= -1;
        Health.HealthCanvas.transform.localScale = canvasScaler;
    }
}
