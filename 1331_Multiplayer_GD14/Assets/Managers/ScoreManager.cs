using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PlayerControls _p1Controller;
    [SerializeField] private PlayerControls _p2Controller;
    private int _p1Score;
    private int _p2Score;

    [SerializeField] private Transform _p1ScoreTracker;
    [SerializeField] private Transform _p2ScoreTracker;
    [SerializeField] protected GameObject _scoreMark;
    private int _markSize = 50;
    private int _placementOffset = 320;

    [SerializeField] private TMP_Text _roundText;
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
            _p2Score++;
            Vector2 pos = new Vector2(_p2Score * -_markSize + _placementOffset, transform.position.y);
            var marker = Instantiate(_scoreMark, _p1ScoreTracker);
            //marker.transform.position = pos;
        }
        else if (hitPlayer == _p2Controller)
        {
            _p1Score++;
            Vector2 pos = new Vector2(_p2Score * -_markSize - _placementOffset, transform.position.y);
            var marker = Instantiate(_scoreMark, _p1ScoreTracker);
            //marker.transform.position = pos;
        }

        NextRound();
    }

    public void NextRound()
    {
        _roundNumber++;
        _roundText.text = _roundNumber.ToString();
    }

}
