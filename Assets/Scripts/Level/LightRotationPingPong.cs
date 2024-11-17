using UnityEngine;
using DG.Tweening;

namespace Level
{
    public class LightRotationPingPong : MonoBehaviour
    {
        [SerializeField] private Light _light;
        [SerializeField] private Vector3 _lightRotationEnd;
        [SerializeField] private float _duration = 2f;

        void Start()
        {
            _light.transform.DORotate(_lightRotationEnd, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
    }
}