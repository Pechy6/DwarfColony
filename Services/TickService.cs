namespace DwarfColony.Services;

public class TickService(ResourceProductionService resourceProductionService, DwarfTickService dwarfTickService)
{
    private readonly ResourceProductionService _resourceProductionService = resourceProductionService;
    private readonly DwarfTickService _dwarfTickService = dwarfTickService;

    public void Tick()
    {
        _resourceProductionService.ProduceManager();
        _dwarfTickService.Tick();
    }
}