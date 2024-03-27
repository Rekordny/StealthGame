using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchState : StateMachineBehaviour
{
    private NavMeshAgent agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null) agent = animator.GetComponent<NavMeshAgent>();

        Debug.Log("<color=yellow><size=16>Starting to search!</size></color>");
        agent.SetDestination(agent.transform.GetComponent<EnemyController>().NoiseLocation);

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // When saw another agent in close range, we know some collision will block us to reach destination
        if (agent.remainingDistance <= agent.stoppingDistance || agent.transform.GetComponent<EnemyController>().ShouldStartSearchInstantly())
        {
            Debug.Log("<color=cyan><size=16>Searching...</size></color>");
            agent.transform.GetComponent<EnemyController>().RotateAndSearch();
        }
    }
}
