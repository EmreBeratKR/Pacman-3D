using System.Collections;
using UnityEngine;

public class ConsumableContainer : Singleton<ConsumableContainer>
{
    [SerializeField] private Transform[] collectableParents;


    public static bool IsAllCollected
    {
        get
        {
            foreach (var collectableParent in Instance.collectableParents)
            {
                if (collectableParent.childCount != 0) return false;
            }

            return true;
        }
    }


    public static void Destroy()
    {
        Destroy(Instance.gameObject);
    }


    private void OnEnable()
    {
        EventSystem.OnConsumableConsumed += OnConsumableConsumed;
    }

    private void OnDisable()
    {
        EventSystem.OnConsumableConsumed -= OnConsumableConsumed;
    }

    private void OnConsumableConsumed(int score)
    {
        if (IsAllCollected)
        {
            StartCoroutine(WinStartCoroutine());
        }

        IEnumerator WinStartCoroutine()
        {
            GameController.FreezeGame();

            yield return null;

            EventSystem.GameWin(CurrentScore.Score);
        }
    }
}
