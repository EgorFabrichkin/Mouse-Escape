using System.Collections;
using Mouse_Escape.Scripts.Game.Gameplay.Players;
using Mouse_Escape.Scripts.Game.GameRoot.Levels;
using Mouse_Escape.Scripts.Game.GameRoot.SceneParams;
using Mouse_Escape.Scripts.Utils;
using Mouse_Escape.Scripts.View;
using Mouse_Escape.Scripts.View.UISceneRoot;
using R3;
using UnityEngine;

namespace Mouse_Escape.Scripts.Game.GameRoot
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIGameplayRoot sceneUIRootPrefab;
        [SerializeField] private CatHandActivator catHand;
        
        private GameplayExitParams _gameplayExitParams;
        
        private bool _isFinished = false;
        
        public Observable<GameplayExitParams> Run(UIRootView uiRoot, GameplayEnterParams enterParams)
        {
            var uiScene = Instantiate(sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            var levelPrefab = Resources.Load<Level>($"Levels/{enterParams.LevelID}");
            var level = Instantiate(levelPrefab);

            var player = level.GetPlayer();
            var counter = new Counter(player, level.LevelCounter);

            switch (enterParams.LevelID)
            {
                case "01-Level":
                    uiScene.ShowInputTutorial();
                    break;
                case "02-Level":
                    uiScene.ShowCounterTutorial();
                    break;
                case "08-Level":
                    uiScene.ShowWaterTutorial();
                    break;
            }

            player.onFinish.AddListener(() =>
            {
                _isFinished = true;
                uiScene.ShowWinPopup();
            });
            
            player.onFail.AddListener(() => StartCoroutine(uiScene.ShowLosePopup()));
            player.onWater.AddListener(() => counter.Debaf());
            
            counter.Count.Subscribe(count =>
            {
                if (count <= 0)
                {
                    uiScene.ControlDisable();
                    StartCoroutine(Delay());
                }
            });
            
            IEnumerator Delay()
            {
                yield return new WaitForSeconds(0.5f);

                if (_isFinished)
                {
                    yield break;
                }
                else
                {
                    catHand.onActivate.Invoke(level.GetMainTilemap(), player.GetCurrentPosition());
                    uiScene.StartCoroutine(uiScene.ShowLosePopup());
                }
            }
            
            uiScene.SubscribeRenderCounter(counter.Count);
            uiScene.onSelectDirection.AddListener(player.onMove.Invoke);
            
            var exitSceneSignalSubj = new Subject<string>();
            uiScene.Bind(exitSceneSignalSubj);

            exitSceneSignalSubj.Subscribe(sceneName =>
            {
                switch (sceneName)
                {
                    case ScenesName.MENU:
                    {
                        var mainMenuEnterParams = new MainMenuEnterParams(ScenesName.MENU, enterParams.LevelID);
                        _gameplayExitParams = new GameplayExitParams(mainMenuEnterParams);
                        break;
                    }
                    case ScenesName.GAME:
                    {
                        var gameplayEnterParams = new GameplayEnterParams(ScenesName.GAME, enterParams.LevelID);        
                        _gameplayExitParams = new GameplayExitParams(gameplayEnterParams);
                        break;
                    }
                    case "NextLevel":
                    {
                        var gameplayEnterParams = new GameplayEnterParams(ScenesName.GAME, GenerateNextLevelID(enterParams.LevelID));        
                        _gameplayExitParams = new GameplayExitParams(gameplayEnterParams);
                        break;
                    }
                }
            });
            
            var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => _gameplayExitParams);

            return exitToMainMenuSceneSignal;
        }
        
        private string GenerateNextLevelID(string levelID)
        {
            var number = levelID.Remove(2);
            var postfix = levelID.Substring(3);
            
            if (int.TryParse(number, out var value))
            {
                value++;
            }
            
            var result = value >= 10 ? $"{value}-{postfix}" : $"0{value}-{postfix}";
            
            return result;
        }
    }
}