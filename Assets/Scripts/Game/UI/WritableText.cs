using System.Collections;
using UnityEngine;
using TMPro;

public class WritableText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private string content;
    [SerializeField] private float duration;


    public Coroutine Write()
    {
        title.text = "";
        title.gameObject.SetActive(true);

        return StartCoroutine(ShowCoroutine());

        IEnumerator ShowCoroutine()
        {
            var timePerLetter = duration / content.Length;
            var timer = 0f;
            var currentLength = 0;

            while (currentLength < content.Length)
            {
                timer += Time.deltaTime;

                if (timer >= timePerLetter)
                {
                    timer = 0f;
                    currentLength++;
                }

                title.text = content.Substring(0, currentLength);
                
                yield return null;
            }
        }
    }

    public void Hide()
    {
        title.gameObject.SetActive(false);
    }
}
