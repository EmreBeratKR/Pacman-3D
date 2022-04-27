using System.Collections;
using UnityEngine;
using NaughtyAttributes;

public class SceneTransition : Singleton<SceneTransition>
{
    [SerializeField] private CanvasGroup transitor;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration;


    [Button]
    public static Coroutine FadeIn()
    {
        return Instance.StartCoroutine(FadeCoroutine(0f, 1f));
    }

    [Button]
    public static Coroutine FadeOut()
    {
        return Instance.StartCoroutine(FadeCoroutine(1f, 0f));
    }


    private static IEnumerator FadeCoroutine(float from, float to)
    {
        var timer = 0f;

        while (timer < Instance.duration)
        {
            timer += Time.deltaTime;
            Instance.transitor.alpha = Mathf.Lerp(from, to, Instance.curve.Evaluate(timer / Instance.duration));

            yield return null;
        }
    }
}
