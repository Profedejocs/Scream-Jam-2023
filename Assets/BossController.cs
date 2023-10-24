using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BossController : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed = 80;
    [SerializeField] private GameObject _cloudMain;
    [SerializeField] private GameObject _cloudSmall1;
    [SerializeField] private GameObject _cloudSmall2;

    private float _circle1CurAngle;
    private float _circle2CurAngle;

    [SerializeField] private float _angularSpeed = 1f;

    private float _circleRad1;
    private float _circleRad2;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Character");
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _circleRad1 = Vector2.Distance(_cloudMain.transform.position, _cloudSmall1.transform.position);
        _circleRad2 = Vector2.Distance(_cloudMain.transform.position, _cloudSmall2.transform.position);

        Vector3 offset1 = _cloudSmall1.transform.position - _cloudMain.transform.position;
        _circle1CurAngle = Mathf.Atan2(offset1.y, offset1.x);

        Vector3 offset2 = _cloudSmall2.transform.position - _cloudMain.transform.position;
        _circle2CurAngle = Mathf.Atan2(offset2.y, offset2.x);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Follow Player
        var direction = _player.transform.position - transform.position;
        _rigidbody2D.velocity = direction * _speed * Time.deltaTime;

        //Circle Rotate 1
        _circle1CurAngle += _angularSpeed * Time.deltaTime;
        Vector3 offset1 = new Vector3(Mathf.Sin(_circle1CurAngle), Mathf.Cos(_circle1CurAngle), 0f) * _circleRad1;
        _cloudSmall1.transform.position = _cloudMain.transform.position + offset1;

        //Circle Rotate 2
        _circle2CurAngle += _angularSpeed * Time.deltaTime;
        Vector3 offset2 = new Vector3(Mathf.Sin(_circle2CurAngle), Mathf.Cos(_circle2CurAngle), 0f) * _circleRad2;
        _cloudSmall2.transform.position = _cloudMain.transform.position + offset2;
    }

    public void Despawn() {
        _cloudMain.GetComponent<ParticleSystem>().Stop();
        _cloudSmall1.GetComponent<ParticleSystem>().Stop();
        _cloudSmall2.GetComponent<ParticleSystem>().Stop();

        Destroy(GetComponent<InfectionSource>());
    }
}
