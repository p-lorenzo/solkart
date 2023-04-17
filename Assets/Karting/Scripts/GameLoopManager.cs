using System.Collections;
using KartGame.KartSystems;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace KartGame
{
    public enum GameState{Play, End}
    public class GameLoopManager : MonoBehaviour
    {
        [Header("Parameters")]
        [Tooltip("Duration of the fade-to-black at the end of the game")]
        public float endSceneLoadDelay = 3f;
        [Tooltip("The canvas group of the fade-to-black screen")]
        public CanvasGroup endGameFadeCanvasGroup;
        
        [Header("End")]
        [Tooltip("This string has to be the name of the scene you want to load when game ends")]
        public string endSceneName = "EndScene";
        [Tooltip("Duration of delay before the fade-to-black")]
        public float delayBeforeFadeToBlack = 4f;
        [Tooltip("Duration of delay before the end message")]
        public float delayBeforeEndMessage = 2f;
        [Tooltip("Sound played on end")]
        public AudioClip endSound;
        [Tooltip("Prefab for the end game message")]
        public DisplayMessage endDisplayMessage;

        public PlayableDirector raceCountdownTrigger;
        
        public GameState gameState { get; private set; }
        
        public ArcadeKart playerKart;
        
        TimeManager m_TimeManager;
        float m_TimeLoadEndGameScene;
        string m_SceneToLoad;
        float elapsedTimeBeforeEndScene = 0;

        private void Start()
        {
            AudioUtility.SetMasterVolume(1);
            endDisplayMessage.gameObject.SetActive(false);
            
            m_TimeManager = FindObjectOfType<TimeManager>();
            DebugUtility.HandleErrorIfNullFindObject<TimeManager, GameLoopManager>(m_TimeManager, this);
            
            m_TimeManager.StopRace();
            playerKart.SetCanMove(false);
            ShowRaceCountdownAnimation();
            StartCoroutine(CountdownThenStartRaceRoutine());
        }
        
        void ShowRaceCountdownAnimation() {
            raceCountdownTrigger.Play();
        }
        
        IEnumerator CountdownThenStartRaceRoutine() {
            yield return new WaitForSeconds(3f);
            StartRace();
        }
        
        void StartRace() {
            playerKart.SetCanMove(true);
            m_TimeManager.StartRace();
        }
        
        void Update()
        {

            if (gameState != GameState.Play)
            {
                elapsedTimeBeforeEndScene += Time.deltaTime;
                if(elapsedTimeBeforeEndScene >= endSceneLoadDelay)
                {

                    float timeRatio = 1 - (m_TimeLoadEndGameScene - Time.time) / endSceneLoadDelay;
                    endGameFadeCanvasGroup.alpha = timeRatio;

                    float volumeRatio = Mathf.Abs(timeRatio);
                    float volume = Mathf.Clamp(1 - volumeRatio, 0, 1);
                    AudioUtility.SetMasterVolume(volume);

                    // See if it's time to load the end scene (after the delay)
                    if (Time.time >= m_TimeLoadEndGameScene)
                    {
                        SceneManager.LoadScene(m_SceneToLoad);
                        gameState = GameState.Play;
                    }
                }
            }
            else
            {
                if (m_TimeManager.IsFinite && m_TimeManager.IsOver)
                    EndGame();
            }
        }
        
        void EndGame()
        {
            // unlocks the cursor before leaving the scene, to be able to click buttons
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            m_TimeManager.StopRace();

            // Remember that we need to load the appropriate end scene after a delay
            gameState = GameState.End;
            endGameFadeCanvasGroup.gameObject.SetActive(true);

            m_SceneToLoad = endSceneName;
            m_TimeLoadEndGameScene = Time.time + endSceneLoadDelay + delayBeforeFadeToBlack;

            // play a sound on win
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = endSound;
            audioSource.playOnAwake = false;
            audioSource.outputAudioMixerGroup = AudioUtility.GetAudioGroup(AudioUtility.AudioGroups.HUDVictory);
            audioSource.PlayScheduled(AudioSettings.dspTime + delayBeforeEndMessage);

            // create a game message
            endDisplayMessage.delayBeforeShowing = delayBeforeEndMessage;
            endDisplayMessage.gameObject.SetActive(true);
           
        }
    }
}