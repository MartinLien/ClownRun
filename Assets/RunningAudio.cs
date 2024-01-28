using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningAudio : StateMachineBehaviour
{
    [SerializeField] private AudioSource _source = null;
    [SerializeField] private AudioClip _clip = null;

    [SerializeField] private float _rightFootTiming = 0.3f;
    [SerializeField] private float _leftFootTiming = 0.6f;

    bool _playedRight = false;
    bool _playedLeft = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float lenght = stateInfo.length;
        float time = animator.playbackTime;

        if (time == 0)
        {
            _playedRight = false;
            _playedLeft = false;
        }

        if (!_playedRight)
        {
            _source.PlayOneShot(_clip);
            _playedRight = true;
        }
        
        if (!_playedLeft)
        {
            _source.PlayOneShot(_clip);
            _playedLeft = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
