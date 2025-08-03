using System;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.Handlers;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace SCriPt.LabAPI.Handlers;

[MoonSharpUserData]
public class LuaServerEvents : ILuaEventHandler
{ 
    
    [MoonSharpVisible(true)] public event EventHandler WaitingForPlayers;

    [MoonSharpVisible(true)] public event EventHandler RoundRestarted;

    [MoonSharpVisible(true)] public event EventHandler<RoundEndingEventArgs> RoundEnding;

    [MoonSharpVisible(true)] public event EventHandler<RoundEndedEventArgs> RoundEnded;

    [MoonSharpVisible(true)] public event EventHandler<RoundStartingEventArgs> RoundStarting;

    [MoonSharpVisible(true)] public event EventHandler RoundStarted;

    [MoonSharpVisible(true)] public event EventHandler<BanIssuingEventArgs> BanIssuing;

    [MoonSharpVisible(true)] public event EventHandler<BanIssuedEventArgs> BanIssued;

    [MoonSharpVisible(true)] public event EventHandler<BanRevokingEventArgs> BanRevoking;

    [MoonSharpVisible(true)] public event EventHandler<BanRevokedEventArgs> BanRevoked;

    [MoonSharpVisible(true)] public event EventHandler<BanUpdatingEventArgs> BanUpdating;

    [MoonSharpVisible(true)] public event EventHandler<BanUpdatedEventArgs> BanUpdated;

    [MoonSharpVisible(true)] public event EventHandler<CommandExecutingEventArgs> CommandExecuting;

    [MoonSharpVisible(true)] public event EventHandler<CommandExecutedEventArgs> CommandExecuted;

    [MoonSharpVisible(true)] public event EventHandler<CassieQueuingScpTerminationEventArgs> CassieQueuingScpTermination;

    [MoonSharpVisible(true)] public event EventHandler<CassieQueuedScpTerminationEventArgs> CassieQueuedScpTermination;

    [MoonSharpVisible(true)] public event EventHandler<WaveRespawningEventArgs> WaveRespawning;

    [MoonSharpVisible(true)] public event EventHandler<WaveRespawnedEventArgs> WaveRespawned;

    [MoonSharpVisible(true)] public event EventHandler<WaveTeamSelectingEventArgs> WaveTeamSelecting;

    [MoonSharpVisible(true)] public event EventHandler<WaveTeamSelectedEventArgs> WaveTeamSelected;

    [MoonSharpVisible(true)] public event EventHandler<LczDecontaminationAnnouncedEventArgs> LczDecontaminationAnnounced;

    [MoonSharpVisible(true)] public event EventHandler<LczDecontaminationStartingEventArgs> LczDecontaminationStarting;

    [MoonSharpVisible(true)] public event EventHandler LczDecontaminationStarted;

    [MoonSharpVisible(true)] public event EventHandler<MapGeneratingEventArgs> MapGenerating;

    [MoonSharpVisible(true)] public event EventHandler<MapGeneratedEventArgs> MapGenerated;

    [MoonSharpVisible(true)] public event EventHandler<PickupCreatedEventArgs> PickupCreated;

    [MoonSharpVisible(true)] public event EventHandler<PickupDestroyedEventArgs> PickupDestroyed;

    [MoonSharpVisible(true)] public event EventHandler<SendingAdminChatEventArgs> SendingAdminChat;

    [MoonSharpVisible(true)] public event EventHandler<SentAdminChatEventArgs> SentAdminChat;

    [MoonSharpVisible(true)] public event EventHandler<ItemSpawningEventArgs> ItemSpawning;

    [MoonSharpVisible(true)] public event EventHandler<ItemSpawnedEventArgs> ItemSpawned;

    [MoonSharpVisible(true)] public event EventHandler<CassieAnnouncingEventArgs> CassieAnnouncing;

    [MoonSharpVisible(true)] public event EventHandler<CassieAnnouncedEventArgs> CassieAnnounced;

    [MoonSharpVisible(true)] public event EventHandler<ProjectileExplodingEventArgs> ProjectileExploding;

    [MoonSharpVisible(true)] public event EventHandler<ProjectileExplodedEventArgs> ProjectileExploded;

    [MoonSharpVisible(true)] public event EventHandler<ExplosionSpawningEventArgs> ExplosionSpawning;

    [MoonSharpVisible(true)] public event EventHandler<ExplosionSpawnedEventArgs> ExplosionSpawned;

    [MoonSharpVisible(true)] public event EventHandler<GeneratorActivatingEventArgs> GeneratorActivating;

    [MoonSharpVisible(true)] public event EventHandler<GeneratorActivatedEventArgs> GeneratorActivated;
      
    [MoonSharpVisible(true)] public event EventHandler<RoundEndingConditionsCheckEventArgs> RoundEndingConditionsCheck;
    // ElevatorSequenceChanged
    [MoonSharpVisible(true)] public event EventHandler<ElevatorSequenceChangedEventArgs> ElevatorSequenceChanged;
    
    
    
    
    [MoonSharpHidden]
    public void OnWaitingForPlayers()
    {
        WaitingForPlayers?.Invoke(null, EventArgs.Empty);
    }

    [MoonSharpHidden]
    public void OnRoundRestarted()
    {
        RoundRestarted?.Invoke(null, EventArgs.Empty);
    }

    [MoonSharpHidden]
    public void OnRoundEnding(RoundEndingEventArgs ev)
    {
        RoundEnding?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnRoundEnded(RoundEndedEventArgs ev)
    {
        RoundEnded?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnRoundStarting(RoundStartingEventArgs ev)
    {
        RoundStarting?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnRoundStarted()
    {
        RoundStarted?.Invoke(null, EventArgs.Empty);
    }

    [MoonSharpHidden]
    public void OnBanIssuing(BanIssuingEventArgs ev)
    {
        BanIssuing?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnBanIssued(BanIssuedEventArgs ev)
    {
        BanIssued?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnBanRevoking(BanRevokingEventArgs ev)
    {
        BanRevoking?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnBanRevoked(BanRevokedEventArgs ev)
    {
        BanRevoked?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnBanUpdating(BanUpdatingEventArgs ev)
    {
        BanUpdating?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnBanUpdated(BanUpdatedEventArgs ev)
    {
        BanUpdated?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnCommandExecuting(CommandExecutingEventArgs ev)
    {
        CommandExecuting?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnCommandExecuted(CommandExecutedEventArgs ev)
    {
        CommandExecuted?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnCassieQueuingScpTermination(CassieQueuingScpTerminationEventArgs ev)
    {
        CassieQueuingScpTermination?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnCassieQueuedScpTermination(CassieQueuedScpTerminationEventArgs ev)
    {
        CassieQueuedScpTermination?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnWaveRespawning(WaveRespawningEventArgs ev)
    {
        WaveRespawning?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnWaveRespawned(WaveRespawnedEventArgs ev)
    {
        WaveRespawned?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnWaveTeamSelecting(WaveTeamSelectingEventArgs ev)
    {
        WaveTeamSelecting?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnWaveTeamSelected(WaveTeamSelectedEventArgs ev)
    {
        WaveTeamSelected?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnLczDecontaminationAnnounced(LczDecontaminationAnnouncedEventArgs ev)
    {
        LczDecontaminationAnnounced?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnLczDecontaminationStarting(LczDecontaminationStartingEventArgs ev)
    {
        LczDecontaminationStarting?.Invoke(null, ev);
    }   
    
    [MoonSharpHidden]
    public void OnLczDecontaminationStarted()
    {
        LczDecontaminationStarted?.Invoke(null, EventArgs.Empty);
    }

    [MoonSharpHidden]
    public void OnMapGenerating(MapGeneratingEventArgs ev)
    {
        MapGenerating?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnMapGenerated(MapGeneratedEventArgs ev)
    {
        MapGenerated?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnPickupCreated(PickupCreatedEventArgs ev)
    {
        PickupCreated?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnPickupDestroyed(PickupDestroyedEventArgs ev)
    {
        PickupDestroyed?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnSendingAdminChat(SendingAdminChatEventArgs ev)
    {
        SendingAdminChat?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnSentAdminChat(SentAdminChatEventArgs ev)
    {
        SentAdminChat?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnItemSpawning(ItemSpawningEventArgs ev)
    {
        ItemSpawning?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnItemSpawned(ItemSpawnedEventArgs ev)
    {
        ItemSpawned?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnCassieAnnouncing(CassieAnnouncingEventArgs ev)
    {
        CassieAnnouncing?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnCassieAnnounced(CassieAnnouncedEventArgs ev)
    {
        CassieAnnounced?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnProjectileExploding(ProjectileExplodingEventArgs ev)
    {
        ProjectileExploding?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnProjectileExploded(ProjectileExplodedEventArgs ev)
    {
        ProjectileExploded?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnExplosionSpawning(ExplosionSpawningEventArgs ev)
    {
        ExplosionSpawning?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnExplosionSpawned(ExplosionSpawnedEventArgs ev)
    {
        ExplosionSpawned?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnGeneratorActivating(GeneratorActivatingEventArgs ev)
    {
        GeneratorActivating?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void OnGeneratorActivated(GeneratorActivatedEventArgs ev)
    {
        GeneratorActivated?.Invoke(null, ev);
    }
    
    [MoonSharpHidden]
    public void OnRoundEndingConditionsCheck(RoundEndingConditionsCheckEventArgs ev)
    {
        RoundEndingConditionsCheck?.Invoke(null, ev);
    }

    [MoonSharpHidden]
    public void RegisterEventTypes()
    {
        UserData.RegisterType<RoundEndingEventArgs>();
        UserData.RegisterType<RoundEndedEventArgs>();
        UserData.RegisterType<RoundStartingEventArgs>();
        UserData.RegisterType<BanIssuingEventArgs>();
        UserData.RegisterType<BanIssuedEventArgs>();
        UserData.RegisterType<BanRevokingEventArgs>();
        UserData.RegisterType<BanRevokedEventArgs>();
        UserData.RegisterType<BanUpdatingEventArgs>();
        UserData.RegisterType<BanUpdatedEventArgs>();
        UserData.RegisterType<CommandExecutingEventArgs>();
        UserData.RegisterType<CommandExecutedEventArgs>();
        UserData.RegisterType<CassieQueuingScpTerminationEventArgs>();
        UserData.RegisterType<CassieQueuedScpTerminationEventArgs>();
        UserData.RegisterType<WaveRespawningEventArgs>();
        UserData.RegisterType<WaveRespawnedEventArgs>();
        UserData.RegisterType<WaveTeamSelectingEventArgs>();
        UserData.RegisterType<WaveTeamSelectedEventArgs>();
        UserData.RegisterType<LczDecontaminationAnnouncedEventArgs>();
        UserData.RegisterType<LczDecontaminationStartingEventArgs>();
        UserData.RegisterType<MapGeneratingEventArgs>();
        UserData.RegisterType<MapGeneratedEventArgs>();
        UserData.RegisterType<PickupCreatedEventArgs>();
        UserData.RegisterType<PickupDestroyedEventArgs>();
        UserData.RegisterType<SendingAdminChatEventArgs>();
        UserData.RegisterType<SentAdminChatEventArgs>();
        UserData.RegisterType<ItemSpawningEventArgs>();
        UserData.RegisterType<ItemSpawnedEventArgs>();
        UserData.RegisterType<CassieAnnouncingEventArgs>();
        UserData.RegisterType<CassieAnnouncedEventArgs>();
        UserData.RegisterType<ProjectileExplodingEventArgs>();
        UserData.RegisterType<ProjectileExplodedEventArgs>();
        UserData.RegisterType<ExplosionSpawningEventArgs>();
        UserData.RegisterType<ExplosionSpawnedEventArgs>();
        UserData.RegisterType<GeneratorActivatingEventArgs>();
        UserData.RegisterType<GeneratorActivatedEventArgs>();
        UserData.RegisterType<RoundEndingConditionsCheckEventArgs>();
    }
    
    
    [MoonSharpHidden]
    public void RegisterEvents()
    {
        ServerEvents.WaitingForPlayers += OnWaitingForPlayers;
        ServerEvents.RoundRestarted += OnRoundRestarted;
        ServerEvents.RoundEnding += OnRoundEnding;
        ServerEvents.RoundEnded += OnRoundEnded;
        ServerEvents.RoundStarting += OnRoundStarting;
        ServerEvents.RoundStarted += OnRoundStarted;
        ServerEvents.BanIssuing += OnBanIssuing;
        ServerEvents.BanIssued += OnBanIssued;
        ServerEvents.BanRevoking += OnBanRevoking;
        ServerEvents.BanRevoked += OnBanRevoked;
        ServerEvents.BanUpdating += OnBanUpdating;
        ServerEvents.BanUpdated += OnBanUpdated;
        ServerEvents.CommandExecuting += OnCommandExecuting;
        ServerEvents.CommandExecuted += OnCommandExecuted;
        ServerEvents.CassieQueuingScpTermination += OnCassieQueuingScpTermination;
        ServerEvents.CassieQueuedScpTermination += OnCassieQueuedScpTermination;
        ServerEvents.WaveRespawning += OnWaveRespawning;
        ServerEvents.WaveRespawned += OnWaveRespawned;
        ServerEvents.WaveTeamSelecting += OnWaveTeamSelecting;
        ServerEvents.WaveTeamSelected += OnWaveTeamSelected;
        ServerEvents.LczDecontaminationAnnounced += OnLczDecontaminationAnnounced;
        ServerEvents.LczDecontaminationStarting += OnLczDecontaminationStarting;
        ServerEvents.LczDecontaminationStarted += OnLczDecontaminationStarted;
        ServerEvents.MapGenerating += OnMapGenerating;
        ServerEvents.MapGenerated += OnMapGenerated;
        ServerEvents.PickupCreated += OnPickupCreated;
        ServerEvents.PickupDestroyed += OnPickupDestroyed;
        ServerEvents.SendingAdminChat += OnSendingAdminChat;
        ServerEvents.SentAdminChat += OnSentAdminChat;
        ServerEvents.ItemSpawning += OnItemSpawning;
        ServerEvents.ItemSpawned += OnItemSpawned;
        ServerEvents.CassieAnnouncing += OnCassieAnnouncing;
        ServerEvents.CassieAnnounced += OnCassieAnnounced;
        ServerEvents.ProjectileExploding += OnProjectileExploding;
        ServerEvents.ProjectileExploded += OnProjectileExploded;
        ServerEvents.ExplosionSpawning += OnExplosionSpawning;
        ServerEvents.ExplosionSpawned += OnExplosionSpawned;
        ServerEvents.GeneratorActivating += OnGeneratorActivating;
        ServerEvents.GeneratorActivated += OnGeneratorActivated;
        ServerEvents.RoundEndingConditionsCheck += OnRoundEndingConditionsCheck;
    }

    [MoonSharpHidden]
    public void UnregisterEvents()
    {
        ServerEvents.WaitingForPlayers -= OnWaitingForPlayers;
        ServerEvents.RoundRestarted -= OnRoundRestarted;
        ServerEvents.RoundEnding -= OnRoundEnding;
        ServerEvents.RoundEnded -= OnRoundEnded;
        ServerEvents.RoundStarting -= OnRoundStarting;
        ServerEvents.RoundStarted -= OnRoundStarted;
        ServerEvents.BanIssuing -= OnBanIssuing;
        ServerEvents.BanIssued -= OnBanIssued;
        ServerEvents.BanRevoking -= OnBanRevoking;
        ServerEvents.BanRevoked -= OnBanRevoked;
        ServerEvents.BanUpdating -= OnBanUpdating;
        ServerEvents.BanUpdated -= OnBanUpdated;
        ServerEvents.CommandExecuting -= OnCommandExecuting;
        ServerEvents.CommandExecuted -= OnCommandExecuted;
        ServerEvents.CassieQueuingScpTermination -= OnCassieQueuingScpTermination;
        ServerEvents.CassieQueuedScpTermination -= OnCassieQueuedScpTermination;
        ServerEvents.WaveRespawning -= OnWaveRespawning;
        ServerEvents.WaveRespawned -= OnWaveRespawned;
        ServerEvents.WaveTeamSelecting -= OnWaveTeamSelecting;
        ServerEvents.WaveTeamSelected -= OnWaveTeamSelected;
        ServerEvents.LczDecontaminationAnnounced -= OnLczDecontaminationAnnounced;
        ServerEvents.LczDecontaminationStarting -= OnLczDecontaminationStarting;
        ServerEvents.LczDecontaminationStarted -= OnLczDecontaminationStarted;
        ServerEvents.MapGenerating -= OnMapGenerating;
        ServerEvents.MapGenerated -= OnMapGenerated;
        ServerEvents.PickupCreated -= OnPickupCreated;
        ServerEvents.PickupDestroyed -= OnPickupDestroyed;
        ServerEvents.SendingAdminChat -= OnSendingAdminChat;
        ServerEvents.SentAdminChat -= OnSentAdminChat;
        ServerEvents.ItemSpawning -= OnItemSpawning;
        ServerEvents.ItemSpawned -= OnItemSpawned;
        ServerEvents.CassieAnnouncing -= OnCassieAnnouncing;
        ServerEvents.CassieAnnounced -= OnCassieAnnounced;
        ServerEvents.ProjectileExploding -= OnProjectileExploding;
        ServerEvents.ProjectileExploded -= OnProjectileExploded;
        ServerEvents.ExplosionSpawning -= OnExplosionSpawning;
        ServerEvents.ExplosionSpawned -= OnExplosionSpawned;
        ServerEvents.GeneratorActivating -= OnGeneratorActivating;
        ServerEvents.GeneratorActivated -= OnGeneratorActivated;
        ServerEvents.RoundEndingConditionsCheck -= OnRoundEndingConditionsCheck;
    }

}