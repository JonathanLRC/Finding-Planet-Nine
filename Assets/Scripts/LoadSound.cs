using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadSound : MonoBehaviour
{
    public AudioSource source { get { return GetComponent<AudioSource>(); } }
    public Button btn { get { return GetComponent<Button>(); }}
    public AudioClip clip;
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        btn.onClick.AddListener(PlaySound);
    }

    // Update is called once per frame
    void PlaySound()
    {
        source.PlayOneShot(clip);
    }
}
