using UnrealReplayReader.Fortnite;
using UnrealReplayReader.Fortnite.Models.Exports;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Tests;

public class FortniteReplayReaderTest
{
    [Fact]
    public void TestReadReplay()
    {
        var settings = new ReplayReaderSettings
        {
            IsDebug = true,
            Logger = null,
            UseCheckpoints = false,
            ExportConfiguration = ReplayExportConfiguration.FromAssembly(typeof(GameStateExport))
        };
        var replay = FortniteReplayReader.FromFile("Replays/CH04SE3_25_20.replay", settings);
        
        Assert.Equal("++Fortnite+Release-25.20", replay.Header.EngineVersion.Branch);
        Assert.Equal(ENetworkVersionHistory.HistoryUseCustomVersion, replay.Header.Version);
        Assert.Equal("WindowsClient", replay.Header.Platform);
        Assert.Equal(466, replay.ExportGroupDict.Count);
        var match = replay.MatchData;
        var state = replay.StateData;
        var events = replay.Events;
        Assert.Equal(132, match.Players.Count);
        Assert.Equal(132, state.Players.Count);
        Assert.Equal(468u, events.MatchStats?.WeaponDamage);
    }
}