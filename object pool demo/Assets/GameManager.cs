using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    class BulletPool
    {
        [SerializeField] private GameObject _bulletObject;
        [SerializeField] private Queue<GameObject> _bullets;

        public GameObject GetNextBullet(bool addToPoolIfNone = false)
        {
            if (_bullets.Count == 0)
            {
                if (addToPoolIfNone)
                    return Instantiate(_bulletObject);
                return null;
            }
            return _bullets.Dequeue();
        }

        public void ReturnToPool(GameObject bullet)
        {
            _bullets.Enqueue(bullet);
        }
    }
}
