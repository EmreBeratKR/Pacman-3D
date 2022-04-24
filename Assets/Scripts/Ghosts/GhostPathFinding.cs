using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GhostPathFinding : MonoBehaviour
{
    private GhostTargetProvider targetProvider;
    private NavMeshAgent agent;


    private void Start()
    {
        SetAgent();
        targetProvider = this.GetComponent<GhostTargetProvider>();
    }

    private void SetAgent()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }


    private void Update()
    {
        if (!targetProvider.Destination.HasValue) return;

        agent.destination = targetProvider.Destination.Value;
    }
}
