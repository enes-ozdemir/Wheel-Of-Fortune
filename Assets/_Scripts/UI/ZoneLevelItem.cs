using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ZoneLevelItem : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image borderImage;
        [SerializeField] private TextMeshProUGUI levelText;

        public void InitLevel(int level, Sprite sprite)
        {
            levelText.text = level.ToString();
            backgroundImage.sprite = sprite;
        }

        public void AddBorderImage() => borderImage.gameObject.SetActive(true);
        public void RemoveBorderImage() => borderImage.gameObject.SetActive(false);
    }
}