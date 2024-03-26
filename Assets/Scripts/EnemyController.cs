using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypoint = 0;
    private NavMeshAgent agent;
    public Animator stateMachine;
    public Vector3 NoiseLocation { get; private set; }

    // search 
    public float rotationSpeed = 80f;
    public float maxAngle = 180f;
    public float duration = 3f;
    private bool isSearching = false;

    // chase
    public GameObject player;
    private bool isChasing = false;

    private readonly int toSearchParam = Animator.StringToHash("ToSearch");
    private readonly int toChaseParam = Animator.StringToHash("ToChase");
    private readonly int cancelSearchParam = Animator.StringToHash("CancelSearch");
    private readonly int cancelChaseParam = Animator.StringToHash("CancelChase");

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(stateMachine, "Error: You forgot to assign the animator!");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&!(isSearching||isChasing))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo))
            {
                NoiseLocation = hitInfo.point;
                stateMachine.SetTrigger(toSearchParam);
            }
        }
        if (isChasing)
        {
            agent.destination = player.transform.position;
        }
    }

    public void SetNextWaypoint()
    {
        if (currentWaypoint < waypoints.Length)
        {
            // set next point
            agent.SetDestination(waypoints[currentWaypoint].position);
            currentWaypoint++;
        }
        else
        {
            currentWaypoint = 0;
            agent.SetDestination(waypoints[currentWaypoint].position);
            currentWaypoint++;
        }
    }

    public void RotateAndSearch()
    {
        StartCoroutine(RotateObjectCoroutine());
    }

    IEnumerator RotateObjectCoroutine()
    {
        isSearching = true;
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            float currentAngle = Mathf.PingPong(Time.time * rotationSpeed, maxAngle * 2) - maxAngle;
            transform.rotation = Quaternion.Euler(0f, currentAngle, 0f);
            yield return null;
        }

        isSearching = false;
        stateMachine.SetTrigger(cancelSearchParam);
    }

    public void ChasePlayer()
    {
        stateMachine.SetTrigger(toChaseParam);
        StartCoroutine(ChasePlayerCoroutine(3f));
    }

    IEnumerator ChasePlayerCoroutine(float seconds)
    {
        isChasing = true;
        agent.destination = player.transform.position; // 设置追逐目标为玩家的位置

        yield return new WaitForSeconds(seconds); // 等待指定秒数

        isChasing = false;
        // 在追逐结束后，你可以在这里添加一些额外的逻辑
        stateMachine.SetTrigger(cancelChaseParam);
    }

}
