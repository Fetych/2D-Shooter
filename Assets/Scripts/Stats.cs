using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, ILivingOrganisms
{
    [SerializeField] LivingOrganisms LivingOrganism;
    public LivingOrganisms LivingOrganisms { get => LivingOrganism; }
    [Header("Физика")]
    public Collider2D Collider;
    public Rigidbody2D Rigidbody;
    [Header("Здоровье")]
    public float MaxHealth;
    public int Armor;
    [Header("Передвижение")]
    public float NormalSpeed;
    public float Speed;
    public float SlowSpeed;
    public float JumpForce;
    [Header("Урон")]
    public int Damage;
    public LayerMask LayerMask;

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Speed = NormalSpeed;
        SlowSpeed = NormalSpeed * 0.1f;
    }
}
