using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public GameObject objectOnLast;

    public Vector2 Target;
    [SerializeField]
    float speed = 200f;
    [SerializeField]
    float hitRange = 0.5f;
    [SerializeField]
    ParticleSystem particle;

    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = Target - (Vector2)transform.position;
        direction.Normalize();

        if(((Vector2)transform.position - Target).sqrMagnitude < hitRange * hitRange)
        {
            if(particle != null)
            {
                particle.Stop();
                if(objectOnLast != null)
                {
                    Instantiate(objectOnLast, transform.position, transform.rotation);
                    objectOnLast = null;
                }
            }
            //Destroy(gameObject);
        }

        transform.position = transform.position + (Vector3)direction * speed * Time.deltaTime;
    }

}
