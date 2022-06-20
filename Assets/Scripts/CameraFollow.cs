using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Vector3 moveTemp;
    public float offsetY = 2;
    public float offsetX = 2;

    // Start is called before the first frame update
    void Start()
    {
        moveTemp=_target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveTemp = _target.transform.position;
        moveTemp.y += offsetY;
        moveTemp.x += offsetX;
        moveTemp.z = transform.position.z;
        transform.position = moveTemp;
    }
}
