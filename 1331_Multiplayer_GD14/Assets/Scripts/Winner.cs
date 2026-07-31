using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour
{
    [SerializeField] private TMP_Text _winnerText;
    private bool _p1Ready = false;
    private bool _p2Ready = false;

    public void WinGame(int winner)
    {
        if (winner == 1)
        {
            _winnerText.text = "PLAYER 1";
        }
        if (winner == 2)
        {
            _winnerText.text = "PLAYER 2";
        }
    }

    public void P1Ready(InputAction.CallbackContext context)
    {
        _p1Ready = true;
        Restart();
    }

    public void P2Ready(InputAction.CallbackContext context)
    {
        _p2Ready = true;
        Restart();
    }

    public void Restart()
    {
        if (_p1Ready && _p2Ready) 
            SceneManager.LoadScene("SampleScene");
    }
}
