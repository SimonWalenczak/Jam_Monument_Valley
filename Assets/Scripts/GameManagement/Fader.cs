using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Fader : MonoBehaviour
{
    [SerializeField] private float _timeToFade;
    
    private void Start()
    {
        GetComponent<Image>().DOFade(0, _timeToFade);
    }

    public void FadeOut()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        GetComponent<Image>().DOFade(1, _timeToFade);
        yield return new WaitForSeconds(_timeToFade);
        SceneManager.LoadScene("MainScene");
    }
}
