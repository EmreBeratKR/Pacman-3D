using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GhostPathFinding : MonoBehaviour
{
    private GhostTargetProvider targetProvider;
    private NavMeshAgent agent;


    public bool IsArrived
    {
        get
        {
            if (!targetProvider.Destination.HasValue) return true;

            var position = transform.position;
            position.y = 0;
            var sqrtDistance = (targetProvider.Destination.Value - position).sqrMagnitude;

            return sqrtDistance <= 0.01f;
        }
    }


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
