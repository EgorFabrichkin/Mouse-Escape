using UnityEngine;
using UnityEngine.UI;

namespace Mouse_Escape.Scripts.View.Popups
{
    public class CounterTutorial : UIPopupBase
    {
        [SerializeField] private Button button;
        
        private void Awake()
        {
            AppearDisappearPopup(false);
            button.onClick.AddListener(() => AppearDisappearPopup(false));
        }
    }
}