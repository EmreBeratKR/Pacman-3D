using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class SceneController : Singleton<SceneController>
{
    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(Instance.scenes.mainMenu);
    }

    public static void LoadGame()
    {
        SceneManager.LoadScene(Instance.scenes.game);
    }

    public static void LoadGameAsync()
    {
        Instance.StartCoroutine(LoadGameCoroutine());

        IEnumerator LoadGameCoroutine()
        {
            yield return SceneTransition.FadeIn();

            var operation = SceneManager.LoadSceneAsync(Instance.scenes.game);

            while (!operation.isDone)
            {
                yield return null;
            }

            GameController.Init();

            yield return SceneTransition.FadeOut();
        }
    }

    public static Coroutine RestartGame()
    {
        return Instance.StartCoroutine(RestartCoroutine());

        IEnumerator RestartCoroutine()
        {
            yield return SceneTransition.FadeIn();

            LoadGame();

            yield return SceneTransition.FadeOut();
        }
    }




    [SerializeField] private Scenes scenes;

    [Serializable]
    internal struct Scenes
    {
        [Scene] public int mainMenu;
        [Scene] public int game;
    }
}
