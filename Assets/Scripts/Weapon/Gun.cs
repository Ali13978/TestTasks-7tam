using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletToShoot;
    public SpriteRenderer weaponSprite;
    public SpriteRenderer muzzleSprite;
    public Transform muzzlePosition;
    public AudioClip shotAudio;

    public int bulletsToShoot;
    public float coolDownTime;
    public float bulletSpeed = 25f;
    public float gunRange = 5f;
    public float spreadAngle = 15f;
    public float cameraShake = 0f;
    private float lastShootTime;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastShootTime = -coolDownTime;
    }
    public void Shoot()
    {
        if (Time.time - lastShootTime < coolDownTime)
            return;

        Vector3 mainDirection = muzzlePosition.right;

        muzzleSprite.gameObject.GetComponent<Animator>().SetBool("showMuzzle", true);
        Invoke(nameof(MuzzleOff), 0.2f);

        audioSource.PlayOneShot(shotAudio);
        CameraShake.instance.ShakeCamera(cameraShake, cameraShake, 0.3f);

        for (int i = 0; i < bulletsToShoot; i++)
        {
            float angle = Random.Range(-spreadAngle, spreadAngle);
            Vector3 direction = Quaternion.Euler(0, 0, angle) * mainDirection;

            GameObject bullet = Instantiate(bulletToShoot, muzzlePosition.position, muzzlePosition.rotation);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction.normalized * bulletSpeed;
            }
            else
            {
                Debug.LogError("No Rigidbody 2D found on bullet");
            }
        }

        lastShootTime = Time.time;
    }


    private void MuzzleOff()
    {
        muzzleSprite.gameObject.GetComponent<Animator>().SetBool("showMuzzle", false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gunRange);
    }
}
