using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Member.KJH.Code.UI
{
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] private List<string> lines;
        [SerializeField] private List<Sprite> images;

        [SerializeField] private Image currentImage;
        [SerializeField] private TextMeshProUGUI currentLine;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button prevButton;
        [SerializeField] private GameObject screening;

        private int _currentIndex;

        private void Awake()
        {
            _currentIndex = 0;

            nextButton.onClick.AddListener(Next);
            prevButton.onClick.AddListener(Prev);

            Refresh();
            screening.SetActive(false);
        }

        public void Open()
        {
            _currentIndex = 0;
            Refresh();
            gameObject.SetActive(true);
            screening.SetActive(true);
        }

        private void Next()
        {
            if (_currentIndex >= lines.Count - 1 || _currentIndex >= images.Count - 1)
            {
                screening.SetActive(false);
                gameObject.SetActive(false);
                return;
            }

            _currentIndex++;
            Refresh();
        }

        private void Prev()
        {
            if (_currentIndex <= 0)
            {
                return;
            }

            _currentIndex--;
            Refresh();
        }

        private void Refresh()
        {
            currentLine.text = lines[_currentIndex];
            currentImage.sprite = images[_currentIndex];

            prevButton.interactable = _currentIndex > 0;
            nextButton.interactable = true;
        }
    }
}