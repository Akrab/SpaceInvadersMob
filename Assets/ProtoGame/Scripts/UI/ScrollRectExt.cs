using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProtoGame.Extensions.UI
{
    public class ScrollRectExt : ScrollRect
    {
        [SerializeField] private ScrollRect _parent;
        private bool _routeToParent = false;

        public ScrollRect ParentScroll
        {
            get { return _parent; }
            set { _parent = value; }
        }

        private void DoForParent<T>(Action<T> action) where T : IEventSystemHandler
        {
            if (_parent is T) action((T)(IEventSystemHandler)_parent);
        }

        public override void OnInitializePotentialDrag(PointerEventData eventData)
        {
            DoForParent<IInitializePotentialDragHandler>((parent) => { parent.OnInitializePotentialDrag(eventData); });
            base.OnInitializePotentialDrag(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (_routeToParent)
                DoForParent<IDragHandler>((parent) => { parent.OnDrag(eventData); });
            else
                base.OnDrag(eventData);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (!horizontal && Math.Abs(eventData.delta.x) > Math.Abs(eventData.delta.y))
                _routeToParent = true;
            else if (!vertical && Math.Abs(eventData.delta.x) < Math.Abs(eventData.delta.y))
                _routeToParent = true;
            else
                _routeToParent = false;

            if (_routeToParent)
                DoForParent<IBeginDragHandler>((parent) => { parent.OnBeginDrag(eventData); });
            else
                base.OnBeginDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if (_routeToParent)
                DoForParent<IEndDragHandler>((parent) => { parent.OnEndDrag(eventData); });
            else
                base.OnEndDrag(eventData);
            _routeToParent = false;
        }
    }
}