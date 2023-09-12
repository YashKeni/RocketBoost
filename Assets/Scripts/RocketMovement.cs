using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [Header("Rocket Performance Tuning")]
    [SerializeField] public float mainThrust = 1000f;
    [SerializeField] float turningSpeed = 10f;
    [Header("Audio Settings")]
    [SerializeField] AudioClip mainEngine;
    [Header("Particle Settings")]
    [Header("Main Thruster Particles")]
    [SerializeField] ParticleSystem engineParticles0;
    [SerializeField] ParticleSystem engineParticles1;
    [SerializeField] ParticleSystem engineParticles2;
    [SerializeField] ParticleSystem engineParticles3;
    [Header("Side Thrusters Particles")]
    [SerializeField] ParticleSystem leftBankParticles;
    [SerializeField] ParticleSystem rightBankParticles;
    Rigidbody myRigidBody;
    AudioSource audioSource;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {   
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            LeftRotate();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RightRotate();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        myRigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!engineParticles0.isPlaying && !engineParticles1.isPlaying && !engineParticles2.isPlaying && !engineParticles3.isPlaying)
        {
            engineParticles0.Play();
            engineParticles1.Play();
            engineParticles2.Play();
            engineParticles3.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        engineParticles0.Stop();
        engineParticles1.Stop();
        engineParticles2.Stop();
        engineParticles3.Stop();
    }

    void ApplyRotation(float rotateThisFrame)
    {
        myRigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateThisFrame * Time.deltaTime);
        myRigidBody.freezeRotation = false;
    }

    void LeftRotate()
    {
        ApplyRotation(turningSpeed);
        if (!leftBankParticles.isPlaying)
        {
            leftBankParticles.Play();
        }
    }

    void RightRotate()
    {
        ApplyRotation(-turningSpeed);
        if (!rightBankParticles.isPlaying)
        {
            rightBankParticles.Play();
        }
    }

    void StopRotating()
    {
        leftBankParticles.Stop();
        rightBankParticles.Stop();
    }
}
