using UnityEngine;

public class PacmanMovement : Scenegleton<PacmanMovement>
{
    [SerializeField] private float moveSpeed;
    [SerializeField, Range(0, 1f)] private float rotationSpeed;


    private Vector3 direction;
    public static Vector3 Direction => Instance.direction;
    public static Vector3 Up => Quaternion.Euler(0, 0, DesiredAngle) * Vector3.up;
    public static Vector3 Right => Quaternion.Euler(0, 0, DesiredAngle) * Vector3.right;


    public static float DesiredAngle
    {
        get
        {
            switch (Pacman.Facing)
            {
                default : return 0;
                
                case Facing.Down : return 180;

                case Facing.Right : return -90;

                case Facing.Left : return 90;
            }
        }
    }


    private void FixedUpdate()
    {
        SmoothRotate();
    }


    private void Update()
    {
        UpdateDirection();
        
        Pacman.State = TryMove() ? PacmanState.Move : PacmanState.Idle;
    }


    private bool TryMove()
    {
        if (PacmanRaycaster.IsBlocked(Facing.Up)) return false;

        Pacman.Transform.position += Direction * (moveSpeed * Time.deltaTime);

        return true;
    }

    private void UpdateDirection()
    {
        var facing = Pacman.Facing;
        var newDirection = direction;

        if (PacmanUserInput.IsUp)
        {
            facing = Facing.Up;
            newDirection = Vector3.up;
        }

        else if (PacmanUserInput.IsDown)
        {
            facing = Facing.Down;
            newDirection = Vector3.down;
        }

        else if (PacmanUserInput.IsRight)
        {
            facing = Facing.Right;
            newDirection = Vector3.right;
        }

        else if (PacmanUserInput.IsLeft)
        {
            facing = Facing.Left;
            newDirection = Vector3.left;
        }

        if (facing == Pacman.Facing) return;

        var relativeFacing = facing.Relative(Pacman.Facing);

        if (PacmanRaycaster.IsBlocked(relativeFacing)) return;

        Pacman.Facing = facing;
        direction = newDirection;
    }

    private void SmoothRotate()
    {
        var currentAngle = Pacman.Transform.eulerAngles.z;

        var lerpedAngle = Mathf.LerpAngle(currentAngle, DesiredAngle, rotationSpeed);

        Pacman.Transform.eulerAngles = new Vector3(0, 0, lerpedAngle);
    }
}
