using System.Collections;
using UnityEngine;
using YG;

public class LoadingDataPlayer : MonoBehaviour
{
    [SerializeField] LeaderboardYG leaderboardYG;
    private void Start()
    {
        if (YandexGame.SDKEnabled)
        {
            StartCoroutine(CheckAuth());
        }
    }
    IEnumerator CheckAuth()
    {
        while (true)
        {
            if (YandexGame.auth)
            {
                StopCoroutine(CheckAuth());
            }
            else
            {
                YandexGame.RequestAuth();
                
                yield return new WaitForSeconds(3);
            }

            yield return new WaitForSeconds(3);
        }
    }
}
