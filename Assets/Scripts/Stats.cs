using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Физика")]
    public Collider2D Collider;
    public Rigidbody2D Rigidbody;
    [Header("Здоровье")]
    public float MaxHealth;
    [Header("Передвижение")]
    public float Speed;
    public float JumpForce;
    [Header("Урон")]
    public int Damage;
    public LayerMask LayerMask;

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }
}
