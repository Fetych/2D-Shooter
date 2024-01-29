using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTrap : MonoBehaviour
{
    [SerializeField] List<Health> Health;
    [SerializeField] float DamageTimer;
    public Animator Animator;

    private void Start()
    {
        Animator = GetComponent<Animator>();
        StartCoroutine(CheckHealth());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Health>() != null)
        {
            Health.Add(collision.GetComponent<Health>());
            collision.GetComponent<Stats>().Speed = collision.GetComponent<Stats>().SlowSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Health>() != null)
        {
            Health.Remove(collision.GetComponent<Health>());
            collision.GetComponent<Stats>().Speed = collision.GetComponent<Stats>().NormalSpeed;
        }
    }

    private IEnumerator CheckHealth()
    {
        yield return new WaitForSeconds(DamageTimer);
        Damage();
    }

    private void Damage()
    {
        if (Health.Count > 0)
        {
            for (int i = 0; i < Health.Count; i++)
            {
                Health[i].Damage(1);
            }
        }
        StartCoroutine(CheckHealth());
    }

    public void OffTrap()
    {
        Animator.SetBool("On", false);
        Destroy(this);
    }
}
