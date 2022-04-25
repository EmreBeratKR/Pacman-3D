using System.Collections;
using UnityEngine;

public class PacmanParticleEmitter : Scenegleton<PacmanParticleEmitter>
{
    [SerializeField] private ParticleSystem dieParticles;
    [SerializeField] private ScoreParticle scoreParticle;


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
    
    public static void EmitScoreParticle(int score)
    {
        var particle = Instantiate(Instance.scoreParticle, Pacman.Position + Vector3.up * 2.5f, Quaternion.identity, Instance.transform);
        particle.Set(score);
    }
}
