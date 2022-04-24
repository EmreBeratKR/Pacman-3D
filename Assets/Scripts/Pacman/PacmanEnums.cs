public enum Facing { Up, Down, Right, Left, None }

public enum PacmanState { Idle, Move }

public static class FacingExtensions
{
    public static Facing Opposite(this Facing facing)
    {
        switch (facing)
        {
            default : return Facing.Down;

            case Facing.Down : return Facing.Up;

            case Facing.Right : return Facing.Left;

            case Facing.Left : return Facing.Right;
        }
    }

    public static Facing Relative(this Facing globalFacing, Facing pivot)
    {
        switch (pivot)
        {
            default : return globalFacing;

            case Facing.Down : return globalFacing.Opposite();

            case Facing.Right:
                switch (globalFacing)
                {
                    default : return Facing.Left;

                    case Facing.Down : return Facing.Right;

                    case Facing.Right : return Facing.Up;

                    case Facing.Left : return Facing.Down;
                }

            case Facing.Left:
                switch (globalFacing)
                {
                    default : return Facing.Right;

                    case Facing.Down : return Facing.Left;

                    case Facing.Right : return Facing.Down;

                    case Facing.Left : return Facing.Up;
                }
        }
    }
}
