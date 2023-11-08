using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    private PlayerControl PlayerControl;   
    Animator Animator;
    int WeaponShop = 30, Left;
    bool Reload = false, CaneFire = true, Bullets = false;
    GameObject SpawnBullet;
    public float Offset;
    public int BullIndex;
    [Header("Вооружение")]
    [SerializeField] private TextMeshProUGUI TextAmmo;
    [SerializeField] private Image ReloadFillAmount;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform SpawnTransformShot;
    [SerializeField] private Stats Stats;

    private void Awake()
    {
        PlayerControl = new PlayerControl();
        Animator = GetComponent<Animator>();
        PlayerControl.Player.Weapon.performed += context => Fire();
        Ammo();
        if (Offset == 0)
            BullIndex = 1;
        else
            BullIndex = -1;
    }

    private void OnEnable()
    {
        PlayerControl.Enable();
    }
    private void OnDisable()
    {
        PlayerControl.Disable();
    }

    void Update()
    {
        Vector3 MousePosition = Mouse.current.position.ReadValue();
        Vector3 Target = Camera.main.ScreenToWorldPoint(MousePosition) - transform.position;
        float RotZ = Mathf.Atan2(Target.y, Target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, RotZ + Offset);
        if (Reload)
        {
            ReloadFillAmount.fillAmount += Time.deltaTime / 1.2f;
            if (ReloadFillAmount.fillAmount == 1)
            {
                Ammo();
            }
        }
    }

    public void Ammo()
    {
        Left = WeaponShop;
        TextAmmo.text = Left.ToString();
        ReloadAmount(false);
    }
    public void Fire()
    {
        if (Left > 0)
        {
            CaneFire = true;
        }
        else
        {
            CaneFire = false;
        }
        if (Reload == false)
        {
            if (CaneFire)
            {
                Left--;                
                TextAmmo.text = Left.ToString();
                SpawnBullet = Instantiate(Bullet, SpawnTransformShot.transform.position, transform.rotation);
                SpawnBullet.GetComponent<Bullet>().Damage = Stats.Damage;
                SpawnBullet.GetComponent<Bullet>().BulletMask = Stats.LayerMask;
                SpawnBullet.GetComponent<Bullet>().Index = BullIndex;
                if (Left == 0)
                {
                    ReloadAmount(true);
                }
            }
            else
            {
                ReloadAmount(true);
            }
        }
    }
    void ReloadAmount(bool Active)
    {
        ReloadFillAmount.enabled = Active;
        ReloadFillAmount.fillAmount = 0;
        Reload = Active;
        TextAmmo.enabled = !Active;
        if(Active == true)
        {
            Animator.SetTrigger("Reload");
        }
    }
}
