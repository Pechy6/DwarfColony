namespace DwarfColony.Services;

public class TickService(ResourceProductionService resourceProductionService, DwarfNeedsService dwarfNeedsService, DwarfRecoveryService dwarfRecoveryService)
{
    private readonly ResourceProductionService _resourceProductionService = resourceProductionService;
    private readonly DwarfNeedsService _dwarfNeedsService = dwarfNeedsService;
    private readonly DwarfRecoveryService _dwarfRecoveryService = dwarfRecoveryService;

    public void Tick()
    {
        _resourceProductionService.ProduceManager();
        _dwarfNeedsService.Tick();
        _dwarfRecoveryService.HandleAutomaticRecovery();
        _dwarfRecoveryService.HandleSleepTick();
    }
}