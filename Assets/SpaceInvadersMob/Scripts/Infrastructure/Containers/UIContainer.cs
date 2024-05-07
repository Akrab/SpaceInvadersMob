using System;
using System.Collections.Generic;
using SpaceInvadersMob.UI;

namespace SpaceInvadersMob.Infrastructure.Containers
{
    public class UIContainer
    {
        private readonly Dictionary<Type, IForm> _forms = new Dictionary<Type, IForm>();
        public void AddForm(IForm form)
        {
            if (_forms.ContainsKey(form.GetType()) == false) _forms[form.GetType()] = form;
        }

        public T GetForm<T>() where T : IForm
        {
            _forms.TryGetValue(typeof(T), out var form);
            return (T)form;
        }

        public void RemoveForm<T>()
        {
            _forms.Remove(typeof(T));
        }
    }
}