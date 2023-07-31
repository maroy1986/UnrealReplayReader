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
            MatchId = "fbc5f85bca534a4eaa90f349a279816f",
            MatchTime = TimeZoneInfo.ConvertTime(new DateTime(638262936271150000), TimeZoneInfo.FindSystemTimeZoneById("America/Toronto"))
        };
        var settings = new ReplayReaderSettings
        {
            IsDebug = true,
            Logger = null,
            UseCheckpoints = false,
            ExportConfiguration = ReplayExportConfiguration.FromAssembly(typeof(GameState))
        };
        var replay = FortniteMinimalReplayReader.FromFile("Replays/CH04SE3_25_20.replay", settings);

        replay.Header.EngineVersion.Branch.Should()
            .Be("++Fortnite+Release-25.20");
        replay.Header.Version.Should()
            .Be(ENetworkVersionHistory.HistoryUseCustomVersion);
        replay.Header.Platform.Should()
            .Be("WindowsClient");
        replay.ExportGroupDict.Count.Should()
            .Be(466);
        expectedMatch.Should()
            .BeEquivalentTo(replay.Match);
    }
}