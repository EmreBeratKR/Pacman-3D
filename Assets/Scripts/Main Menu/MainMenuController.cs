using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneController.LoadGameAsync();
    }

    public void OpenSourceCode()
    {
        Application.OpenURL("https://github.com/EmreBeratKR/Pacman-3D");
    }
}
