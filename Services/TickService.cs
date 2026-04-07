namespace DwarfColony.Services;

public class TickService(ResourceProductionService resourceProductionService, DwarfNeedsService dwarfNeedsService)
{
    private readonly ResourceProductionService _resourceProductionService = resourceProductionService;
    private readonly DwarfNeedsService _dwarfNeedsService = dwarfNeedsService;

    public void Tick()
    {
        _resourceProductionService.ProduceManager();
        _dwarfNeedsService.Tick();
    }
}