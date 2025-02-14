using R3;

namespace Mouse_Escape.Scripts.Game.Gameplay.Players
{
    public class Counter
    {
        private readonly Player _player;
        private readonly ReactiveProperty<int> _count = new ();
        public ReadOnlyReactiveProperty<int> Count => _count;

        public Counter(Player player, int count)
        {
            _player = player;
            _count.Value = count;
            
            ChangeCount();
        }

        private void ChangeCount()
        {
            _player.onCounter.AddListener(() =>
            {
                _count.Value--;
                
                if (_count.Value < 0)
                {
                    _count.Value = 0;
                }
            });
        }

        public void Debaf()
        {
            _count.Value = _count.Value - 2;
            
            if (_count.Value <= 0)
            {
                _count.Value = 0;
            }
        }
    }
}