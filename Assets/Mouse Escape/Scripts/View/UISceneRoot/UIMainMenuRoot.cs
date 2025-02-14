using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Mouse_Escape.Scripts.View.UISceneRoot
{
    public class UIMainMenuRoot : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        
        private Subject<string> _exitSceneSignalSubj;

        private void Awake()
        {
            playButton.onClick.AddListener(() => LoadLevel("01-Level"));
        }

        private void LoadLevel(string levelID) => _exitSceneSignalSubj.OnNext(levelID);
        
        public void Bind(Subject<string> exitSceneSignalSubj) => _exitSceneSignalSubj = exitSceneSignalSubj;
    }
}