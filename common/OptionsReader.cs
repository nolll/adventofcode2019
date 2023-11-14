﻿using Microsoft.Extensions.Configuration;

namespace Puzzles.common;

public static class OptionsReader
{
    public static Options Read()
    {
        var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, false);
        var configuration = configurationBuilder.Build();

        return new Options(
            configuration["hashSeed"],
            configuration["timeoutSeconds"],
            configuration["debugPuzzle"]);
    }
}