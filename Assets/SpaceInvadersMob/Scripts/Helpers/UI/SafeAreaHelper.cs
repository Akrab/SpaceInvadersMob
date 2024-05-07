using UnityEngine;

namespace SpaceInvadersMob.Helpers.UIHelpers
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaHelper : MonoBehaviour
    {
        [SerializeField] private bool _applySafeAreaX;
        [SerializeField] private bool _applySafeAreaY;

        private RectTransform _rectTransform;
        private Rect _lastSafeArea;
        private int _lastScreenWidth;
        private int _lastScreenHeight;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            RecalculateSafeArea();
        }

        private void RecalculateSafeArea()
        {
            Rect safeArea = Screen.safeArea;

            if (safeArea == _lastSafeArea && Screen.width == _lastScreenWidth && Screen.height == _lastScreenHeight)
            {
                return;
            }

            _lastSafeArea = safeArea;
            _lastScreenWidth = Screen.width;
            _lastScreenHeight = Screen.height;

            ApplySafeArea(safeArea);
        }

        private void ApplySafeArea(Rect safeArea)
        {
            if (!_applySafeAreaX)
            {
                safeArea.x = 0;
                safeArea.width = Screen.width;
            }

            if (!_applySafeAreaY)
            {
                safeArea.y = 0;
                safeArea.height = Screen.height;
            }

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            _rectTransform.anchorMin = anchorMin;
            _rectTransform.anchorMax = anchorMax;
        }
    }
}

