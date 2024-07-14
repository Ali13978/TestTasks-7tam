using AllIn1SpriteShader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IShootable
{
    [SerializeField] AudioClip bulletImpactAudio;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ApplyHit()
    {
        Material mat = GetComponent<Renderer>().material;
        mat.EnableKeyword("HITEFFECT_ON");
        audioSource.PlayOneShot(bulletImpactAudio);
        Invoke(nameof(DisableHitEffect), 0.2f);
    }

    private void DisableHitEffect()
    {
        Material mat = GetComponent<Renderer>().material;
        mat.DisableKeyword("HITEFFECT_ON");
    }
}
