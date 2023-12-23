using UnityEngine;
using YG;

public class FixFpsForMobile : MonoBehaviour
{
    [SerializeField] private MeshRenderer _floor;
    [SerializeField] private MeshRenderer _wall;

    [Header("Materials")] 
    [SerializeField] private Material _materialFloor;
    [SerializeField] private Material _materialWall;

    private void Start()
    {
        if (YandexGame.SDKEnabled)
        {
            if (YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.deviceType=="mobile")
            {
                _floor.material = _materialFloor;
                _wall.material = _materialWall;
            }
        }
    }
}
