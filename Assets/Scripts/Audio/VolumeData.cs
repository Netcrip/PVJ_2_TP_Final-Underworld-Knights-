using System;

[Serializable]
public class VolumeData
{
    public float master;
    public float music;
    public float sfx;

    public VolumeData(float master, float music, float sfx)
    {
        this.master = master;
        this.music = music;
        this.sfx = sfx;
    }
}
