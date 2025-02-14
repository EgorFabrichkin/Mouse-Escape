using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Mouse_Escape.Scripts.Game.Gameplay
{
    public class CatHand : MonoBehaviour
    {
        public UnityEvent onActivate = new ();

        [SerializeField] private Transform targetPosition;
        [SerializeField] private float duration;
        
        private void Awake()
        {
            onActivate.AddListener(Activate);
        }

        private void Activate()
        {
            transform.DOMove(targetPosition.position, duration);
        }
    }
}