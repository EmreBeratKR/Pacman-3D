using System.Collections;
using UnityEngine;

public class PacmanParticleEmitter : Scenegleton<PacmanParticleEmitter>
{
    [SerializeField] private ParticleSystem dieParticles;


    public static Coroutine EmitDieParticles()
    {
        return Instance.StartCoroutine(EmitCoroutine());


        IEnumerator EmitCoroutine()
        {
            var particles = Instantiate(Instance.dieParticles, Pacman.Transform.position, Quaternion.identity, Instance.transform);

            yield return new WaitForSeconds(particles.main.duration);

            Destroy(particles.gameObject);
        }
    }
}
