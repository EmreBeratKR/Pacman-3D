using System.Collections;
using UnityEngine;

public class TextWriter : Scenegleton<TextWriter>
{
    [SerializeField] private WritableText gameOverText;
    [SerializeField] private WritableText gameWinText;


    public static Coroutine WriteGameOver()
    {
        return Instance.StartCoroutine(WriteCoroutine());
        
        IEnumerator WriteCoroutine()
        {
            yield return Instance.gameOverText.Write();

            yield return new WaitForSeconds(0.5f);
        }

    }

    public static Coroutine WriteGameWin()
    {
        return Instance.StartCoroutine(WriteCoroutine());
        
        IEnumerator WriteCoroutine()
        {
            yield return Instance.gameWinText.Write();

            yield return new WaitForSeconds(0.5f);
        }
    }
}
