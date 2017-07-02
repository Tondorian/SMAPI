﻿using StardewModdingAPI;
using StardewValley;

namespace TrainerMod.Framework.Commands.Other
{
    /// <summary>A command which sends a debug command to the game.</summary>
    internal class DebugCommand : TrainerCommand
    {
        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        public DebugCommand()
            : base("debug", "Run one of the game's debug commands; for example, 'debug warp FarmHouse 1 1' warps the player to the farmhouse.") { }

        /// <summary>Handle the command.</summary>
        /// <param name="monitor">Writes messages to the console and log file.</param>
        /// <param name="command">The command name.</param>
        /// <param name="args">The command arguments.</param>
        public override void Handle(IMonitor monitor, string command, string[] args)
        {
            // submit command
            string debugCommand = string.Join(" ", args);
            string oldOutput = Game1.debugOutput;
            Game1.game1.parseDebugInput(debugCommand);

            // show result
            monitor.Log(Game1.debugOutput != oldOutput
                ? $"> {Game1.debugOutput}"
                : "Sent debug command to the game, but there was no output.", LogLevel.Info);
        }
    }
}
