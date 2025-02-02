﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SEMS_APP.Interface
{
    public interface IProcessLoader
    {
        Task Hide();
        Task Show(string title = "Loading");
    }
}
