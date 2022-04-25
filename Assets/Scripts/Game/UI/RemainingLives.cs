using UnityEngine;

public class RemainingLives : Scenegleton<RemainingLives>
{
    [SerializeField] private GameObject[] lives;


    private void Start()
    {
        UpdateLivesBoard(Pacman.RemainingLife);
    }

    private void OnEnable()
    {
        EventSystem.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventSystem.OnGameOver -= OnGameOver;
    }

    private void OnGameOver(int score, int remainingLives)
    {
        UpdateLivesBoard(remainingLives);
    }

    public static void UpdateLivesBoard(int remainingLives)
    {
        var counter = 1;
        foreach (var life in Instance.lives)
        {
            life.SetActive(counter <= remainingLives);
            counter++;
        }
    }
}
