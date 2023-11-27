using System.Collections;
using UnityEngine;
using YG;
using TMPro;

public class LoadingDataPlayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] LeaderboardYG leaderboardYG;
    private void OnEnable() => YandexGame.GetDataEvent += DebugData;
    private void OnDisable() => YandexGame.GetDataEvent -= DebugData;
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
                NewName();
                DebugData();
                
                yield return new WaitForSeconds(3);
            }
        }
    }
    void DebugData()
    {
        _name.text = YandexGame.playerName;
    }
    
    public void NewName()
    {
        leaderboardYG.nameLB = "TimeToCompleteLevel1";
        leaderboardYG.UpdateLB();
    }
}
