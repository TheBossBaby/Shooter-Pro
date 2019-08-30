using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Serilalizable Private Field
        [SerializeField] private Text _scoreText;
        [SerializeField] private GameObject _gameOverText;
        [SerializeField] private GameObject _reloadLevelText;
        [SerializeField] private Image _lifeImage;
        [SerializeField] private Sprite[] _liveSprites;
    #endregion

    #region Private Field
        
    #endregion

    #region Public Field
        
    #endregion

    #region MonoBehaviour Callbacks
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }            
    #endregion

    #region Public Methods
        public void UpdateScore(int socre)
        {
            _scoreText.text = "Score: " + socre.ToString();
        }
        public void UpdateLife(int numberOfLivesLeft)
        {
            _lifeImage.sprite = _liveSprites[numberOfLivesLeft];
            if(numberOfLivesLeft == 0)
            {
                GameoverSequence();
            }
        }
    #endregion

    #region Private Methods
        private void GameoverSequence()
        {
            _gameOverText.SetActive(true);
            _reloadLevelText.SetActive(true);
            GameManager _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
            _gameManager.GameOver();
        }
    #endregion
}