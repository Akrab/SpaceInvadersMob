﻿using DG.Tweening;
using System.Linq;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMob.UI
{

    public interface IForm
    {
        bool IsShow { get; }
        GameObject gameObject { get; }
        Tween Show(bool instance = false);
        Tween Hide(bool instance = false);
        void Disable();
        void Enable();

    }

    public abstract class BaseForm<T> : MonoBehaviour, IForm where T : MonoBehaviour
    {
        private const float DURATION = 0.25f;

        private bool _isSetup = false;
        [Inject] protected DiContainer _diContainer;
        [SerializeField]
        protected CanvasGroup _canvasGroup;
        [SerializeField]
        protected Canvas _canvas;
        [SerializeField]
        protected RectTransform _rectTransform;
        protected bool _isShow;
        protected Sequence _currentAnimSequence = null;

        public bool IsShow => _isShow;

        private void Awake()
        {
            Setup();
        }


        protected virtual void setup()
        {

        }

        public void Setup()
        {
            if (_isSetup)
                return;

            setup();
            _isSetup = true;
        }

        public virtual Tween Hide(bool instance = false)
        {
            _currentAnimSequence?.Kill(true);
            _canvasGroup.interactable = false;
            _currentAnimSequence = DOTween.Sequence();

            _currentAnimSequence.Append(_canvasGroup.DOFade(0, DURATION).OnComplete(() => {
                Disable();
            }));

            if (instance)
                _currentAnimSequence.Complete();


           
            return _currentAnimSequence;
        }

        public virtual Tween Show(bool instance = false)
        {
            _currentAnimSequence?.Kill(true);

            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0;
            gameObject.SetActive(true);
            _currentAnimSequence = DOTween.Sequence();
            _isShow = true;
            _currentAnimSequence.Append(_canvasGroup.DOFade(1f, DURATION).OnComplete(() =>
            {
                _canvasGroup.interactable = true;
            }));
            if (instance)
                _currentAnimSequence.Complete();


            return _currentAnimSequence;
        }

        public void Disable()
        {
            _canvasGroup.alpha = 0;
            gameObject.SetActive(false);
            _isShow = false;
        }

        public void Enable()
        {
            _canvasGroup.alpha = 1;
            gameObject.SetActive(true);
            _isShow = true;
            _canvasGroup.interactable = true;
        }

    }
}
