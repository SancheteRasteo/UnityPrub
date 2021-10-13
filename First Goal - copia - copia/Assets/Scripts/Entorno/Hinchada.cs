using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hinchada : MonoBehaviour
{
    float VolumenHinchada = 0.5f;       //Modificar para q saque valor desde la configuracion

    new AudioSource Audio;

    [SerializeField]
    AudioClip[] PlayList;

    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();

        Audio.volume = 0;

        Audio.clip = PlayList[Random.Range(0, PlayList.Length)] as AudioClip;

        Audio.Play();

        StartCoroutine(RetardarHinchada());
    }

    IEnumerator RetardarHinchada()
    {
        while (Audio.volume < VolumenHinchada)
        {
            Audio.volume += 0.0005f;
            yield return null;
        }
    }
}
