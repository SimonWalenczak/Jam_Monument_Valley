using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LobbyBackground : MonoBehaviour
{
    [field: SerializeField] private Image _background;

    [field: SerializeField] private float _timer;
    
    private void Start()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        _background.DOFade(0.1f, _timer);
        yield return new WaitForSeconds(_timer);
        _background.DOFade(0.3f, _timer);
        yield return new WaitForSeconds(_timer);

        StartCoroutine(Fade());
    }
}
