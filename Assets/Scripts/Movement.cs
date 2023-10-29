using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rgbody;

    AudioSource audioSource;

    [SerializeField] float MainThrust = 100f;

    [SerializeField] float RotationThrust = 1.0f;

    [SerializeField] AudioClip mainEngineThrust;

    [SerializeField] ParticleSystem mainEnginePS;
    [SerializeField] ParticleSystem leftBoosterPS;
    [SerializeField] ParticleSystem rightBoosterPS;

    // Start is called before the first frame update
    void Start()
    {
        rgbody = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainEnginePS.Stop();
    }

    private void StartThrusting()
    {
        rgbody.AddRelativeForce(Vector3.up * MainThrust * Time.deltaTime);

        if (!mainEnginePS.isPlaying)
        {
            mainEnginePS.Play();
        }

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineThrust);
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            StartLeftRotation();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            StartRightRotation();
        }
        else
        {
            StopRotation();
        }
    }

    private void StopRotation()
    {
        leftBoosterPS.Stop();
        rightBoosterPS.Stop();
    }

    private void StartRightRotation()
    {
        ApplyRotation(-RotationThrust);
        if (!rightBoosterPS.isPlaying)
        {
            rightBoosterPS.Play();
        }
    }

    private void StartLeftRotation()
    {
        ApplyRotation(RotationThrust);
        if (!leftBoosterPS.isPlaying)
        {
            leftBoosterPS.Play();
        }
    }

    void ApplyRotation(float thrustApplied)
    {
        transform.Rotate(Vector3.forward *  thrustApplied * Time.deltaTime);
    }
}
