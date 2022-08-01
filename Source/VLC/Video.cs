namespace VLC;

public class Video : VLCMedia
{
    public Video(string url) : base(url, isVideo: true) { }
}