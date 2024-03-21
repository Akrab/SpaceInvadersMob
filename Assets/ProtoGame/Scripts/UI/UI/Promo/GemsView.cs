using DG.Tweening;
using ProtoGame.UI.Promo;
using TMPro;
using UnityEngine;

namespace ProtoGame.UI
{
    public class GemsView : MonoBehaviour, IGemsViewProvider
    {
        private const float DURATION = 0.25f;

        [SerializeField] private TextMeshProUGUI _valueTMP;

        private Tween _tween;

        private int _currentValue = 0;

        public void SetData(int value)
        {
            _valueTMP.text = value.ToString();
            _currentValue = value;
        }

        public void ReduceCurrency(int value)
        {
            if (_tween != null)
                _tween.Kill();

            SetData(_currentValue);

            int from = _currentValue;
            int to = _currentValue - value;
            _tween = DOTween.To(() => from, V => _valueTMP.text = V.ToString(), to, DURATION);
            _currentValue -= value;
        }
    }
}