using System;
using UnityEngine;
using NaughtyAttributes;

public class PacmanAnimator : Scenegleton<PacmanAnimator>
{
    [SerializeField] private AnimatorParams animatorParams;


    private void Update()
    {
        animatorParams.SetBool(Pacman.State);
    }
}

[Serializable]
internal struct AnimatorParams
{
    [SerializeField] private Animator animator;
    [SerializeField, AnimatorParam("animator")] private string idle;
    [SerializeField, AnimatorParam("animator")] private string move;


    public void SetBool(PacmanState state)
    {
        if (GameController.IsFreezed)
        {
            state = PacmanState.Idle;
        }

        switch (state)
        {
            default:
                animator.SetBool(idle, true);
                animator.SetBool(move, false);
                break;

            case PacmanState.Move:
                animator.SetBool(idle, false);
                animator.SetBool(move, true);
                break;
        }
    }
}