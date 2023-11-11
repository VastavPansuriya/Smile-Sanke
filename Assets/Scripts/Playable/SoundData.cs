using UnityEngine;
using Snake2D.Enum;

[System.Serializable]
public class SoundData<T>
{
    public T effectType;
    public AudioClip effectClip;
}


public interface ISoundMusicChanger
{
    public void ChangeBGMusic (MusicType musicType);
    public void PlayEffect (SoundType soundType);
}