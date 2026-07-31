using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private GameObject _p1ReadyText;
    [SerializeField] private GameObject _p2ReadyText;
    [SerializeField] private AudioSource _readySound;
    private bool _p1Ready = false;
    private bool _p2Ready = false;
    public void P1Ready(InputAction.CallbackContext context)
    {
        _p1Ready = true;
        _p1ReadyText.SetActive(true);
        _readySound.Play();
        Restart();
    }

    public void P2Ready(InputAction.CallbackContext context)
    {
        _p2Ready = true;
        _readySound.Play();
        _p2ReadyText.SetActive(true);
        Restart();
    }

    public void Restart()
    {
        if (_p1Ready && _p2Ready)
            SceneManager.LoadScene("SampleScene");
    }
}
