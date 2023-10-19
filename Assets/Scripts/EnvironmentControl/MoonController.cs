using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonController : MonoBehaviour
{
    [SerializeField] private GameObject follow;
    private float offsetX;
    private float offsetY;


    // Start is called before the first frame update
    void Start()
    {
        offsetX = transform.position.x - follow.transform.position.x;
        offsetY = transform.position.y - follow.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(offsetX + follow.transform.position.x, offsetY + follow.transform.position.y);
    }
}
