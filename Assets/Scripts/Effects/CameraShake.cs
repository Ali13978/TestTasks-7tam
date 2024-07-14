using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin perlinNoise;

    #region Singleton
    public static CameraShake instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    void Start()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        perlinNoise = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float amplitude, float frequency, float duration)
    {
        StartCoroutine(Shake(amplitude, frequency, duration));
    }

    private IEnumerator Shake(float amplitude, float frequency, float duration)
    {
        perlinNoise.m_AmplitudeGain = amplitude;
        perlinNoise.m_FrequencyGain = frequency;

        yield return new WaitForSeconds(duration);

        perlinNoise.m_AmplitudeGain = 0f;
        perlinNoise.m_FrequencyGain = 0f;
    }
}
