using UnityEngine;

namespace PurpleCable
{
    public class TooltipManager : MonoBehaviour
    {
        [SerializeField]
        private Tooltip TooltipPrefab = null;

        private static TooltipManager _instance = null;

        private Tooltip _tooltip = null;

        public static bool IsTooltipAutomatic { get; set; } = true;

        public bool IsScreenSpace = false;

        private Camera Camera;

        private void Start()
        {
            _instance = this;

            Camera = Camera.main;

            _tooltip = Instantiate(TooltipPrefab, transform);
            _tooltip.Hide();
        }

        private void ShowTooltipInternal(string tooltip)
        {
            if (_tooltip == null)
                return;

            if (string.IsNullOrWhiteSpace(tooltip))
                return;

            if (IsScreenSpace)
                _tooltip.transform.position = Input.mousePosition;
            else
                _tooltip.transform.position = (Vector2)Camera.ScreenToWorldPoint(Input.mousePosition);

            if (!_tooltip.gameObject.activeSelf)
                _tooltip.Show(tooltip);
        }

        public static void ShowTooltip(string tooltip)
        {
            _instance?.ShowTooltipInternal(tooltip);
        }

        private void ShowTooltipInternal(string displayName, string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
                ShowTooltip(displayName);
            else
                ShowTooltip(keyWord + " - " + displayName);
        }

        public static void ShowTooltip(string displayName, string keyWord)
        {
            _instance?.ShowTooltipInternal(displayName, keyWord);
        }

        private void HideTooltipInternal()
        {
            if (_tooltip == null)
                return;

            _tooltip.Hide();
        }

        public static void HideTooltip()
        {
            _instance?.HideTooltipInternal();
        }
    }
}
