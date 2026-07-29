using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private PlayerControls _player;
    [SerializeField] private ScoreManager _scoreManager;

    private void OnTriggerEnter2D()
    {
        if (_player._guarding)
            return;
        else if (!_player._guarding)
            _scoreManager.AddScore(_player);
    }
}
