using System.Collections;
using UnityEngine;

public class WinAnimation : Scenegleton<WinAnimation>
{
    [SerializeField] private MeshRenderer walls;
    [SerializeField] private Material normalMaterial, blinkedMaterial;
    [SerializeField] private float sleep, duration, blinkRate;


    public static Coroutine Play()
    {
        return Instance.StartCoroutine(WinCoroutine());

        IEnumerator WinCoroutine()
        {
            bool isBlinked = true;
            float totalTimer = 0;
            float timer = 0;

            yield return new WaitForSeconds(Instance.sleep);

            while (totalTimer < Instance.duration)
            {
                var deltaTime = Time.deltaTime;

                totalTimer += deltaTime;
                timer += deltaTime;

                if (timer > (1 / Instance.blinkRate))
                {
                    timer = 0;
                    isBlinked = !isBlinked;
                }

                Instance.walls.material = isBlinked ? Instance.blinkedMaterial : Instance.normalMaterial;

                yield return null;
            }
        }
    }
}
