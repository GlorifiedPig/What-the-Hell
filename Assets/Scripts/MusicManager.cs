using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] music;
    public AudioSource musicPlayer;
    public float musicVolume = 0.4f;

    public int musicState = 0;

    int musicStateTest = -1;

    public float transitionTime = 0.2f;

    bool switchingMusic = false;

    void PlayAppropriateMusic()
    {
        StartCoroutine( SwitchMusic( music[Random.Range( 0, music.Length )] ) );
    }

    void Update()
    {
        if( !switchingMusic )
        {
            if( musicStateTest != musicState )
            {
                musicStateTest = musicState;
                PlayAppropriateMusic();
            } else if( !musicPlayer.isPlaying ) PlayAppropriateMusic();
        }
    }

    IEnumerator SwitchMusic( AudioClip nextSong )
    {
        switchingMusic = true;
        float musicVol = 1;

        while( musicVol > 0 )
        {
            musicVol = Mathf.Max( musicVol - ( Time.deltaTime / transitionTime ), 0 );
            musicPlayer.volume = musicVol * musicVolume;
            yield return new WaitForEndOfFrame();
        }

        musicPlayer.Stop();
        musicPlayer.clip = nextSong;
        musicPlayer.Play();

        while( musicVol < 1 )
        {
            musicVol = Mathf.Min( musicVol + ( Time.deltaTime / transitionTime ), 1 );
            musicPlayer.volume = musicVol * musicVolume;
            yield return new WaitForEndOfFrame();
        }

        switchingMusic = false;
    }
}