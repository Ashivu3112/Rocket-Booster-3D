using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] AudioClip Success;
    [SerializeField] AudioClip Blast;
    [SerializeField] ParticleSystem SuccessPS;
    [SerializeField] ParticleSystem BlastPS;
    
    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    private void Update()
    {
        ProcessDebugKeys();
    }

    private void ProcessDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning || collisionDisabled) return;

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                // Load Next level
                StartLoadNext();
                break;
            default:
                // Reload current level
                StartReload();
                break;
        }
    }

    void StartReload()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(Blast);
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        if (!BlastPS.isPlaying)
        {
            BlastPS.Play();
        }
        Invoke("ReloadLevel", 1f);
    }

    void StartLoadNext()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(Success);
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        if(!SuccessPS.isPlaying)
        {
            SuccessPS.Play();
        }
        Invoke("LoadNextLevel", 1f);
    }

    void ReloadLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }

    void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;
        if(nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);
    }
}
