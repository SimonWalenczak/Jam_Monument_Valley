using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    /// <summary>
    /// Handles the main menu functionality, including scene transitions, character animations, and quitting the application.
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        #region Properties

        [SerializeField] private GameObject _transitionObject;
        [SerializeField] private float _timer;

        [SerializeField] private GameObject _charaToMove;
    
        #endregion

        #region Methods

        public void ValidPreset()
        {
            StartCoroutine(Transition("Lobby"));
        }

        public void GoToCredits()
        {
            StartCoroutine(Transition("Credits"));
        }

        /// <summary>
        /// Quits the application or stops playing the scene based on the platform (standalone or editor).
        /// </summary>
        public void Quit()
        {
            //If we are running in a standalone build of the game
#if UNITY_STANDALONE
            //Quit the application
            Application.Quit();
#endif

            //If we are running in the editor
#if UNITY_EDITOR
            //Stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    
        /// <summary>
        /// Handles the scene transition with a character animation and fade effect.
        /// </summary>
        /// <param name="SceneName">The name of the scene to load after the transition.</param>
        IEnumerator Transition(string SceneName)
        {
            _transitionObject.GetComponent<Animator>().SetBool("isActive", true);
            yield return new WaitForSeconds(3f);
            _charaToMove.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            MusicPlayer.Instance.AudioSource.Play();
            _charaToMove.transform.DOMoveX(15.5f, _timer).SetEase(Ease.Linear);
            yield return new WaitForSeconds(_timer);
            SceneManager.LoadScene(SceneName);
        }

        #endregion
    }
}