using SpaceInvadersMob.Extensions.UI;
using SpaceInvadersMob.Infrastructure;
using UniRx;
using UnityEngine;

namespace SpaceInvadersMob.UI
{
    public class PauseForm: BaseForm<PauseForm>
    {
        [SerializeField] private ButtonExt _btnRestart;
        [SerializeField] private ButtonExt _btnClose;

        protected override void setup()
        {
            
            _btnClose.OnClickAsObservable().Subscribe(_ =>
            {
                MessageBroker.Default.Publish( MessageOnPauseGame.Create(false));
                
            }).AddTo(this);

            _btnRestart.OnClickAsObservable().Subscribe(_ =>
            {
                Hide();
                MessageBroker.Default.Publish( MessageRestartGame.Create());
                
            }).AddTo(this);
        }
    }
}
