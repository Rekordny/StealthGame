using UnityEngine;
using UnityEngine.AI;

public class AlertedState : StateMachineBehaviour
{
    private NavMeshAgent agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null) agent = animator.GetComponent<NavMeshAgent>();

        Debug.Log("<color=yellow><size=16>What's that noise?!</size></color>");

        ParentController pc = animator.GetComponent<ParentController>();
        agent.SetDestination(pc.NoiseLocation);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("<color=red><size=16>Ahh, must be the wind...</size></color>");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance > agent.stoppingDistance) return;

        Debug.Log("<color=cyan><size=16>Investigating!</size></color>");
    }
}
