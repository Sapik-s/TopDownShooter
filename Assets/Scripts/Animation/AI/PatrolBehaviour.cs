using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : StateMachineBehaviour
{
    private float _timer;
    private List<Transform> _points = new List<Transform>();
    private NavMeshAgent _agent;

    private Transform _player;
    private float _chaseRange = 10;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer = 0;

        Transform pointsObject = GameObject.FindGameObjectWithTag("Points").transform;
        foreach (Transform item in pointsObject)
        {
            _points.Add(item);
        }

        _agent = animator.GetComponent<NavMeshAgent>();
        _agent.SetDestination(_points[Random.Range(0, _points.Count)].position);

        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if( _agent.remainingDistance <= _agent.stoppingDistance )
        {
            _agent.SetDestination(_points[Random.Range(0, _points.Count)].forward);
        }
        _timer += Time.deltaTime;
        if (_timer > 10) { animator.SetBool("isPatrolling", false); }

        float distance = Vector3.Distance(animator.transform.position, _player.position);

        if (distance < _chaseRange) { animator.SetBool("isChasing", true); }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ( _agent == null )
            _agent.SetDestination(_agent.transform.position);
    }

}
