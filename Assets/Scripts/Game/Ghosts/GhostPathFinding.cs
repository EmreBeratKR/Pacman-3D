using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(GhostTargetProvider))]
public class GhostPathFinding : MonoBehaviour
{
    private GhostTargetProvider targetProvider;
    private NavMeshAgent agent;


    public Vector3 Direction
    {
        get 
        {
            var velocity = agent.velocity;
            velocity.y = 0;
            return velocity.normalized;
        }
    }

    public Facing Facing
    {
        get
        {
            if (IsArrived) return Facing.None;

            var direction = Direction;

            var upDot = Vector3.Dot(direction, Vector3.forward);
            var rightDot = Vector3.Dot(direction, Vector3.right);
            
            if (Mathf.Abs(upDot) > Mathf.Abs(rightDot))
            {
                return upDot < 0 ? Facing.Down : Facing.Up;
            }

            return rightDot < 0 ? Facing.Left : Facing.Right;
        }
    }

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
        agent.isStopped = GameController.IsFreezed;

        if (!targetProvider.Destination.HasValue) return;

        agent.destination = targetProvider.Destination.Value;
    }
}
