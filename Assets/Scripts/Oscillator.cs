using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 moveMent;
    [SerializeField] float period = 2f;

    //[SerializeField] [Range(0,1)] float MovementSpeed;  // Makes the floating value a slider 

    float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) { return; }

        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2f;

        float rawsinwave = Mathf.Sin(cycles * tau);

        movementFactor = (rawsinwave + 1f) / 2f;

        Vector3 offset = moveMent * movementFactor;

        transform.position = startingPosition + offset;
    }
}
