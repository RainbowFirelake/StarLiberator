using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReaction : MonoBehaviour
{
    [SerializeField]
    private Health _health;
    [SerializeField]
    private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _health.OnHit += PlayImpactSound;
    }

    private void OnDisable()
    {
        _health.OnHit -= PlayImpactSound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayImpactSound()
    {
        _source.Play();
    }
}
