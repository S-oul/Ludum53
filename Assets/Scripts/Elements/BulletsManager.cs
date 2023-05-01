using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsManager : MonoBehaviour
{
    [SerializeField] GameObject _bullets;
    [SerializeField] PlayerMovement _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = transform.parent.GetComponent<PlayerMovement>();
        UpdateBulletPos();
    }
    
    // Update is called once per frame
    public void UpdateBulletPos()
    {
        int count = 0;
        bool isfirst = true;
        foreach(Transform t in transform)
        {
            if (isfirst)
            {
                isfirst = false;
            }
            else
            {
                t.localPosition = new Vector3(7 - (count * .7f), -4.10f, 1);
                count++;

            }
        }
        /*if(count < _player.BulletInInvetory)
        {
            for(int i = _player.BulletInInvetory - count; i  >= 0; i--)
            {
                Instantiate(_bullets);
            }
            UpdateBulletPos();
        }*/
    }
}
