using System;
using UnityEngine;
using NaughtyAttributes;

public class GhostAnimator : MonoBehaviour
{
    private Ghost ghost;


    private void Awake()
    {
        ghost = this.GetComponent<Ghost>();
    }

    private void Update()
    {
        scatterAnimation.Update(ghost.Mode);
        eyeAnimation.Update(ghost.Facing);
    }





    [SerializeField] private ScatterAnimation scatterAnimation;

    [Serializable]
    internal struct ScatterAnimation
    {
        public MeshRenderer body;
        public MeshRenderer eyes;
        public MeshRenderer[] pupils;
        public MeshRenderer mouth;
        public Material normalBodyMaterial;
        public Material scatterBodyMaterial;
        public Material criticalScatterBodyMaterial;
        public Material normalFaceMaterial;
        public Material scatterFaceMaterial;
        public Material criticalScatterFaceMaterial;
        public int warningFrequency;


        public void Update(GameMode mode)
        {
            switch (mode)
            {
                case GameMode.Chase:
                    body.material = normalBodyMaterial;
                    eyes.gameObject.SetActive(true);
                    pupils[0].material = normalFaceMaterial;
                    pupils[1].material = normalFaceMaterial;
                    mouth.gameObject.SetActive(false);
                    break;

                case GameMode.Scatter:
                    if (GameController.IsCriticalScatter)
                    {
                        float warningInterval = GameController.CriticalScatterTime / (float) warningFrequency;
                        bool primaryColor = Mathf.RoundToInt(GameController.RemainingScatterTime / warningInterval) % 2 == 0;
                        body.material = primaryColor ? scatterBodyMaterial : criticalScatterBodyMaterial;
                        pupils[0].material = primaryColor ? scatterFaceMaterial : criticalScatterFaceMaterial;
                        pupils[1].material = primaryColor ? scatterFaceMaterial : criticalScatterFaceMaterial;
                        mouth.material = primaryColor ? scatterFaceMaterial : criticalScatterFaceMaterial;
                    }
                    else
                    {
                        body.material = scatterBodyMaterial;
                        pupils[0].material = scatterFaceMaterial;
                        pupils[1].material = scatterFaceMaterial;
                        mouth.material = scatterFaceMaterial;
                    }

                    eyes.gameObject.SetActive(false);
                    mouth.gameObject.SetActive(true);
                    break;
            }
        }
    }



    [SerializeField] private EyeAnimation eyeAnimation;

    [Serializable]
    internal struct EyeAnimation
    {
        [SerializeField] private Animator animator;
        [AnimatorParam("animator")] public string up;
        [AnimatorParam("animator")] public string down;
        [AnimatorParam("animator")] public string right;
        [AnimatorParam("animator")] public string left;

        
        public void Update(Facing facing)
        {
            switch (facing)
            {
                default:
                    animator.SetBool(up, false);
                    animator.SetBool(down, false);
                    animator.SetBool(right, false);
                    animator.SetBool(left, false);
                    break;

                case Facing.Up:
                    animator.SetBool(up, true);
                    animator.SetBool(down, false);
                    animator.SetBool(right, false);
                    animator.SetBool(left, false);
                    break;

                case Facing.Down:
                    animator.SetBool(up, false);
                    animator.SetBool(down, true);
                    animator.SetBool(right, false);
                    animator.SetBool(left, false);
                    break;

                case Facing.Right:
                    animator.SetBool(up, false);
                    animator.SetBool(down, false);
                    animator.SetBool(right, true);
                    animator.SetBool(left, false);
                    break;

                case Facing.Left:
                    animator.SetBool(up, false);
                    animator.SetBool(down, false);
                    animator.SetBool(right, false);
                    animator.SetBool(left, true);
                    break;
            }
        }
    }
}
