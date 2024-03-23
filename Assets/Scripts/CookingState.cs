using UnityEngine;
using UnityEngine.AI;

public class CookingState : StateMachineBehaviour
{
    private NavMeshAgent agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null) agent = animator.GetComponent<NavMeshAgent>();

        Debug.Log("<color=yellow><size=16>Starting to cook!</size></color>");

        ParentController pc = animator.GetComponent<ParentController>();

        agent.SetDestination(pc.kitchenDest.position);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("<color=red><size=16>Leaving the kitchen!</size></color>");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log("<color=cyan><size=16>Cooking dinner...</size></color>");
        }
    }
}
