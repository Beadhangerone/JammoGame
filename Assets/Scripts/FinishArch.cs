using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishArch : MonoBehaviour
{
    public GameObject player;
    private MyLevelManager _levelManager;
    [Range(1, 3)]
    public int level;
    private Rigidbody _rigidbody;

    void Awake()
    {
        _levelManager = MyLevelManager.Instance;
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other == player)
        {
            _levelManager.FinishLevel(level);
        }
    }
}
