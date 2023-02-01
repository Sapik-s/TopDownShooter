using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidBody;
    [SerializeField] private FloatingJoystick _movementJoystick;
    [SerializeField] private FloatingJoystick _shootingJoystick;

    private Animator _animator;

    [SerializeField] private float _moveSpeed = 5f;

    static public bool _shooting = false;

    private Vector3 _rotationShooting;
    private Vector3 _movingRotation;
    private void InitValue()
    {
        _rigidBody.freezeRotation = true;
    }

    private void InitCompnent()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void VectorPlayer()
    {
        _rotationShooting = new Vector3(_shootingJoystick.Horizontal * _moveSpeed, 0, _shootingJoystick.Vertical * _moveSpeed);        

        _movingRotation = new Vector3(_movementJoystick.Horizontal * _moveSpeed, 0, _movementJoystick.Vertical * _moveSpeed);
        _rigidBody.velocity = _movingRotation;
    }

    private void InteractionLeftJystick()
    {
        if (_movementJoystick.Horizontal != 0 || _movementJoystick.Vertical != 0 && !_shooting)
        {
            PlayerRun(true);
            RotationPlayer(_movingRotation);

        } else if (!_shooting) { PlayerRun(false); }
    }
    private void InteractionRightJystick()
    {
        if (_shootingJoystick.Horizontal != 0 || _shootingJoystick.Vertical != 0)
        {
            PlayerShooting(true);
            PlayerRun(true);
            RotationPlayer(_rotationShooting);
            _shooting = true;

        } else if (_shooting) {
            _shooting = false;
            PlayerRun(false);
            PlayerShooting(false);
        }
    }

    private void Start()
    {
        InitCompnent();
        InitValue();
    }

    private void FixedUpdate()
    {
        VectorPlayer();

        InteractionLeftJystick();
        InteractionRightJystick();
    }

    private void RotationPlayer(Vector3 vector)
    {
        transform.rotation = Quaternion.LookRotation(vector);
    }

    private void PlayerShooting(bool isShoot)
    {
        _animator.SetBool("PlayerShoot", isShoot);
    }

    private void PlayerRun(bool isRun)
    {
       _animator.SetBool("PlayerRun", isRun);
    }
}