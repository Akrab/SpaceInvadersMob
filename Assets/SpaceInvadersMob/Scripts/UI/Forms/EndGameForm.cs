using SpaceInvadersMob.Extensions.UI;
using SpaceInvadersMob.Infrastructure;
using UniRx;
using UnityEngine;

namespace SpaceInvadersMob.UI
{
    public class EndGameForm : BaseForm<EndGameForm>
    {
        [SerializeField] private ButtonExt _btnRestart;

        protected override void setup()
        {
            _btnRestart.OnClickAsObservable().Subscribe(_ =>
            {
                Hide();
                MessageBroker.Default.Publish(MessageRestartGame.Create());

            }).AddTo(this);

        }
    }
}
