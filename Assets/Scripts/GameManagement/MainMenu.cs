using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Properties

    [SerializeField] private GameObject _transitionObject;

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
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneName);
    }

    #endregion
}