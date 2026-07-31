using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PlayerControls _p1Controller;
    [SerializeField] private PlayerControls _p2Controller;
    private int _p1Score;
    private int _p2Score;

    [SerializeField] private Transform _p1ScoreTracker;
    [SerializeField] private Transform _p2ScoreTracker;
    [SerializeField] protected GameObject _scoreMark;
    private int _placementOffset = 100;

    [SerializeField] private TMP_Text _roundText;
    [SerializeField] private TMP_Text _countdownText;
    [SerializeField] private RectTransform _countdownPanel;
    [SerializeField] private Vector2 _countdownStartPos;
    [SerializeField] private Vector2 _countdownEndPos;
    [SerializeField] private float _countdownTransitionTime;
    [SerializeField] private AudioSource _countdownAudioSource;
    [SerializeField] private AudioSource _startAudioSource;
    private int _roundNumber;
    private int _roundStartTime = 3;

    [SerializeField] private RectTransform _hitPanel;
    [SerializeField] private float _hitPaneltransitionDuration;
    [SerializeField] private Vector2 _leftPosLow;
    [SerializeField] private Vector2 _leftPosHigh;
    [SerializeField] private Vector2 _rightPosHigh;
    [SerializeField] private Vector2 _rightPosLow;
    private float _panelRotation = 30f;

    [SerializeField] private GameObject _winPanel;
    [SerializeField] private Winner _winGame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _p1Score = 0;
        _p2Score = 0;
        _roundNumber = 1;
        StartCoroutine(RoundStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(PlayerControls hitPlayer)
    {
        _p1Controller.PauseAnim();
        _p2Controller.PauseAnim();
        _p1Controller.StopAllCoroutines();
        _p2Controller.StopAllCoroutines();
        _p1Controller._actionable = false;
        _p2Controller._actionable = false;
        if (hitPlayer == _p1Controller)
        {
            _p2Score++;

            Vector2 pos = new Vector2(_p2ScoreTracker.position.x + (_placementOffset * _p2Score), _p2ScoreTracker.position.y);
            var marker = Instantiate(_scoreMark, _p2ScoreTracker);
            marker.transform.position = pos;

            _hitPanel.rotation = Quaternion.Euler(0, 0, _panelRotation);
            _hitPanel.anchoredPosition = _leftPosLow;
            _hitPanel.DOAnchorPos(Vector2.zero, _hitPaneltransitionDuration).SetEase(Ease.InQuad).OnComplete(() =>
            {
                _hitPanel.DOAnchorPos(_rightPosHigh, _hitPaneltransitionDuration).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    _p1Controller.ResumeAnim();
                    _p1Controller.StopAllAnim();
                    _p2Controller.ResumeAnim();
                    _p2Controller.StopAllAnim();
                    NextRound();
                });
            });
        }
        else if (hitPlayer == _p2Controller)
        {
            _p1Score++;

            Vector2 pos = new Vector2(_p1ScoreTracker.position.x - (_placementOffset * _p1Score), _p1ScoreTracker.position.y);
            var marker = Instantiate(_scoreMark, _p1ScoreTracker);
            marker.transform.position = pos;

            _hitPanel.rotation = Quaternion.Euler(0, 0, -_panelRotation);
            _hitPanel.anchoredPosition = _rightPosLow;
            _hitPanel.DOAnchorPos(Vector2.zero, _hitPaneltransitionDuration).SetEase(Ease.InQuad).OnComplete(() =>
            {
                _hitPanel.DOAnchorPos(_leftPosHigh, _hitPaneltransitionDuration).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    _p1Controller.ResumeAnim();
                    _p1Controller.StopAllAnim();
                    _p2Controller.ResumeAnim();
                    _p2Controller.StopAllAnim();
                    NextRound();
                });
            });
        }

    }

    public void NextRound()
    {
        if (_p1Score >= 5)
        {
            _winPanel.SetActive(true);
            _winGame.WinGame(1);
            return;
        }
        else if ( _p2Score >= 5)
        {
            _winPanel.SetActive(true);
            _winGame.WinGame(2);
            return;
        }
        _roundNumber++;
        _roundText.text = _roundNumber.ToString();
        _p1Controller.ReturnToSpawn();
        _p2Controller.ReturnToSpawn();
        StartCoroutine(RoundStart());
    }

    public IEnumerator RoundStart()
    {
        _countdownPanel.DOAnchorPos(_countdownEndPos, _countdownTransitionTime).SetEase(Ease.OutBounce);
        for (var i = _roundStartTime; i > 0; i--)
        {
            _countdownText.text = "Round starts in " + i;
            _countdownAudioSource.Play();
            yield return new WaitForSeconds(1);

        }

        _startAudioSource.Play();
        _countdownText.text = "FIGHT";
        _countdownPanel.DOAnchorPos(_countdownStartPos, _countdownTransitionTime).SetEase(Ease.InQuad);


        _p1Controller._actionable = true;
        _p2Controller._actionable = true;
    }

}
