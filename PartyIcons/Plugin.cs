﻿using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using PartyIcons.Configuration;
using PartyIcons.Runtime;
using PartyIcons.Stylesheet;
using PartyIcons.UI;
using PartyIcons.View;

namespace PartyIcons;

public sealed class Plugin : IDalamudPlugin
{
    public static PartyStateTracker PartyStateTracker { get; private set; } = null!;
    public static PartyListHUDView PartyHudView { get; private set; } = null!;
    public static PartyListHUDUpdater PartyListHudUpdater { get; private set; } = null!;
    public static NameplateUpdater NameplateUpdater { get; private set; } = null!;
    public static NameplateView NameplateView { get; private set; } = null!;
    public static RoleTracker RoleTracker { get; private set; } = null!;
    public static ViewModeSetter ModeSetter { get; private set; } = null!;
    public static ChatNameUpdater ChatNameUpdater { get; private set; } = null!;
    public static ContextMenu ContextMenu { get; private set; } = null!;
    public static CommandHandler CommandHandler { get; private set; } = null!;
    public static Settings Settings { get; private set; } = null!;
    public static PlayerStylesheet PlayerStylesheet { get; private set; } = null!;
    public static WindowManager WindowManager { get; private set; } = null!;

    public Plugin(IDalamudPluginInterface pluginInterface)
    {
        pluginInterface.Create<Service>();

        Settings = Settings.Load();

        PlayerStylesheet = new PlayerStylesheet(Settings);

        PartyStateTracker = new PartyStateTracker();
        PartyHudView = new PartyListHUDView(PlayerStylesheet);
        RoleTracker = new RoleTracker(Settings, PartyStateTracker);
        NameplateView = new NameplateView(RoleTracker, Settings, PlayerStylesheet);
        ChatNameUpdater = new ChatNameUpdater(RoleTracker, PlayerStylesheet);
        PartyListHudUpdater = new PartyListHUDUpdater(PartyHudView, RoleTracker, Settings, PartyStateTracker);
        ModeSetter = new ViewModeSetter(NameplateView, Settings, ChatNameUpdater, PartyListHudUpdater);
        NameplateUpdater = new NameplateUpdater(NameplateView);
        ContextMenu = new ContextMenu(RoleTracker, Settings, PlayerStylesheet);
        CommandHandler = new CommandHandler();
        WindowManager = new WindowManager();

        PartyStateTracker.Enable();
        PartyListHudUpdater.Enable();
        ModeSetter.Enable();
        RoleTracker.Enable();
        NameplateUpdater.Enable();
        ChatNameUpdater.Enable();
    }

    public void Dispose()
    {
        PartyStateTracker.Dispose();
        PartyHudView.Dispose();
        PartyListHudUpdater.Dispose();
        ChatNameUpdater.Dispose();
        ContextMenu.Dispose();
        NameplateUpdater.Dispose();
        RoleTracker.Dispose();
        ModeSetter.Dispose();
        CommandHandler.Dispose();
        WindowManager.Dispose();
    }
}