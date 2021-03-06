﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DwarfMine.Interfaces.Util
{
    public interface ILogService
    {
        void Info(string message);
        void Debug(string message);
        void Warn(string message);
        void Error(string message, Exception ex = null);
    }
}
