using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

namespace Level
{
    public class CameraPropertiesModifier : MonoBehaviour
    {
        [Header("Color Background")]
        [SerializeField] private string _colorEndHex;

        [SerializeField] private float _colorBackgroundDuration = 1f;

        private Color _colorEnd;

        [Header("Post Process")]
        [SerializeField] private PostProcessProfile _postProcessProfile;

        [SerializeField] private float _maxIntensity = 0.8f;
        [SerializeField] private float _intensityPostProcessDuration = 2f;

        private Vignette _vignette;

        void Start()
        {
            ColorUtility.TryParseHtmlString(_colorEndHex, out _colorEnd);

            //Color Background
            GetComponent<Camera>().DOColor(_colorEnd, _colorBackgroundDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);

            //Post Process Vignette Change Intensity
            if (_postProcessProfile.TryGetSettings(out _vignette))
                DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, _maxIntensity, _intensityPostProcessDuration).SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);
        }
    }
}