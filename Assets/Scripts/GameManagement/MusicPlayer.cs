using UnityEngine;

namespace GameManagement
{
    /// <summary>
    /// Manages the audio playback for the game, ensuring that the music persists across scene transitions.
    /// </summary>
    public class MusicPlayer : MonoBehaviour
    {
        #region Properties
        
        public static MusicPlayer Instance;
        public AudioSource AudioSource {get; private set;}
    
        #endregion
        
        #region Maethods
        
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            AudioSource = GetComponent<AudioSource>();
        }
        
        #endregion
    }
}