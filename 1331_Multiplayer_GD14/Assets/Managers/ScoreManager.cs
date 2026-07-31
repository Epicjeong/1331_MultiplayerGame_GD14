using TMPro;
using UnityEngine;
using DG.Tweening;

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
    [SerializeField] private RectTransform _hitPanel;
    [SerializeField] private float _transitionDuration;
    [SerializeField] private Vector2 _leftPosLow;
    [SerializeField] private Vector2 _leftPosHigh;
    [SerializeField] private Vector2 _rightPosHigh;
    [SerializeField] private Vector2 _rightPosLow;
    private float _panelRotation = 30f;
    private int _roundNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _p1Score = 0;
        _p2Score = 0;
        _roundNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(PlayerControls hitPlayer)
    {
        if (hitPlayer == _p1Controller)
        {
            _hitPanel.rotation = Quaternion.Euler(0, 0, _panelRotation);
            _hitPanel.anchoredPosition = _leftPosLow;
            _hitPanel.DOAnchorPos(Vector2.zero, _transitionDuration).SetEase(Ease.InQuad).OnComplete(() =>
            {
                _hitPanel.DOAnchorPos(_rightPosHigh, _transitionDuration).SetEase(Ease.InQuad);
            });
            _p2Score++;
            Vector2 pos = new Vector2(_p2ScoreTracker.position.x + (_placementOffset * _p2Score), _p2ScoreTracker.position.y);
            var marker = Instantiate(_scoreMark, _p2ScoreTracker);
            marker.transform.position = pos;
        }
        else if (hitPlayer == _p2Controller)
        {
            _hitPanel.rotation = Quaternion.Euler(0, 0, -_panelRotation);
            _hitPanel.anchoredPosition = _rightPosLow;
            _hitPanel.DOAnchorPos(Vector2.zero, _transitionDuration).SetEase(Ease.InQuad).OnComplete(() =>
            {
                _hitPanel.DOAnchorPos(_leftPosHigh, _transitionDuration).SetEase(Ease.InQuad);
            });
            _p1Score++;
            Vector2 pos = new Vector2(_p1ScoreTracker.position.x - (_placementOffset * _p1Score), _p1ScoreTracker.position.y);
            var marker = Instantiate(_scoreMark, _p1ScoreTracker);
            marker.transform.position = pos;
        }

        NextRound();
    }

    public void NextRound()
    {
        _roundNumber++;
        _roundText.text = _roundNumber.ToString();
    }

}
