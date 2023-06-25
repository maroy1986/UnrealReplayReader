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
                Id = "795f8ecd0b7b466e818cbe1c2b3e66cc",
                DisplayName = "phoenix1074"
            },
            Playlist = "Playlist_NoBuildBR_Solo",
            MatchId = "140c02856903406fbd7ae310af14ba67",
            MatchTime = new DateTime(638219206017500000)
        };
        var settings = new ReplayReaderSettings
        {
            IsDebug = true,
            Logger = null,
            UseCheckpoints = false,
            ExportConfiguration = ReplayExportConfiguration.FromAssembly(typeof(GameState))
        };
        var replay = FortniteMinimalReplayReader.FromFile("Replays/Chapter4_Season5.replay", settings);

        replay.Header.EngineVersion.Branch.Should()
            .Be("++Fortnite+Release-25.00");
        replay.Header.Version.Should()
            .Be(ENetworkVersionHistory.HistoryUseCustomVersion);
        replay.Header.Platform.Should()
            .Be("WindowsClient");
        replay.ExportGroupDict.Count.Should()
            .Be(330);
        expectedMatch.Should()
            .BeEquivalentTo(replay.Match);
    }
}