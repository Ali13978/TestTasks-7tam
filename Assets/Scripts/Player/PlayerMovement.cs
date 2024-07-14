using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] ParticleSystem footstepParticles;

    #region Private Members
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerInputs inputs;
    private AudioSource audioSource;

    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        inputs = GetComponent<PlayerInputs>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("MoveX", inputs.movement.x);
        anim.SetFloat("MoveY", inputs.movement.y);
        anim.SetFloat("Speed", inputs.movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + inputs.movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    public void PlayFootstep()
    {
        footstepParticles.Play();
    }
    public void PlayFootstepSound()
    {
        audioSource.Play();
    }
}
