using TUI;

ProgressBar PrBar = new(32);

for (int i = 0; i <= PrBar.Max; i++)
{
    PrBar.Value = i;
    Thread.Sleep(200);
}