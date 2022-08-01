using VLC;

Audio a = new Audio("Assets/asia.mp3");

a.Play();

while (true)
{
    var key = Console.ReadKey();

    if (key.KeyChar == 'p') a.Pause();

    if (key.KeyChar == 'r') a.Play();

    if (key.KeyChar == 'q') break;
}