using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

namespace GameManagement
{
    /// <summary>
    /// Handles the fade-in and fade-out effects for the screen. 
    /// It allows transitioning between scenes with a smooth fade effect.
    /// </summary>
    public class Fader : MonoBehaviour
    {
        #region Properties
    
        [SerializeField] private float _timeToFadeIn;
        [SerializeField] private float _timeToFadeOut;

        #endregion

        #region Methods
    
        private void Start()
        {
            StartCoroutine(FadeIn());
        }

        IEnumerator FadeIn()
        {
            yield return new WaitForSeconds(_timeToFadeIn);
            GetComponent<Image>().DOFade(0, _timeToFadeOut);
        }

        /// <summary>
        /// Triggers the fade-out effect and loads the scene after the fade completes.
        /// </summary>
        public void LaunchFadeOutLobby()
        {
            StartCoroutine(FadeOutLobby());
        }
        
        public void LaunchFadeOutMainScene()
        {
            StartCoroutine(FadeOutMainScene());
        }

        /// <summary>
        /// Performs the fade-out effect and then loads the "MainScene".
        /// </summary>
        /// <returns>Coroutine for delaying the scene load after fade-out.</returns>
        private IEnumerator FadeOutLobby()
        {
            GetComponent<Image>().DOFade(1, _timeToFadeOut);

            yield return new WaitForSeconds(_timeToFadeOut);

            SceneManager.LoadScene("MainScene");
        }

        private IEnumerator FadeOutMainScene()
        {
            GetComponent<Image>().DOFade(1, _timeToFadeOut);

            yield return new WaitForSeconds(_timeToFadeOut);

            SceneManager.LoadScene("MainMenu");
        }
        #endregion
    }
}