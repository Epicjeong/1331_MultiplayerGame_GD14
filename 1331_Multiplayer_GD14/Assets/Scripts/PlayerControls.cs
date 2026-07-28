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
    private Vector2 _input;
    private bool _actionable = true;
    private bool _attacking = false;

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

    public IEnumerator Cooldown(float cooldownLength)
    {
        yield return new WaitForSeconds(cooldownLength);
        _actionable = true;
        _attacking = false;
        _input = Vector2.zero;
    }

    //Animation things
    [SerializeField] private Animator _animator;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attacking = Animator.StringToHash("Attacking");

    private void AnimParameters()
    {
        _animator.SetFloat(Speed, _input.sqrMagnitude);
        _animator.SetBool(Attacking, _attacking);
    }
}
