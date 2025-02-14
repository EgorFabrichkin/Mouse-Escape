using System.Collections;
using Mouse_Escape.Scripts.Game.GameRoot.SceneParams;
using Mouse_Escape.Scripts.Utils;
using Mouse_Escape.Scripts.View;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mouse_Escape.Scripts.Game.GameRoot
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        private readonly Coroutines _coroutines;
        private readonly UIRootView _uiRoot;

        private const float DELAY = 1f;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            _instance = new GameEntryPoint();
            _instance.StartGame();
        }

        private GameEntryPoint()
        {
            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);

            var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
            _uiRoot = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRoot.gameObject);
        }
        
        private void StartGame()
        {
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            switch (sceneName)
            {
                case ScenesName.MENU:
                {
                    var enterParams = new MainMenuEnterParams(ScenesName.MENU, "01-Level");
                    _coroutines.StartCoroutine(LoadAndStartMainMenu(enterParams));
                    return;
                }
                case ScenesName.GAME:
                {
                    var enterParams = new GameplayEnterParams(ScenesName.GAME,"08-Level");
                    _coroutines.StartCoroutine(LoadAndStartGameplay(enterParams)); 
                    return;
                }
            }

            if (sceneName != ScenesName.BOOT)
            {
                return;
            }
#endif
            
            _coroutines.StartCoroutine(LoadAndStartMainMenu());
        }
        
        private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams enterParams = null)
        {
            _uiRoot.ShowLoadingScreen();

            yield return LoadScene(ScenesName.BOOT);
            yield return LoadScene(ScenesName.MENU);

            yield return new WaitForSeconds(DELAY);
            
            var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();

            sceneEntryPoint.Run(_uiRoot, enterParams).Subscribe(mainMenuExitParams =>
            {
                _coroutines.StartCoroutine(LoadAndStartGameplay(mainMenuExitParams.GameplayEnterParams));
            });

            _uiRoot.HideLoadingScreen();
        }
        
        private IEnumerator LoadAndStartGameplay(GameplayEnterParams enterParams = null)
        {
            _uiRoot.ShowLoadingScreen();

            yield return LoadScene(ScenesName.BOOT);
            yield return LoadScene(ScenesName.GAME);

            yield return new WaitForSeconds(DELAY);
            
            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();

            sceneEntryPoint.Run(_uiRoot, enterParams).Subscribe(gameplayExitParams =>
            {
                var targetSceneName = gameplayExitParams.TargetSceneEnterParams.SceneName;

                switch (targetSceneName)
                {
                    case ScenesName.MENU:
                        _coroutines.StartCoroutine(LoadAndStartMainMenu(gameplayExitParams.TargetSceneEnterParams.As<MainMenuEnterParams>()));
                        break;
                    case ScenesName.GAME:
                        _coroutines.StartCoroutine(
                            LoadAndStartGameplay(gameplayExitParams.TargetSceneEnterParams.As<GameplayEnterParams>()));
                        break;
                }
            });

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}