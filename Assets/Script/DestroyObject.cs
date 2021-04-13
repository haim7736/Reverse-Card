using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float time;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destory", time);
    }

    private void Destory()
    {
        Destroy(gameObject);
    }
}
