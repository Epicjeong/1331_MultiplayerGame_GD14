using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private CharacterController _charControl;
    [SerializeField] private float _speed;
    [SerializeField] private float _backstepStrength;
    private Vector2 _input;
    private bool _actionable = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _charControl.Move(_input * _speed * Time.deltaTime);
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
            var backstepLength = .1f;
            StartCoroutine(Cooldown(backstepLength));
        }
    }

    public IEnumerator Cooldown(float cooldownLength)
    {
        yield return new WaitForSeconds(cooldownLength);
        _actionable = true;
        _input = Vector2.zero;
    }
}
