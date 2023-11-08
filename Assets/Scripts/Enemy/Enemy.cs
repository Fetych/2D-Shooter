using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Distance;
    public WeaponEnemy WeaponEnemy;
    //[SerializeField] LayerMask LayerMask;
    [SerializeField] Transform Ray;
    //[SerializeField] Canvas Canvas;
    public int X;
    bool CaneFire = false;
    public float ReloadTime, EnterTime;

    private void Start()
    {
        EnterTime = ReloadTime;
    }

    private void FixedUpdate()
    {
        Vector2 RayTr = new Vector2(Distance * X, Ray.localPosition.y);
        Vector2 Ray2 = new Vector2( X, Ray.localPosition.y);
        Debug.DrawRay(transform.position, RayTr, Color.black);
        RaycastHit2D Hit = Physics2D.Raycast(transform.position, Ray2, Distance, GetComponent<Stats>().LayerMask);
        if (Hit.collider != null && Hit.collider.GetComponent<Health>() != null)
        {
            CaneFire = true;
        }
        else
        {
            CaneFire = false;
        }
        if (CaneFire)
        {
            if(EnterTime > 0)
            {
                EnterTime -= Time.deltaTime;
            }
            else
            {
                WeaponEnemy.Fire();
                EnterTime = ReloadTime;
            }
        }
    }
}
