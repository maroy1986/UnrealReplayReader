using FluentAssertions;
using UnrealReplayReader.FortniteMinimal;
using UnrealReplayReader.FortniteMinimal.Models;
using UnrealReplayReader.FortniteMinimal.Models.Exports;
using UnrealReplayReader.Models;
using UnrealReplayReader.Models.Enums;

namespace UnrealReplayReader.Tests;

public class FortniteMinimalReplayReaderTest
{
    [Fact]
    public void TestReadReplay()
    {
        var expectedMatch = new Match
        {
            Player = new Player
            {
                Id = "1d5838424f2e412db74121c5ae4b317f",
                DisplayName = "nmzlf0x"
            },
            Playlist = "Playlist_NoBuildBR_Squad",
            MatchId = "140acae7dcd84bba9ce020ab5f04ad70",
            MatchTime = new DateTime(638233574236320000)
        };
        var settings = new ReplayReaderSettings
        {
            IsDebug = true,
            Logger = null,
            UseCheckpoints = false,
            ExportConfiguration = ReplayExportConfiguration.FromAssembly(typeof(GameState))
        };
        var replay = FortniteMinimalReplayReader.FromFile("Replays/Chapter4_Season3_25.10.replay", settings);

        replay.Header.EngineVersion.Branch.Should()
            .Be("++Fortnite+Release-25.10");
        replay.Header.Version.Should()
            .Be(ENetworkVersionHistory.HistoryUseCustomVersion);
        replay.Header.Platform.Should()
            .Be("WindowsClient");
        replay.ExportGroupDict.Count.Should()
            .Be(806);
        expectedMatch.Should()
            .BeEquivalentTo(replay.Match);
    }
}