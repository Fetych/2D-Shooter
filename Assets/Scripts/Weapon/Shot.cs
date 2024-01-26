using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Shot : MonoBehaviour
{
    private PlayerControl PlayerControl;
    Animator Animator;
    [SerializeField] AnimationClip Recharg;
    public int WeaponShop, Left;
    bool Reload = false, CaneFire = true, Bullets = false;
    GameObject SpawnBullet;
    public float Offset, RechargeTimer;
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
        RechargeTimer = Recharg.length;
        PlayerControl.Player.Weapon.performed += context => Shots(true);
        PlayerControl.Player.Weapon.canceled += context => Shots(false);
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
        if (Reload)
        {
            ReloadFillAmount.fillAmount += Time.deltaTime / RechargeTimer;
            if (ReloadFillAmount.fillAmount == 1)
            {
                Ammo();
                CaneFire = true;
                Animator.SetBool("Recharge", false);
            }
        }
    }
    void Shots(bool Fire)
    {
        if (Fire && CaneFire && Left > 0)
        {
            Animator.SetBool("Fire", true); 
        }
        else
        {
            Animator.SetBool("Fire", false);
        }
    }
    public void Ammo()
    {
        Left = WeaponShop;
        TextAmmo.text = Left.ToString();
        ReloadAmount(false);
    }
    public void AmmoText()
    {
        if(Left > 0)
            Left--;        
        if (Left == 0)
        {
            Animator.SetBool("Fire", false);
            CaneFire = false;
            Reload = true;
        }
    }
    public void Fire()
    {
        TextAmmo.text = Left.ToString();
        SpawnBullet = Instantiate(Bullet, SpawnTransformShot.transform.position, transform.rotation);
        SpawnBullet.GetComponent<Bullet>().Damage = Stats.Damage;
        SpawnBullet.GetComponent<Bullet>().BulletMask = Stats.LayerMask;
        SpawnBullet.GetComponent<Bullet>().Index = BullIndex;        
        if (Left == 0)
        {
            Animator.SetBool("Recharge", true);
            Reload = true;
        }
    }
    void ReloadAmount(bool Active)
    {
        ReloadFillAmount.fillAmount = 0;
        Reload = Active;
        TextAmmo.enabled = !Active;
    }
}
