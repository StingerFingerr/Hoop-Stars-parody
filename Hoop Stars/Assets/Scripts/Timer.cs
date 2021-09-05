using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField,Range(10,120)] private int _timerValue;
    [SerializeField] private TextMeshProUGUI _timerText;
    private GameManager gameManager;
    [SerializeField] private GameObject _countDownHolder;

    private int minutes;
    private int seconds;
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    void Start()
    {
        minutes = (int)_timerValue / 60;
        seconds = _timerValue - minutes * 60;

        var secondLength = seconds.ToString().Length;
        if (secondLength == 1)
            _timerText.text = "0" + minutes.ToString() + ":0" + seconds.ToString();
        else
            _timerText.text = "0" + minutes.ToString() + ":" + seconds.ToString();
    }

    public IEnumerator StartCountDown()
    {
        var seconds = _countDownHolder.transform.childCount;
        var holderIndex = 0;
        if (seconds == 0)
            yield break;

        while (holderIndex <= seconds-1)
        {
            _countDownHolder.transform.GetChild(holderIndex).gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            _countDownHolder.transform.GetChild(holderIndex).gameObject.SetActive(false);
            holderIndex++;
        }
    }
    public IEnumerator StartTimer()
    {
        

        while (true)
        {
            seconds--;
            if (seconds <= 0)
            {
                if (minutes <= 0)
                    break;
                else
                    minutes--;
                seconds = 59;
            }
            var secondLength = seconds.ToString().Length;
            if (secondLength==1)
                _timerText.text = "0" + minutes.ToString() + ":0" + seconds.ToString();
            else
                _timerText.text = "0" + minutes.ToString() + ":" + seconds.ToString();

            yield return new WaitForSeconds(1f);
        }
        _timerText.text = "00:00";
        gameManager.EndOfGame();
    }
}
