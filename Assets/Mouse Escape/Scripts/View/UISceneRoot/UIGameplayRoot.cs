using System;
using System.Collections;
using Mouse_Escape.Scripts.Utils;
using Mouse_Escape.Scripts.View.Buttons;
using Mouse_Escape.Scripts.View.Popups;
using Mouse_Escape.Scripts.View.UIGameplay;
using R3;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Mouse_Escape.Scripts.View.UISceneRoot
{
    public class UIGameplayRoot : MonoBehaviour
    {
        public UnityEvent<Vector2Int> onSelectDirection = new();

        [Header("Popups")] 
        [SerializeField] private WinPopup winPopup;
        [SerializeField] private LosePopup losePopup;
        [SerializeField] private InputTutorial inputTutorial;
        [SerializeField] private CounterTutorial counterTutorial;
        [SerializeField] private WaterTutorial waterTutorial;
        [Header("UI Buttons")]
        [SerializeField] private UICounter counter;
        [SerializeField] private RestartButton[] restartButtons;
        [SerializeField] private MainMenuButton[] mainMenuButtons;
        [SerializeField] private NextLevelButton nextLevelButton;
        [Header("Control Buttons")]
        [SerializeField] private Button buttonUp;
        [SerializeField] private Button buttonDown;
        [SerializeField] private Button buttonLeft;
        [SerializeField] private Button buttonRight;
        
        private void Awake()
        {
            foreach (var button in mainMenuButtons)
            {
                button.onClick.AddListener(sceneName => _exitSceneSignalSubj.OnNext(sceneName));
            }

            foreach (var button in restartButtons)
            {
                button.onClick.AddListener(sceneName => _exitSceneSignalSubj.OnNext(sceneName));
            }
            
            nextLevelButton.onClick.AddListener(scenesName => _exitSceneSignalSubj.OnNext(scenesName));
            
            
            buttonUp.onClick.AddListener(() => onSelectDirection.Invoke(Vector2Int.up));
            buttonDown.onClick.AddListener(() => onSelectDirection.Invoke(Vector2Int.down));
            buttonLeft.onClick.AddListener(() => onSelectDirection.Invoke(Vector2Int.left));
            buttonRight.onClick.AddListener(() => onSelectDirection.Invoke(Vector2Int.right));
        }

        private Subject<string> _exitSceneSignalSubj;
        
        public void Bind(Subject<string> exitSceneSignalSubj) => _exitSceneSignalSubj = exitSceneSignalSubj;

        public void SubscribeRenderCounter(ReadOnlyReactiveProperty<int> value) => counter.CounterView(value);

        public void ShowWinPopup() => winPopup.ShowPopup();
        
        public IEnumerator ShowLosePopup()
        {
            yield return new WaitForSeconds(1.5f);
            
            losePopup.ShowPopup();
        }

        public void ShowInputTutorial() => inputTutorial.ShowPopup();
        
        public void ShowCounterTutorial() => counterTutorial.ShowPopup();
        
        public void ShowWaterTutorial() => waterTutorial.ShowPopup();

        public void ControlDisable()
        {
            buttonUp.gameObject.SetActive(false);
            buttonDown.gameObject.SetActive(false);
            buttonLeft.gameObject.SetActive(false);
            buttonRight.gameObject.SetActive(false);
        }
    }
}