using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ZipLine : MonoBehaviour
{
    [SerializeField] Transform _start;
    [SerializeField] Transform _end;

    [SerializeField] LineRenderer _lr;
    // Start is called before the first frame update
    void Start()
    {
        _lr = GetComponent<LineRenderer>();
        _lr.SetPosition(0, _start.position);
        _lr.SetPosition(1, _end.position);
    }
}
