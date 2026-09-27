using UnityEngine;

public class Microwave : MonoBehaviour, IInteractable
{
    [Header("Microwave Light")]
    public Light m_Microwave;
    public bool isOn;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;      // для одноразовых кликов
    [SerializeField] private AudioClip onSound;
    [SerializeField] private AudioClip offSound;

    [Header("Loop Sound")]
    [SerializeField] private AudioSource loopSource;       // отдельный AudioSource с Loop = true
    [SerializeField] private AudioClip humSound;           // гудение, пока микроволновка работает

    [Header("Auto Off")]
    [SerializeField] private float autoOffTime = 5f;

    private float timer;
    private bool timerActive;

    private void Start()
    {
        if (m_Microwave != null)
            m_Microwave.enabled = isOn;

        if (isOn)
        {
            StartTimer();
            StartHum();
        }
    }

    private void Update()
    {
        if (!timerActive) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            TurnOff();
        }
    }

    public string GetDescription()
    {
        if (isOn)
            return "Turn off the microwave";
        else
            return "Turn on the microwave";
    }

    public void Interact()
    {
        if (isOn)
            TurnOff();
        else
            TurnOn();
    }

    private void TurnOn()
    {
        isOn = true;
        if (m_Microwave != null) m_Microwave.enabled = true;

        if (audioSource != null && onSound != null)
            audioSource.PlayOneShot(onSound);

        StartHum();
        StartTimer();
    }

    private void TurnOff()
    {
        isOn = false;
        if (m_Microwave != null) m_Microwave.enabled = false;

        if (audioSource != null && offSound != null)
            audioSource.PlayOneShot(offSound);

        StopHum();
        timerActive = false;
    }

    private void StartTimer()
    {
        timer = autoOffTime;
        timerActive = true;
    }

    private void StartHum()
    {
        if (loopSource == null || humSound == null) return;

        loopSource.clip = humSound;
        loopSource.loop = true;
        loopSource.Play();
    }

    private void StopHum()
    {
        if (loopSource != null && loopSource.isPlaying)
            loopSource.Stop();
    }
}