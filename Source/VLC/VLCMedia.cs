using LibVLCSharp.Shared;

namespace VLC;

public abstract class VLCMedia
{
    private MediaPlayer _MediaPlayer;
    private Media _Media;

    public bool IsPlaying => _MediaPlayer.IsPlaying;

    public VLCMedia(string url, bool isVideo = false){
        using var libVLC = new LibVLC(enableDebugLogs: true);
        _Media = new Media(libVLC, new Uri(url));
        if (!isVideo) _Media.AddOption(":no-video");
        _MediaPlayer = new MediaPlayer(_Media);
    }

    public void Play() => _MediaPlayer.Play();

    public void Pause() => _MediaPlayer.Pause();

    public void Stop() => _MediaPlayer.Stop();
}