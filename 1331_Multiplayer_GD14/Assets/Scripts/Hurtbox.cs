using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private PlayerControls _player;
    [SerializeField] private PlayerControls _attackingPlayer;
    [SerializeField] private ScoreManager _scoreManager;

    private void OnTriggerEnter2D()
    {
        if (_player._guarding)
        {
            _attackingPlayer._actionable = false;
            _attackingPlayer._stunned = true;
            _player._guarding = false;
            _player._actionable = true;
        }
        else if (!_player._guarding)
            _scoreManager.AddScore(_player);
    }
}
