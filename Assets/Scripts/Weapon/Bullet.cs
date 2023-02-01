using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    private Vector3 _startPosition;
    private RaycastHit _hit;

    private float _timeLife = 3f;
    private float _time = 0f;

    private void InitValue()
    {
        _startPosition = transform.position;
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
            Destroy(gameObject);
        }

        _startPosition = transform.position;
    }

    private void TimeLife()
    {
        _time += Time.deltaTime;
        if (_time > _timeLife) { Destroy(gameObject); }
    }
}
