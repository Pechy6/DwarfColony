using DwarfColony.Models.Entities.Dwarfs;
using DwarfColony.Models.Entities.World;

namespace DwarfColony.Services;

public class TickService(ResourceProductionService resourceProductionService, DwarfNeedsService dwarfNeedsService, DwarfRecoveryService dwarfRecoveryService, DwarfMoveToArea dwarfMoveToArea)
{
    private readonly ResourceProductionService _resourceProductionService = resourceProductionService;
    private readonly DwarfNeedsService _dwarfNeedsService = dwarfNeedsService;
    private readonly DwarfRecoveryService _dwarfRecoveryService = dwarfRecoveryService;
    private readonly DwarfMoveToArea _dwarfMoveToArea = dwarfMoveToArea;

    public void Tick()
    {
        _resourceProductionService.ProduceManager();
        _dwarfNeedsService.Tick();
        _dwarfRecoveryService.HandleAutomaticRecovery();
        _dwarfRecoveryService.HandleSleepTick();
        
    }
}