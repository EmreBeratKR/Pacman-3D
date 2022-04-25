using UnityEngine;
using TMPro;

public class ScoreParticle : MonoBehaviour
{
    [SerializeField] private TextMeshPro particle;
    [SerializeField] private float lifeTime;
    private new Transform camera;


    private void Start()
    {
        GameController.FreezeGame();
        camera = Camera.main.transform;
        Destroy(this.gameObject, lifeTime);
    }

    private void OnDestroy()
    {
        GameController.UnFreezeGame();
    }


    public void Set(int score)
    {
        particle.text = score.ToString();
    }
}
