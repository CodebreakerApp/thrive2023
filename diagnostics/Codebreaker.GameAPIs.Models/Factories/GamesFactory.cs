﻿using Codebreaker.GameAPIs.Exceptions;

namespace Codebreaker.GameAPIs.Factories;

public static class GamesFactory
{
    private static readonly string[] s_colors6 = { Colors.Red, Colors.Green, Colors.Blue, Colors.Yellow, Colors.Purple, Colors.Orange };
    private static readonly string[] s_colors8 = { Colors.Red, Colors.Green, Colors.Blue, Colors.Yellow, Colors.Purple, Colors.Orange, Colors.Pink, Colors.Brown };
    private static readonly string[] s_colors5 = { Colors.Red, Colors.Green, Colors.Blue, Colors.Yellow, Colors.Purple };
    private static readonly string[] s_shapes5 = { Shapes.Circle, Shapes.Square, Shapes.Triangle, Shapes.Star, Shapes.Rectangle };

    public static Game CreateGame(GameType gameType, string playerName)
    {
#if !NET8_0_OR_GREATER
        static string GetRandomValue(string[] items) => items[Random.Shared.Next(items.Length)];
#endif
        static SimpleGame Create6x4SimpleGame(GameType gameType, string playerName) =>
            new (Guid.NewGuid(), gameType, playerName, DateTime.Now, 4, 12)
            {
                Fields = s_colors6.Select(c => new ColorField(c)).ToArray(),
#if NET8_0_OR_GREATER
                Codes = Random.Shared.GetItems(s_colors6, 4).Select(c => new ColorField(c)).ToArray()
#else
                Codes = Enumerable.Range(0, 4).Select(i => new ColorField(GetRandomValue(s_colors6))).ToArray()
#endif
            };

        static ColorGame Create6x4Game(GameType gameType, string playerName) =>
            new(Guid.NewGuid(), gameType, playerName, DateTime.Now, 4, 12)
            {
                Fields = s_colors6.Select(c => new ColorField(c)).ToArray(),
#if NET8_0_OR_GREATER
                Codes = Random.Shared.GetItems(s_colors6, 4).Select(c => new ColorField(c)).ToArray()
#else
                Codes = Enumerable.Range(0, 4).Select(i => new ColorField(GetRandomValue(s_colors6))).ToArray()
#endif
            };

        static ColorGame Create8x5Game(GameType gameType, string playerName) =>
            new(Guid.NewGuid(), gameType, playerName, DateTime.Now, 5, 12)
            {
                Fields = s_colors8.Select(c => new ColorField(c)).ToArray(),
#if NET8_0_OR_GREATER
                Codes = Random.Shared.GetItems(s_colors8, 5).Select(c => new ColorField(c)).ToArray()
#else
                Codes = Enumerable.Range(0, 5).Select(
                    i => new ColorField(
                        GetRandomValue(s_colors8))).ToArray()
#endif
            };

        static ShapeGame Create5x5x4Game(GameType gameType, string playerName) =>
            new(Guid.NewGuid(), gameType, playerName, DateTime.Now, 4, 14)
            {
                Fields = Enumerable.Range(0, 5).Select(i => new ShapeAndColorField(s_shapes5[i], s_colors5[i])).ToArray(),
#if NET8_0_OR_GREATER
                Codes = Random.Shared.GetItems(s_shapes5, 4)
                    .Zip(Random.Shared.GetItems(s_colors5, 4), (s, c) => new ShapeAndColorField(s, c)).ToArray()    
#else
                Codes = Enumerable.Range(0, 4).Select(
                    i => new ShapeAndColorField(
                        GetRandomValue(s_shapes5),
                        GetRandomValue(s_colors5))).ToArray()
#endif
            };

        return gameType switch
        {
            GameType.Game6x4Mini => Create6x4SimpleGame(gameType, playerName),
            GameType.Game6x4 => Create6x4Game(gameType, playerName),
            GameType.Game8x5 => Create8x5Game(gameType, playerName),
            GameType.Game5x5x4 => Create5x5x4Game(gameType, playerName),
            _ => throw new InvalidGameException("Invalid game type") { HResult = 4000 }
        };
    }
}
