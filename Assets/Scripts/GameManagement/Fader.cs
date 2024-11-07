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
    
        [SerializeField] private float _timeToFade;

        #endregion

        #region Methods
    
        private void Start()
        {
            GetComponent<Image>().DOFade(0, _timeToFade);
        }

        /// <summary>
        /// Triggers the fade-out effect and loads the scene after the fade completes.
        /// </summary>
        public void FadeOut()
        {
            StartCoroutine(Fade());
        }

        /// <summary>
        /// Performs the fade-out effect and then loads the "MainScene".
        /// </summary>
        /// <returns>Coroutine for delaying the scene load after fade-out.</returns>
        private IEnumerator Fade()
        {
            GetComponent<Image>().DOFade(1, _timeToFade);

            yield return new WaitForSeconds(_timeToFade);

            SceneManager.LoadScene("MainScene");
        }

        #endregion
    }
}