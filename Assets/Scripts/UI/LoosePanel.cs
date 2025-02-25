using UnityEngine;

namespace LearnGame
{
    public class LoosePanel : MonoBehaviour
    {

        [SerializeField]
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager.Loss += ShowPanel;
            gameObject.SetActive(false);
        }

       private void ShowPanel()
        {
            gameObject.SetActive(true);
        }

    }
}