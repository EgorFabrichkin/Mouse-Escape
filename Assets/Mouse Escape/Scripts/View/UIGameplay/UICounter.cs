using R3;
using TMPro;
using UnityEngine;

namespace Mouse_Escape.Scripts.View.UIGameplay
{
    public class UICounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        
        public void CounterView(ReadOnlyReactiveProperty<int> count) => count.Subscribe(RenderCounter);
        
        private void RenderCounter(int value)
        {
            text.text = $"{value}";
        }
    }
}