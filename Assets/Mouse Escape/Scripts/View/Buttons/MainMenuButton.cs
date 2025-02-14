using Mouse_Escape.Scripts.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mouse_Escape.Scripts.View.Buttons
{
    public class MainMenuButton : MonoBehaviour
    {
        public UnityEvent<string> onClick = new();

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => onClick.Invoke(ScenesName.MENU));
        }
    }
}