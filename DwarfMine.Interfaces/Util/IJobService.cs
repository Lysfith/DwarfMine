using DwarfMine.Interfaces.Util.Base;
using DwarfMine.Models.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Interfaces.Util
{
    public interface IJobService: IUpdateService
    {
        Job AddJob(Action action);
        int GetJobsCount();
        int GetActiveJobsCount();
    }
}
