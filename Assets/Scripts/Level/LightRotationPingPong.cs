using UnityEngine;
using DG.Tweening;

namespace Level
{
    public class LightRotationPingPong : MonoBehaviour
    {
        [SerializeField] private Transform _light;
        [SerializeField] private Vector3 _lightRotationEnd;
        [SerializeField] private float _duration = 2f;
        [SerializeField] private Ease _ease;

        void Start()
        {
            _light.DORotate(_lightRotationEnd, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(_ease);
        }
    }
}