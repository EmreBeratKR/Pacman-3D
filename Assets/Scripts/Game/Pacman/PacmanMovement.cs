using UnityEngine;

public class PacmanMovement : Scenegleton<PacmanMovement>
{
    [SerializeField] private float moveSpeed;
    [SerializeField, Range(0, 1f)] private float rotationSpeed;


    public static Vector3 Direction
    {
        get
        {
            switch (Pacman.Facing)
            {
                default : return Vector3.zero;

                case Facing.Up : return Vector3.forward;
                
                case Facing.Down : return Vector3.back;
                
                case Facing.Right : return Vector3.right;

                case Facing.Left : return Vector3.left;
            }
        }
    }

    public static Vector3 Up => Quaternion.Euler(0, DesiredAngle, 0) * Vector3.forward;
    public static Vector3 Right => Quaternion.Euler(0, DesiredAngle, 0) * Vector3.right;


    public static float DesiredAngle
    {
        get
        {
            switch (Pacman.Facing)
            {
                default : return 0;
                
                case Facing.Down : return 180;

                case Facing.Right : return 90;

                case Facing.Left : return -90;

                case Facing.None : return 90;
            }
        }
    }


    private void FixedUpdate()
    {
        SmoothRotate();
    }


    private void Update()
    {
        if (GameController.IsFreezed) return;

        UpdateFacing();

        if (!TryStartGame())
        {
            Pacman.State = PacmanState.Idle;
            return;
        }
        
        Pacman.State = TryMove() ? PacmanState.Move : PacmanState.Idle;
    }


    private bool TryStartGame()
    {
        if (AudioManager.IsStartMusicPlaying) return false;

        if (!GameController.GameStarted)
        {
            if (Pacman.Facing != Facing.None)
            {
                GameController.StartGame();
                return true;
            }

            return false;
        }

        return true;
    }
    
    private bool TryMove()
    {
        if (PacmanRaycaster.IsBlocked(Facing.Up)) return false;

        Pacman.Transform.position += Direction * (moveSpeed * Time.deltaTime);

        return true;
    }

    private void UpdateFacing()
    {
        var facing = Pacman.Facing;

        if (PacmanUserInput.IsUp)
        {
            facing = Facing.Up;
        }

        else if (PacmanUserInput.IsDown)
        {
            facing = Facing.Down;
        }

        else if (PacmanUserInput.IsRight)
        {
            facing = Facing.Right;
        }

        else if (PacmanUserInput.IsLeft)
        {
            facing = Facing.Left;
        }

        if (facing == Pacman.Facing) return;

        var currentFacing = Pacman.Facing;

        if (currentFacing == Facing.None)
        {
            currentFacing = Pacman.InitialFacing;
        }

        var relativeFacing = facing.Relative(currentFacing);

        if (PacmanRaycaster.IsBlocked(relativeFacing)) return;

        Pacman.Facing = facing;
    }

    private void SmoothRotate()
    {
        var currentAngle = Pacman.Transform.eulerAngles.y;

        var lerpedAngle = Mathf.LerpAngle(currentAngle, DesiredAngle, rotationSpeed);

        Pacman.Transform.eulerAngles = new Vector3(0, lerpedAngle, 0);
    }
}
