using UnityEngine;

namespace LearnGame
{
    public class WinPanel : MonoBehaviour
    {

        [SerializeField]
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager.Win += ShowPanel;
            gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            gameObject.SetActive(true);
        }
    }
}