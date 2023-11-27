using UnityEngine;
using YG;

public class BtnUpdateLB : MonoBehaviour
{
    [SerializeField] LeaderboardYG leaderboardYG;
    
    public void NewName(string name)
    {
        if (name!="")
        {
            leaderboardYG.nameLB = "TimeToCompleteLevel1";
            leaderboardYG.UpdateLB();
        }
    }
}
