using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Weapons", menuName = "GBJam/New Weapon", order = 0)]
public class Weapons : ScriptableObject 
{
    [SerializeField]
    private Projectile projectile = null;
    [SerializeField] 
    private int _rateOfFire = 600;
    [SerializeField] 
    private float _damage;
    [SerializeField]
    private float _bulletSpeed = 10;
    [SerializeField]
    private bool _isAmmoInfinite = false;
    [SerializeField]
    private int _ammoCount = 60;
    [SerializeField]
    private bool _isDamageImpactsOnArea = false;
    [SerializeField]
    private float _damageArea = 1f;
    [SerializeField]
    private bool _isLaunchingSeveralPellets = false;
    [SerializeField]
    private int _pelletsCount = 5;
    [SerializeField]
    private float _spreadAngle;

    [SerializeField] 
    private EffectSoundPlayer _shootSounds;
    [SerializeField]
    private EffectSoundPlayer _impactSounds;
    [SerializeField]
    private GameObject _impactEffect = null;

    const string weaponName = "Weapon";

    public void Equip(Transform rightHand, Transform leftHand)
    {
        DestroyOldWeapon(rightHand, leftHand);
    }

    private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
    {
        Transform oldWeapon = rightHand.Find("Weapon");
        if (oldWeapon == null)
        {
            oldWeapon = leftHand.Find("Weapon");
        }
        if (oldWeapon == null) return;

        oldWeapon.name = "DESTROYING";
        Destroy(oldWeapon.gameObject);
    }

    public bool HasProjectile()
    {
        return projectile != null;
    }

    public EffectSoundPlayer GetSoundPlayer()
    {
        return _shootSounds;
    }

    public void LaunchProjectile(Transform launchPosition, Quaternion rotation, bool isPlayer, Side side = Side.Default)
    {
        if (_isLaunchingSeveralPellets)
        {
            var spreadAngle = -_spreadAngle / 2;
            var step = (_spreadAngle * 2) / _pelletsCount;   
            for (int i = 0; i < _pelletsCount; i++)
            {
                Transform transform = launchPosition;
                Vector3 bulletDirection = Quaternion.Euler(0, spreadAngle, 0) * transform.forward;

                Projectile p = Instantiate(projectile, 
                    transform.position, rotation);
                p.transform.forward = bulletDirection;

                InitializeBullet(p, isPlayer, side);
                spreadAngle += step;
            }
        }
        else {
            Projectile projectileInstance = Instantiate(projectile,
            launchPosition.position, rotation);
            InitializeBullet(projectileInstance, isPlayer, side);
        }
    }

    public float GetDamage()
    {
        return _damage;
    }

    public int GetRateOfFire()
    {
        return _rateOfFire;
    }

    public int GetAmmoCount()
    {
        if (_isAmmoInfinite) return 99999;
        return _ammoCount;
    }

    public float GetBulletSpeed()
    {
        return _bulletSpeed;
    }

    private void InitializeBullet(Projectile projectileInstance, bool isPlayer, Side side = Side.Default)
    {
        projectileInstance.SetDamage(_damage);
        projectileInstance.SetSpeed(_bulletSpeed);
        projectileInstance.SetImpactEffect(_impactEffect);
        projectileInstance.SetImpactSounds(_impactSounds);
        projectileInstance.SetSide(side);
        projectileInstance.SetPlayer(isPlayer);
        if (_isDamageImpactsOnArea)
        {
            projectileInstance.SetAreaDamage(_damageArea);
        }
    }
}