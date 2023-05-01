using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    bool _isOk;
    [SerializeField] PlayerMovement _player;
    [SerializeField] Barrel _barrel;
    [SerializeField] BulletsManager _bulletsManager;

    // Start is called before the first frame update
    void Start()
    {
       _player = transform.parent.parent.GetComponent<PlayerMovement>();
       _barrel = transform.parent.GetChild(0).GetComponent<Barrel>();
       _bulletsManager = transform.parent.GetComponent<BulletsManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = transform.position.z;
            float Dist = Vector3.Distance(transform.position, mousepos);
            //print(Dist);
            if(Dist < 0.5f) 
            {
                _isOk = true;
                _player.CanShoot = false;
            }
        }
        if(_isOk && Input.GetMouseButton(0))
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z = transform.position.z;
            transform.position = mousepos;
        }
        if(Input.GetMouseButtonUp(0))
        {
            _isOk = false;
            foreach(Transform t in _barrel.transform) 
            { 
                float Dist = Vector2.Distance(t.position, transform.position);  
                print(Dist);
                if(Dist < 1.4f)
                {
                    if (!_barrel.isholed(t.name))
                    {
                        _barrel.changeholed(t.name);
                        _player.CanShoot = true;
                        _player.BulletCount++;
                        _player.BulletInInvetory--;
                        t.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                        print(t.name);
                        Destroy(gameObject);
                        break;
                    }

                }
            }
            _bulletsManager.UpdateBulletPos();
        }
    }
}
