using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private string enemyTag = "Enemy";
    
    public static List<GameObject> agents;
    private void Awake()
    {
        agents = new List<GameObject>();
        foreach (Transform t in transform)
            agents.Add(t.gameObject);
        foreach (GameObject agent in agents)
        {
            agent.tag = enemyTag;
        }
    }

    public static List<GameObject> GetAgents()
    {
        return agents;
    }
}
