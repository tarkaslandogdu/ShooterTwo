using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponSounds : MonoBehaviour
{
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip reloadSound;

    AudioSource gunAudio;

    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
    }

    public void ShoutSound()
    {
        gunAudio.PlayOneShot(shootSound);
    }

    public void ReloadSound()
    {
        gunAudio.PlayOneShot(reloadSound);
    }
}
