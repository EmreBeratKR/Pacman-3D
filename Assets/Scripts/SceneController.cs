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
            var operation = SceneManager.LoadSceneAsync(Instance.scenes.game);

            while (!operation.isDone)
            {
                yield return null;
            }

            GameController.Init();
        }
    }

    public static void RestartGame()
    {
        Instance.StartCoroutine(RestartCoroutine());

        IEnumerator RestartCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            LoadGame();
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
