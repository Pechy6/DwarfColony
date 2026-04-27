using DwarfColony.Data;
using DwarfColony.Models.Entities;
using DwarfColony.Models.Entities.Dwarfs;

namespace DwarfColony.Services;

public class DwarfStateService
{

    public void ChangeState(Dwarf dwarf)
    {
        if (dwarf.State == DwarfState.Sleeping)
        {
            return;
        }

        dwarf.State = GetStateByJob(dwarf);
    }

    public void RestoreStateAfterSleeping(Dwarf dwarf)
    {
        if (dwarf.State == DwarfState.Sleeping)
        {
            dwarf.State = GetStateByJob(dwarf);
        }
    }

private DwarfState GetStateByJob(Dwarf dwarf)
    {
        return dwarf.Job == DwarfJob.None
            ? DwarfState.Idle
            : DwarfState.Working;
    }
}