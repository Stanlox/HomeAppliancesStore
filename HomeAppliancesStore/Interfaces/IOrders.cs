﻿using HomeAppliancesStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Interfaces
{
    public interface IOrders
    {
        void CreateOrder(Order order);
    }
}
