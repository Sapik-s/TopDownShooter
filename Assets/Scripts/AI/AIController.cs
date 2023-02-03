using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

[RequireComponent(typeof(NavMeshAgent), typeof(CapsuleCollider))]
public class AIController : MonoBehaviour
{
    private Transform _player;
    public bool lookAt = false;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (lookAt)
        {
            transform.LookAt(_player);
        }
    }
}
