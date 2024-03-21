using UnityEngine;

namespace ProtoGame.UI
{
    public class CanvasRoot : MonoBehaviour
    {
        public IForm[] Forms { get; private set; }


        public void Init()
        {
            Forms = GetComponentsInChildren<IForm>();

            foreach (var form in Forms)
            {
                IFormSetSize currentForm = form as IFormSetSize;
                form.Disable();
            }


        }
    }
}
