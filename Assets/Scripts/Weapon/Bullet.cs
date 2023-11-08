using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float Speed = 5, Distance = 0;
    public int Damage;
    public LayerMask BulletMask;
    public int Index;

    void Update()
    {
        RaycastHit2D Hit = Physics2D.Raycast(transform.position, transform.up, Distance, BulletMask);
        if (Hit.collider != null)
        {
            if (Hit.collider.TryGetComponent(out Health health))
            {
                health.Damage(Damage);
            }
            Destroy(gameObject);
        }
        //transform.Translate(Vector2.right * Speed * Time.deltaTime);
        transform.Translate(Vector2.right * Speed * Time.deltaTime * Index);
    }
}
