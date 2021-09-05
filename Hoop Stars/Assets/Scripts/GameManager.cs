using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    #region SingleTon
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion

    [SerializeField] private TextMeshProUGUI _playerScoreText;
    [SerializeField] private TextMeshProUGUI _botScoreText;
    private int _playerScore = 0;
    private int _botScore = 0;
    [SerializeField] private int maxGameScore = 7;


    [SerializeField] private GameObject _LcoloredMarkersHolder;
    [SerializeField] private GameObject _RcoloredMarkersHolder;

    [SerializeField] private Animator _scoreBlockAnimator;
    

    [SerializeField] private Timer _timer;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _gameControls;

    [SerializeField] private TextMeshProUGUI _gameOverMessageText;
    [SerializeField] private string _gameWinMessage;
    [SerializeField] private string _gameLoseMessage;
    [SerializeField] private string _gameEqualScoreMessage;

    [SerializeField] private BotAI _botAI;
    [SerializeField] private Rigidbody _playerRB;

    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _botTransform;
    [SerializeField] private Transform _crownTransform;
    private Transform _leaderTransform;
    public bool isGame { get; private set;}

    
    private void Start()
    {
        isGame = false;
        Time.timeScale = 0;

        _mainMenu.SetActive(true);
        _gameOverScreen.SetActive(false);
    }

    public void StartGame()
    {
        //isGame = true;
        Time.timeScale = 1;
        ResetScore();
        _scoreBlockAnimator.SetTrigger("ScoreBlockDropping");

        StartCoroutine(CountDown()); 
    }

    private void Update()
    {
        if (_leaderTransform != null)
            _crownTransform.position = _leaderTransform.position + Vector3.up * .5f;


    }

    IEnumerator CountDown()
    {
        yield return StartCoroutine(_timer.StartCountDown());

        isGame = true;
        _playerRB.isKinematic = true;
        _botAI.StartGame();

        yield return StartCoroutine(_timer.StartTimer());

    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
        //isGame = false;
        //Time.timeScale = 0;
        //_gameOverScreen.SetActive(false);
        //_mainMenu.SetActive(true);
        //_spawnerAnimator.enabled = true;
    }
    public void EndOfGame()
    {
        isGame=false;
        StartCoroutine(GameIsOver());
    }

    IEnumerator GameIsOver()
    {
        float animTime = .5f;
        float step = .1f;
        float timeScaler=1f;
        while (true)
        {
            timeScaler -= step;
            if (timeScaler <= 0)
                break;
            
            Time.timeScale = timeScaler;

            yield return new WaitForSeconds(step * animTime);
        }
        Time.timeScale = 0;

        _gameOverScreen.SetActive(true);
        if (_playerScore > _botScore)
        {
            _gameOverMessageText.text = _gameWinMessage;
        }else if (_playerScore < _botScore)
        {
            _gameOverMessageText.text = _gameLoseMessage;
        }
        else
        {
            _gameOverMessageText.text = _gameEqualScoreMessage;
        }
        _gameControls.gameObject.SetActive(false);
    }

    public void IncreasePlayerScore()
    {
        if (isGame)
        {
            _playerScore++;
            SetLeaderCrown();
            _playerScoreText.text = _playerScore.ToString();
            _LcoloredMarkersHolder.transform.GetChild(_playerScore - 1).gameObject.SetActive(true);
        }
        
        if (_playerScore >= maxGameScore)
        {
            EndOfGame();
            return;
        }
    }
    public void IncreaseBotScore()
    {
        if (isGame)
        {
            _botScore++;
            SetLeaderCrown();
            _botScoreText.text = _botScore.ToString();
            _RcoloredMarkersHolder.transform.GetChild(_botScore - 1).gameObject.SetActive(true);
        }
        
        if (_botScore >= maxGameScore)
        {
            EndOfGame();
            return;
        }
    }
    private void SetLeaderCrown()
    {
        if (_playerScore > _botScore)
        {
            _leaderTransform = _playerTransform;
            _crownTransform.gameObject.SetActive(true);
            _crownTransform.position = _leaderTransform.position + Vector3.up * .5f;
        }else if (_playerScore < _botScore)
        {
            _leaderTransform = _botTransform;
            _crownTransform.gameObject.SetActive(true);
            _crownTransform.position = _leaderTransform.position + Vector3.up * .5f;
        }
        else
        {
            _leaderTransform = null;
            _crownTransform.gameObject.SetActive(false);
        }
    }
    private void ResetScore()
    {
        _playerScore = 0;
        _botScore = 0;
        _playerScoreText.text = _playerScore.ToString();
        _botScoreText.text = _botScore.ToString();

    }
}

