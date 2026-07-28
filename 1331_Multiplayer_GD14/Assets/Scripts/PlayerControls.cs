using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private CharacterController _charControl;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private float _speed;
    [SerializeField] private float _backstepStrength;
    [SerializeField] private float _backstepLength;
    [SerializeField] private float _attackLength;
    [SerializeField] private float _guardLength;
    private Vector2 _input;
    private bool _actionable = true;
    private bool _attacking = false;
    private bool _guarding = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _charControl.Move(_input * _speed * Time.deltaTime);
        AnimParameters();
    }

    public void Forward(InputAction.CallbackContext context)
    {
        if (_actionable)
            _input = context.ReadValue<Vector2>();
    }

    public void Backstep(InputAction.CallbackContext context)
    {
        if (_actionable)
        {
            _input = new Vector2(_backstepStrength, 0) * context.ReadValue<Vector2>();
            _actionable = false;
            StartCoroutine(Cooldown(_backstepLength));
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (_actionable)
        {
            _attacking = true;
            _actionable = false;
            StartCoroutine(Cooldown(_attackLength));
        }
    }

    public void Guard(InputAction.CallbackContext context)
    {
        if (_actionable)
        {
            _guarding = true;
            _actionable = false;
            StartCoroutine(GuardCooldown());
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var opponent = other.GetComponent<PlayerControls>();
        _scoreManager.AddScore(opponent);
    }

    //Universal cooldown, adjustable for most actions
    public IEnumerator Cooldown(float cooldownLength)
    {
        yield return new WaitForSeconds(cooldownLength);
        _actionable = true;
        _attacking = false;
        _input = Vector2.zero;
    }

    //guarding needs a seperate cooldown because player should be vulnerable before being actionable
    public IEnumerator GuardCooldown()
    {
        yield return new WaitForSeconds(_guardLength);
        _guarding = false;
        yield return new WaitForSeconds(_guardLength);
        _actionable = true;
    }

    //Animation things
    [SerializeField] private Animator _animator;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attacking = Animator.StringToHash("Attacking");
    private static readonly int Guarding = Animator.StringToHash("Guarding");

    private void AnimParameters()
    {
        _animator.SetFloat(Speed, _input.sqrMagnitude);
        _animator.SetBool(Attacking, _attacking);
        _animator.SetBool(Guarding, _guarding);
    }
}
