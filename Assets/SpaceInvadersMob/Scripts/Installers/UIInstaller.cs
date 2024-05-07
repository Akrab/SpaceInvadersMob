using SpaceInvadersMob.Infrastructure.Containers;
using SpaceInvadersMob.UI;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.Installers
{
    public class UIInstaller : MonoInstaller<UIInstaller>
    {
        [Inject] private UIContainer _uiContainer;
        [SerializeField] private CanvasRoot _canvasRoot;
        public override void InstallBindings()
        {
            _canvasRoot.Init();
            
            foreach (var item in _canvasRoot.Forms)
            {
                _uiContainer.AddForm(item);
                Container.BindInstance(item);
            }

        }
    }
}
