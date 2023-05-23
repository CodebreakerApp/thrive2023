using System.Diagnostics.Metrics;

namespace Codebreaker.GameAPIs.Metrics;

public class GameMetrics
{
    private static readonly Meter s_meter;
    private static readonly Counter<int> s_gamesStarted;
    private static readonly Counter<int> s_movesDone;
    private static readonly UpDownCounter<int> s_gamesActive;

    static GameMetrics()
    {
        s_meter = new("Codebreaker.GameAPI", "3.0.1");
        s_gamesStarted = s_meter.CreateCounter<int>("games-started", "games", "the number of games started");
        s_movesDone = s_meter.CreateCounter<int>("moves-done", "moves", "the number of moves done");
        s_gamesActive = s_meter.CreateUpDownCounter<int>("games-active", "games", "the current number of games active");
    }

    public static string Name => s_meter.Name;

    public static void StartGame()
    {
        s_gamesStarted.Add(1);
        s_gamesActive.Add(1);
    }

    public static void EndGame()
    {
        s_gamesActive.Add(-1);
    }

    public static void SetMove()
    {
        s_movesDone.Add(1);
    }
}