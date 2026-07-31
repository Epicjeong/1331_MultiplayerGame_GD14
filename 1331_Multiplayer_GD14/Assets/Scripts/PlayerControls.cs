using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private CharacterController _charControl;
    [SerializeField] private float _speed;
    [SerializeField] private float _backstepStrength;
    [SerializeField] private float _backstepLength;
    [SerializeField] private float _attackLength;
    [SerializeField] private float _guardLength;
    [SerializeField] private Transform _spawn;
    private Vector2 _input;
    public bool _attacking = false;
    public bool _actionable = false;
    public bool _guarding = false;
    public bool _stunned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!_stunned)
        {
            _charControl.Move(_input * _speed * Time.deltaTime);
        }
        AnimParameters();
    }

    public void Forward(InputAction.CallbackContext context)
    {
        if (_actionable && !_attacking && !_guarding && !_stunned)
            _input = context.ReadValue<Vector2>();
    }

    public void Backstep(InputAction.CallbackContext context)
    {
        if (_actionable && !_attacking)
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
            StartCoroutine(Cooldown(_attackLength));
        }
    }

    public void Guard(InputAction.CallbackContext context)
    {
        if (_actionable)
        {
            _guarding = true;
            StartCoroutine(GuardCooldown());
        }
    }

    public void ReturnToSpawn()
    {
        transform.position = _spawn.position;
        Physics.SyncTransforms();
    }

    //Universal cooldown, adjustable for most actions
    public IEnumerator Cooldown(float cooldownLength)
    {
        yield return new WaitForSeconds(cooldownLength);
        _stunned = false;
        _attacking = false;
        _actionable = true;
        _input = Vector2.zero;
    }

    //guarding needs a seperate cooldown because player should be vulnerable before being actionable
    public IEnumerator GuardCooldown()
    {
        yield return new WaitForSeconds(_guardLength);
        _guarding = false;
        yield return new WaitForSeconds(_guardLength * 2);
        //_actionable = true;
    }

    //Animation things
    [SerializeField] private Animator _animator;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attacking = Animator.StringToHash("Attacking");
    private static readonly int Guarding = Animator.StringToHash("Guarding");
    private static readonly int Stunned = Animator.StringToHash("Stunned");

    private void AnimParameters()
    {
        _animator.SetFloat(Speed, _input.sqrMagnitude);
        _animator.SetBool(Attacking, _attacking);
        _animator.SetBool(Guarding, _guarding);
        _animator.SetBool(Stunned, _stunned);
    }

    public void PauseAnim()
    {
        _animator.speed = 0;
    }

    public void ResumeAnim()
    {
        _animator.speed = 1;
    }
    public void StopAllAnim()
    {
        _attacking = false;
        _guarding = false;
        _stunned = false;
    }
}
