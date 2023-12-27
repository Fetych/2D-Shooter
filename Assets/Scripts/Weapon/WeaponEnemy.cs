using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEnemy : MonoBehaviour
{
    public int BullIndex;
    [Header("Вооружение")]
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform SpawnTransformShot;
    [SerializeField] private Stats Stats;
    private GameObject SpawnBullet;
    private Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void Fire()
    {
        Animator.SetTrigger("Fire");
        SpawnBullet = Instantiate(Bullet, SpawnTransformShot.transform.position, transform.rotation);
        SpawnBullet.GetComponent<Bullet>().Damage = Stats.Damage;
        SpawnBullet.GetComponent<Bullet>().BulletMask = Stats.LayerMask;
        SpawnBullet.GetComponent<Bullet>().Index = BullIndex;
    }
}
