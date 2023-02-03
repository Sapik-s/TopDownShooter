using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using static Cinemachine.AxisState;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float _setMaxHealth = 100f;
    [SerializeField, Range(0, 100)] private float _setHealthRegen;
    [SerializeField] private AnimationClip[] _animation;
    [SerializeField] private GameObject[] _setBloodSpot;

    private float _maxHealth;
    private float _currentHealth;
    private float _healthRegen;
    private float _time = 0;

    private bool _death = false;
    private Animator _animator;

    private NavMeshAgent _agent;
    private CapsuleCollider _collider;

    private void InitValue()
    {
        _maxHealth = _setMaxHealth;
        _currentHealth = _maxHealth;
        _healthRegen = _setHealthRegen;
    }

    private void InitComponent()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        InitValue();
        InitComponent();
    }

    private void Update()
    {
        if (_death) { _time += Time.deltaTime; }

        if (_time > 15) {
            Destroy(gameObject); 
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth < 0 )
        {
            OffComponent();
            transform.GetComponent<AIController>().lookAt = false;
            _animator.Play(RandomAnimation().name);
            _death = true;
            Instantiate(RandomBloodSpot(), transform.position, Quaternion.identity);
            
        }
    }

    private AnimationClip RandomAnimation()
    {
        int randomClip = Random.Range(0, _animation.Length);
        AnimationClip randomAnim = _animation[randomClip];
        return randomAnim;
    }

    private void OffComponent()
    {
        _agent.enabled = false;
        _collider.enabled = false;
    }

    private GameObject RandomBloodSpot()
    {
        GameObject _bloodSpot = _setBloodSpot[Random.Range(0, _setBloodSpot.Length)];
        return _bloodSpot;
    }

}
