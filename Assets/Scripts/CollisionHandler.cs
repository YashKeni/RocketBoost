using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float timeToWait = 1f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    AudioSource audioSource;
    int skipCheat = 0;
    int disableColli = 0;
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        SkipLevel();
        DisableCollision();
    }

    void OnCollisionEnter(Collision other) 
    {   
        if(isTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                SuccessSeq();
                break;
            default:
                CrashSeq();
                break;
        }   
    }

    void SuccessSeq()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        GetComponent<RocketMovement>().enabled = false;
        Invoke("LoadNextLevel", timeToWait);
    }

    void CrashSeq()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        GetComponent<RocketMovement>().enabled = false;
        Invoke("ReloadScene", timeToWait);
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void SkipLevel()
    {
        if(skipCheat == 0 && Input.GetKeyDown(KeyCode.Tab))
        {
            skipCheat++;
        }
        if(skipCheat == 1 && Input.GetKeyDown(KeyCode.S))
        {
            skipCheat++;
        }
        if(skipCheat == 2 && Input.GetKeyDown(KeyCode.K))
        {
            skipCheat++;
        }
        if(skipCheat == 3 && Input.GetKeyDown(KeyCode.I))
        {
            skipCheat++;
        }
        if(skipCheat == 4 && Input.GetKeyDown(KeyCode.P))
        {
            skipCheat++;
        }
        if(skipCheat == 5 && Input.GetKeyDown(KeyCode.Tab))
        {
            LoadNextLevel();
        }
    }

    void DisableCollision()
    {
        if(disableColli == 0 && Input.GetKeyDown(KeyCode.G))
        {
            disableColli++;
        }
        if(disableColli == 1 && Input.GetKeyDown(KeyCode.O))
        {
            disableColli++;
        }
        if(disableColli == 2 && Input.GetKeyDown(KeyCode.D))
        {
            disableColli++;
        }   
        if(disableColli == 3 && Input.GetKeyDown(KeyCode.M))
        {
            disableColli++;
        }
        if(disableColli == 4 && Input.GetKeyDown(KeyCode.O))
        {
            disableColli++;
        }
        if(disableColli == 5 && Input.GetKeyDown(KeyCode.D))
        {
            disableColli++;
        }
        if(disableColli == 6 && Input.GetKeyDown(KeyCode.E))
        {
            disableColli++;
            collisionDisabled = !collisionDisabled;
        }
        if(disableColli == 7 && Input.GetKeyDown(KeyCode.C))
        {
            disableColli++;
            collisionDisabled = false;
        }
    }
}
