using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachBehaviour : StateMachineBehaviour
{
    private Transform _player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(animator.transform.position, _player.position);
        if (distance > 2 ) { animator.SetBool("isAttaching", false); }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
