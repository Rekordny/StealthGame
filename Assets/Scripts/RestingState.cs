using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Diagnostics;

public class RestingState : StateMachineBehaviour
{
    private NavMeshAgent agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null) agent = animator.GetComponent<NavMeshAgent>();

        Debug.Log("<color=yellow><size=16>Starting to rest!</size></color>");

        ParentController pc = animator.GetComponent<ParentController>();
        agent.SetDestination(pc.livingRoomDest.position);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("<color=red><size=16>Finished resting!</size></color>");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance > agent.stoppingDistance) return;

        Debug.Log("<color=cyan><size=16>ZzzZZZZzZz...</size></color>");
    }
}
