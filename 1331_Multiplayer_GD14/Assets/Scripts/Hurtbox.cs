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
            Debug.Log(_player._guarding);
            StopCoroutine(_attackingPlayer.Cooldown(1));
            _attackingPlayer._actionable = false;
            _attackingPlayer._stunned = true;
            StartCoroutine(_attackingPlayer.Cooldown(1f));
        }
        else if (!_player._guarding)
            _scoreManager.AddScore(_player);
    }
}
