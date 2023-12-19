using System;
using UnityEngine;
using YG;

public class CheckingRequestsLeaderboard : MonoBehaviour
{
    [SerializeField] private LeaderboardYG _leaderboard1;
    [SerializeField] private LeaderboardYG _leaderboard2;
    [SerializeField] private LeaderboardYG _leaderboard3;
    [SerializeField] private LeaderboardYG _leaderboard4;

    private void Start()
    {
        Debug.Log("Обновил позиции лидеров");
        _leaderboard1.UpdateLB();
        _leaderboard2.UpdateLB();
        _leaderboard3.UpdateLB();
        _leaderboard4.UpdateLB();
    }
}
