using System.Collections;
using UnityEngine;
using YG;

public class LoadingDataPlayer : MonoBehaviour
{
    [SerializeField] LeaderboardYG leaderboardYG;
    private void Start()
    {
        NewName();
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
                NewName();
                
                yield return new WaitForSeconds(3);
            }

            yield return new WaitForSeconds(3);
        }
    }
    
    public void NewName()
    {
        leaderboardYG.nameLB = "TimeToCompleteLevel1";
        leaderboardYG.UpdateLB();
    }
}
