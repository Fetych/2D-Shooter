using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] Health Health;
    [SerializeField] private WeaponEnemy WeaponEnemy;

    public float TimeStay, TimeToRevert;
    public float currentState;
    public bool Check;
    public Animator Animator;

    private const float IDLE_STATE = 0;
    private const float WALK_STATE = 1;
    private const float REVERT_STATE = 2;
    private const float ATTACK_STATE = 3;

    float Speed;

    private void Start()
    {
        Health = GetComponent<Health>();
        currentState = WALK_STATE;
        TimeStay = 0;
        Speed = GetComponent<Stats>().Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent (out DirectionTrigger directionTrigger) && directionTrigger.EnemyTrigger == EnemyTrigger.DirectionTrigger)
        {
            Check = true;
            currentState = IDLE_STATE;
        }
    }

    private void Update()
    {
        //if (GetComponent<EnemyDamage>().TriggerAttack == false)
        //{
        //    if (TimeStay >= TimeToRevert)
        //    {
        //        TimeStay = 0;
        //        currentState = REVERT_STATE;
        //    }
        //}
        if (TimeStay >= TimeToRevert)
        {
            TimeStay = 0;
            currentState = REVERT_STATE;
        }
        //if (Check == false)
        //{
        //    currentState = ATTACK_STATE;
        //}

        switch (currentState)
        {
            case IDLE_STATE:
                // gameObject.GetComponent<EnemyDamage>().anim.SetBool("Walk", false);
                TimeStay += Time.deltaTime;
                Animator.SetBool("Walk", false);
                break;
            case WALK_STATE:
                //gameObject.GetComponent<EnemyDamage>().anim.SetBool("Walk", true);
                Animator.SetBool("Walk", true);
                //EnemyRb.velocity = new Vector2(Speed, EnemyRb.velocity.y);
                GetComponent<Stats>().Rigidbody.velocity = new Vector2(Speed, GetComponent<Stats>().Rigidbody.velocity.y);
                break;
            case REVERT_STATE:
                Flip();
                currentState = WALK_STATE;
                Check = false;
                break;
            case ATTACK_STATE:
                //gameObject.GetComponent<EnemyDamage>().anim.SetBool("Walk", false);
                currentState = WALK_STATE;
                Animator.SetBool("Walk", false);
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
        GetComponent<Enemy>().X *= -1;
        if(transform.localScale.x > 0)
            WeaponEnemy.BullIndex = 1;
        else
            WeaponEnemy.BullIndex = -1;
    }
}
