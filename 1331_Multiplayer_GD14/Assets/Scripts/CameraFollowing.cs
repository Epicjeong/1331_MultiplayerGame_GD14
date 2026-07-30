using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    //refrence to both players
    [SerializeField] private Transform _player1;
    [SerializeField] private Transform _player2;
    [SerializeField] private float _bounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((_player1.position.x + _player2.position.x) / 2 <= _bounds && (_player1.position.x + _player2.position.x) / 2 >= -_bounds)
        {
            //makes sure that the camera is always between the players
            transform.position = new Vector3((_player1.position.x + _player2.position.x) / 2, transform.position.y, transform.position.z);
        }
    }
}
