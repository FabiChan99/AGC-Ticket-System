﻿#region

using AGC_Ticket;
using DisCatSharp;
using DisCatSharp.CommandsNext;
using DisCatSharp.Entities;
using DisCatSharp.Enums;
using DisCatSharp.EventArgs;

#endregion

namespace AGC_Ticket_System.Eventlistener;

[EventHandler]
public class DmWarning : BaseCommandModule
{
    [Event]
    private Task MessageCreated(DiscordClient client, MessageCreateEventArgs args)
    {
        _ = Task.Run(async () =>
            {
                if (args.Channel.Type == ChannelType.Private && !args.Message.Author.IsBot)
                {
                    string supportlink = BotConfig.GetConfig()["SupportConfig"]["SupportLink"];
                    List<DiscordLinkButtonComponent> supportbutton = new(1)
                    {
                        new DiscordLinkButtonComponent(supportlink, "Zum Support")
                    };
                    DiscordEmbed embed = new DiscordEmbedBuilder().WithTitle("AGC Support-System")
                        .WithDescription(
                            "Support wird nicht mehr per DM bearbeitet. \nBitte nutze den untenstehenden Button um zum Support zu gelangen!")
                        .WithColor(BotConfig.GetEmbedColor());
                    var msgbuilder = new DiscordMessageBuilder().AddComponents(supportbutton)
                        .WithEmbed(embed);
                    await args.Message.RespondAsync(msgbuilder);
                }
            }
        );
        return Task.CompletedTask;
    }
}