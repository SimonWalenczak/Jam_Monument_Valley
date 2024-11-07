using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Menu
{
    /// <summary>
    /// Manages the background fade effect in the lobby scene.
    /// </summary>
    public class LobbyBackground : MonoBehaviour
    {
        #region Properties
    
        [SerializeField] private Image _background;
        [SerializeField] private float _timer;
    
        #endregion
    
        #region Methods
    
        private void Start()
        {
            StartCoroutine(Fade());
        }

        /// <summary>
        /// Fades the background in and out in a loop.
        /// </summary>
        /// <returns>An enumerator to handle the fading sequence.</returns>
        IEnumerator Fade()
        {
            _background.DOFade(0.1f, _timer);
            yield return new WaitForSeconds(_timer);
            _background.DOFade(0.3f, _timer);
            yield return new WaitForSeconds(_timer);

            StartCoroutine(Fade());
        }
    
        #endregion
    }
}
