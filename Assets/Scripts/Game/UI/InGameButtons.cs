using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InGameButtons : MonoBehaviour
{
    [SerializeField] private Toggle volumeToggle;


    private void Start()
    {
        SetVolumeToggle();
    }

    private void SetVolumeToggle()
    {
        volumeToggle.isOn = AudioManager.IsVolumeOn;
    }

    public void ReturnMainMenu()
    {
        GameController.FreezeGame();

        AudioManager.StopAll();

        StartCoroutine(ReturnCoroutine());

        IEnumerator ReturnCoroutine()
        {
            yield return SceneTransition.FadeIn();

            ConsumableContainer.Destroy();

            SceneController.LoadMainMenu();

            yield return SceneTransition.FadeOut();
        }
    }

    public void OnVolumeToggled()
    {
        AudioManager.Volume = volumeToggle.isOn ? 1f : 0f;
    }
}
