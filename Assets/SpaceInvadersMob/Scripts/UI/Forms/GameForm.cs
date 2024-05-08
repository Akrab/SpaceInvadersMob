using DG.Tweening;
using SpaceInvadersMob.Extensions.UI;
using SpaceInvadersMob.Game.Actors.Enemy;
using SpaceInvadersMob.Infrastructure;
using SpaceInvadersMob.Infrastructure.Controllers;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMob.UI
{
    public class GameForm : BaseForm<GameForm>
    {
        [SerializeField] private ButtonExt _bntMenu;
        [SerializeField] private TextMeshProUGUI _redEnemyCount;
        [SerializeField] private TextMeshProUGUI _baseEnemyCount;

        [Inject] private IGameRuntimeController _gameRuntimeController;
        private CompositeDisposable disposables;
        
        
        protected override void setup()
        {
            _baseEnemyCount.text = _redEnemyCount.text = "x0";

            _bntMenu.OnClickAsObservable().Subscribe(_ =>
            {
                MessageBroker.Default.Publish( MessageOnPauseGame.Create(true));
            }).AddTo(this);
        }
        

        public override Tween Show(bool instance = false)
        {
            if (disposables != null)
                disposables.Dispose();
            disposables = new CompositeDisposable();

            _gameRuntimeController.KillCounter.ObserveReplace()
                .Subscribe(kc =>
                {
                    if (kc.Key == EnemyType.Base)    _baseEnemyCount.text = $"x{kc.NewValue}";
                    if (kc.Key == EnemyType.Red)    _redEnemyCount.text = $"x{kc.NewValue}";
  
                }).AddTo(this);

            return base.Show(instance);
        }

        public override Tween Hide(bool instance = false)
        {
            if (disposables != null)
                disposables.Dispose();

            return base.Hide(instance);
        }

        
    }
}
