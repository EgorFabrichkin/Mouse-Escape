using Mouse_Escape.Scripts.Game.GameRoot.SceneParams;
using Mouse_Escape.Scripts.Utils;
using Mouse_Escape.Scripts.View;
using Mouse_Escape.Scripts.View.UISceneRoot;
using R3;
using UnityEngine;

namespace Mouse_Escape.Scripts.Game.GameRoot
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIMainMenuRoot sceneUIRootPrefab;
        
        private MainMenuExitParams _mainMenuExitParams;
        
        public Observable<MainMenuExitParams> Run(UIRootView uiRoot, MainMenuEnterParams enterParams)
        {
            var uiScene = Instantiate(sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiScene.gameObject);
            
            var exitSignalSubj = new Subject<string>();
            uiScene.Bind(exitSignalSubj);
            
            exitSignalSubj.Subscribe(levelID =>
            {
                var gameplayEnterParams = new GameplayEnterParams(ScenesName.GAME, levelID);
                _mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
            });
            
            var exitToGameplaySceneSignal = exitSignalSubj.Select(_ => _mainMenuExitParams);
             
            return exitToGameplaySceneSignal;
        }
    }
}