using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PlayerControls _p1Controller;
    [SerializeField] private PlayerControls _p2Controller;
    private int _p1Score;
    private int _p2Score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        }
        else if (hitPlayer == _p2Controller)
        {
            _p1Score++;
        }
        Debug.Log(_p1Score);
        Debug.Log(_p2Score);
    }
}
