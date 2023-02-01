using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [Header("Gun Settings")]
    [SerializeField] private float _fireRate = 0.1f;
    [SerializeField] private int _clipSize = 30;
    [SerializeField] private int _reservedAmmoCapacity = 60;

    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _pointSpawnBullet;

    private bool _canShoot;
    private int _currentAmmoInClip;
    private int _ammoInReverse;

    static public bool isShoot = false;


    private void InitValue()
    {
        _ammoInReverse = _reservedAmmoCapacity;
        _currentAmmoInClip = _clipSize;
        _canShoot = true;
    }

    private void Start()
    {
        InitValue();
    }

    private void Update()
    {
        Shooting();
    }

    private void Shooting()
    {
        if (PlayerController._shooting && _currentAmmoInClip > 0 && _canShoot)
        {
            _canShoot = false;
            _currentAmmoInClip--;
            StartCoroutine(ShootGun());

        }
        else if (_currentAmmoInClip == 0 && _ammoInReverse != 0)
        {
            int amountNeeded = _clipSize - _currentAmmoInClip;

            if (amountNeeded >= _ammoInReverse)
            {
                _currentAmmoInClip += _ammoInReverse;
                _ammoInReverse -= amountNeeded;

            }
            else
            {
                _currentAmmoInClip = _clipSize;
                _ammoInReverse -= amountNeeded;
            }
            if (_ammoInReverse < 0)
            {
                _ammoInReverse = 0;
            }
        }
    }

    IEnumerator ShootGun()
    {
        SpawnBullet();
        StartCoroutine(MuzzleFlash());

        yield return new WaitForSeconds(_fireRate);
        _canShoot = true;
    }

    IEnumerator MuzzleFlash()
    {
        _muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        _muzzleFlash.SetActive(false);
    }

    private void SpawnBullet() {
        GameObject newBullet = Instantiate(_bullet, _pointSpawnBullet);
        newBullet.transform.parent = null;
    }

}
