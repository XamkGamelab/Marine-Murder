using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource SonarPingAudioSource;
    [SerializeField] private float sonarPingInterval = 3f;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= sonarPingInterval)
        {
            SonarPingAudioSource.Play();
            timer = 0f;
        }
    }
}
