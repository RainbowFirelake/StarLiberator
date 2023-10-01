using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shooter : MonoBehaviour
{
    public event Action<int> OnAmmoCountUpdate;

    [SerializeField] private Transform gun;
    [SerializeField] private Weapons currentWeapon = null;
    [SerializeField] private Weapons defaultWeapon = null;
    [SerializeField] Transform rightHandTransform = null;
    [SerializeField] Transform leftHandTransform = null;
    [SerializeField] private AudioSource _source;
    [SerializeField] private SideManager _sideManager;
    [SerializeField]
    private bool _isPlayer = false;

    private float timeAfterLastShoot = Mathf.Infinity;
    private float timeOnOneShot;
    private int _currentAmmoCount;

    private void Start() 
    {
        SetupDefaultWeapon();
    }

    private void Update() 
    {
        timeAfterLastShoot += Time.deltaTime;
    }

    public void Shoot()
    {
        if (timeAfterLastShoot > timeOnOneShot)
        {
            currentWeapon.LaunchProjectile(gun, 
                gun.rotation, _isPlayer, _sideManager.GetSide());
            if (_source != null && _isPlayer)
            {
                _source.Play();
            }
            else if (!_isPlayer)
            {
                _source.Play();
            }
            timeAfterLastShoot = 0;
            OnAmmoCountUpdate?.Invoke(_currentAmmoCount);
        }
    }

    public void EquipWeapon(Weapons weapon)
    {
        currentWeapon = weapon;
        AttachWeapon(weapon);
    }

    public float GetBulletSpeed()
    {
        return currentWeapon.GetBulletSpeed();
    }

    private void AttachWeapon(Weapons weapon)
    {
        InitializeRateOfFire();
        _source.clip = currentWeapon.GetSoundPlayer().GetRandomSound();
        _currentAmmoCount = weapon.GetAmmoCount();
        OnAmmoCountUpdate?.Invoke(_currentAmmoCount);
        //currentWeapon.Equip(rightHandTransform, leftHandTransform);
    }

    private Weapons SetupDefaultWeapon()
    {
        AttachWeapon(defaultWeapon);
        return defaultWeapon;
    }

    private void InitializeRateOfFire()
    {
        float x = (float)currentWeapon.GetRateOfFire() / 60;
        timeOnOneShot = 1 / x;
        timeAfterLastShoot = Mathf.Infinity;
    }

    public void SetAimDirection(Vector3 point)
    {
        gun.LookAt(point);
    }
}
