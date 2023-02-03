using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _setDamage = 30f;

    private Vector3 _startPosition;
    private RaycastHit _hit;

    private float _timeLife = 3f;
    private float _time = 0f;

    private float _damage;


    private void InitValue()
    {
        _startPosition = transform.position;
        _damage = _setDamage;
    }

    private void Start()
    {
        InitValue();
    }

    void Update()
    {
        TimeLife();
        CheckHit();
    }

    private void CheckHit()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.up.normalized);

        if (Physics.Linecast(_startPosition, transform.position, out _hit))
        {
            Damage();
            Destroy(gameObject);
        }

        _startPosition = transform.position;
    }

    private void TimeLife()
    {
        _time += Time.deltaTime;
        if (_time > _timeLife) { Destroy(gameObject); }
    }

    private void Damage()
    {
        if (_hit.collider.gameObject.tag == "Enemy") {
            _hit.collider.GetComponent<HealthSystem>().TakeDamage(_damage);
        }
    }
}
