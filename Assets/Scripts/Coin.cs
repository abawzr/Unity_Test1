using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip pickupCoinClip;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // When another collider hits the coin, 
        //  check that collider tag if it's player then play pickup coin clip and destory the coin object after the clip ends
        if (other.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(pickupCoinClip);
            Destroy(gameObject, pickupCoinClip.length);
        }
    }
}
