using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("������")]
    public Collider2D Collider;
    public Rigidbody2D Rigidbody;
    [Header("��������")]
    public float MaxHealth;
    [Header("������������")]
    public float Speed;
    public float JumpForce;
    [Header("����")]
    public int Damage;
    public LayerMask LayerMask;

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }
}
