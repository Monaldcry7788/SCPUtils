﻿using System;
using CommandSystem;



namespace SCPUtils.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    public class HideBadge : ICommand
    {
        public string Command { get; } = "scputils_hide_badge";

        public string[] Aliases { get; } = new string[] { "hb" };

        public string Description { get; } = "Hides your badge permanently until you execute scputils_show_badge or sb";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!CommandExtensions.IsAllowed(((CommandSender)sender).SenderId, "scputils.badgevisibility"))
            {
                response = $"{ScpUtils.StaticInstance.Config.UnauthorizedBadgeChangeVisibility} ";
                return false;
            }
            else if (((CommandSender)sender).Nickname.Equals("SERVER CONSOLE"))
            {
                response = "This command cannot be executed from console!";
                return false;
            }
            else
            {
                var player = Exiled.API.Features.Player.Get(((CommandSender)sender).SenderId);
                player.BadgeHidden = true;
                player.GetDatabasePlayer().HideBadge = true;
                response = "<color=green>Your badge has been hidden!</color>";

                return true;
            }
        }
    }
}