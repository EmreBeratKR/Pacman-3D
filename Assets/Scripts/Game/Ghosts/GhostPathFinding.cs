using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(GhostTargetProvider))]
public class GhostPathFinding : MonoBehaviour
{
    [SerializeField] private float basePacingTime;
    [SerializeField] private float sleepTime;
    private Coroutine pacingCoroutine;
    private Vector3 initialPosition;
    private bool isPacing;
    private bool isUp;

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
            if (isPacing)
            {
                return isUp ? Facing.Up : Facing.Down;
            }

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
        initialPosition = transform.position;
        SetAgent();
        targetProvider = this.GetComponent<GhostTargetProvider>();
        if (basePacingTime > 0)
        {
            PaceUpAndDown();
        }
    }

    private void SetAgent()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    private void PaceUpAndDown()
    {
        isPacing = true;
        agent.enabled = false;

        pacingCoroutine = StartCoroutine(PacingCoroutine());


        IEnumerator PacingCoroutine()
        {
            isUp = true;

            while (!targetProvider.Destination.HasValue)
            {
                yield return null;
            }

            yield return new WaitForSeconds(sleepTime);

            while (basePacingTime > 0)
            {
                while (true)
                {
                    if (GameController.IsFreezed) StopCoroutine(pacingCoroutine);

                    var deltaTime = Time.deltaTime;

                    transform.position += (isUp ? Vector3.forward : Vector3.back) * (agent.speed * deltaTime);

                    if (Mathf.Abs(transform.position.z - initialPosition.z) >= 2)
                    {
                        isUp = !isUp;
                        break;
                    }

                    basePacingTime -= deltaTime;

                    yield return null; 
                }
            }

            isPacing = false;
            agent.enabled = true;
            pacingCoroutine = null;
        }
    }


    private void Update()
    {
        if (isPacing) return;

        agent.isStopped = GameController.IsFreezed;

        if (!targetProvider.Destination.HasValue) return;

        agent.destination = targetProvider.Destination.Value;
    }
}
