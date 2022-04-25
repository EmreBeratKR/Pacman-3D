using System.Collections;
using UnityEngine;
using TMPro;

public class GameOverTitle : Scenegleton<GameOverTitle>
{
    private const string ResultTitle = "GAME OVER!";

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private float duration;



    public static Coroutine Show()
    {
        Instance.title.text = "";
        Instance.title.gameObject.SetActive(true);

        return Instance.StartCoroutine(ShowCoroutine());

        IEnumerator ShowCoroutine()
        {
            var timePerLetter = Instance.duration / ResultTitle.Length;
            var timer = 0f;
            var currentLength = 0;

            while (currentLength < ResultTitle.Length)
            {
                timer += Time.deltaTime;

                if (timer >= timePerLetter)
                {
                    timer = 0f;
                    currentLength++;
                }

                Instance.title.text = ResultTitle.Substring(0, currentLength);
                
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            yield return SceneTransition.FadeIn();
            
            SceneController.LoadMainMenu();

            yield return SceneTransition.FadeOut();
        }
    }
}
