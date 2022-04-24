using System;
using UnityEngine;

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
    }





    [SerializeField] private ScatterAnimation scatterAnimation;

    [Serializable]
    internal struct ScatterAnimation
    {
        public MeshRenderer body;
        public MeshRenderer eyes;
        public MeshRenderer pupil;
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
                    pupil.material = normalFaceMaterial;
                    mouth.gameObject.SetActive(false);
                    break;

                case GameMode.Scatter:
                    if (GameController.IsCriticalScatter)
                    {
                        float warningInterval = GameController.CriticalScatterTime / (float) warningFrequency;
                        bool primaryColor = Mathf.RoundToInt(GameController.RemainingScatterTime / warningInterval) % 2 == 0;
                        body.material = primaryColor ? scatterBodyMaterial : criticalScatterBodyMaterial;
                        pupil.material = primaryColor ? scatterFaceMaterial : criticalScatterFaceMaterial;
                        mouth.material = primaryColor ? scatterFaceMaterial : criticalScatterFaceMaterial;
                    }
                    else
                    {
                        body.material = scatterBodyMaterial;
                        pupil.material = scatterFaceMaterial;
                        mouth.material = scatterFaceMaterial;
                    }

                    eyes.gameObject.SetActive(false);
                    mouth.gameObject.SetActive(true);
                    break;
            }
        }
    }
}
