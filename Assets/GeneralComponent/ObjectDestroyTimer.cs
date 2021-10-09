using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyTimer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float life = 2f;
    void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(life);
        Destroy(gameObject);
    }
}
