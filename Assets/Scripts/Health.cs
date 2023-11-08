using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    Animator Animator;
    [Header("Çהמנמגüו")]
    public Canvas HealthCanvas;
    [SerializeField] private TextMeshProUGUI TextHP;
    [SerializeField] private Image HPFillAmount;
    [SerializeField] private float MaxHP, CurrentHP;

    void Awake()        
    {
        Animator = GetComponent<Animator>();
        MaxHP = GetComponent<Stats>().MaxHealth;
        CurrentHP = MaxHP;
        Damage(0);
    }

    public void Damage(int Damage)
    {
        if (CurrentHP > 0)
        {
            CurrentHP -= Damage;
            HPFillAmount.fillAmount = CurrentHP / MaxHP;
            TextHP.text = $"{CurrentHP}/{MaxHP}";
            if(CurrentHP - Damage <= 0)
            {
                Animator.SetBool("Death", true);
            }
        }
        else
        {
            Animator.SetBool("Death", true);
        }
    }

    public void Death(int Index)
    {
        if (Index == 0)
        {
            GetComponent<Stats>().Collider.enabled = false;
            GetComponent<Stats>().Rigidbody.simulated = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
