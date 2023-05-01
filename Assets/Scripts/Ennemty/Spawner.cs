using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] List<Transform> _transforms;

    bool _once = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_once)
        {
            _once = !_once;
            foreach (Transform t in _transforms)
            {
                GameObject en = Instantiate(EnemyPrefab);
                en.GetComponent<Enemy>().enabled = true;
                en.transform.position = t.position;
            }
        }

    }
}
