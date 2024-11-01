using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

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