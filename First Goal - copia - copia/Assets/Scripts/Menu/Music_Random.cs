using UnityEngine;
using System.Collections;

public class Music_Random : MonoBehaviour
{
    AudioSource audio;

    float alpha = 1.0f;
    public float fadeSpeed = 2.0f;

    public AudioClip[] PlayList;


    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (!audio.playOnAwake)
            audio.clip = PlayList[Random.Range(0, PlayList.Length)] as AudioClip;
        audio.Play();
        StartCoroutine(Fade());
    }

    void Update()
    {
        if (!audio.isPlaying)
        {
            playRandomMusic();
            StartCoroutine(Fade());
        }
    }

    void playRandomMusic()
    {
        audio.clip = PlayList[Random.Range(0, PlayList.Length)] as AudioClip;
        audio.Play();
    }

    void OnGUI()
    {
        GUIStyle myStyle = new GUIStyle(GUI.skin.GetStyle("label"));
        myStyle.fontSize = 28;
        GUI.color = new Color(255, 255, 0, alpha);
        GUI.Label(new Rect(25, 50, 1500, 200), "Play Song:\n" + "[" + audio.clip.name + "]", myStyle);
    }

    IEnumerator Fade()
    {
        alpha = 0;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
        StartCoroutine(FadeOUT());
    }

    IEnumerator FadeOUT()
    {
        yield return new WaitForSeconds(10);

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }
}