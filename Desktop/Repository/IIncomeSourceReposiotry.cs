﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Desktop;

namespace DAL.Repository
{
    public interface IIncomeSourceReposiotry:IRepository<IncomeSource>
    {
        IncomeSource GetById(int id);
        IncomeSource GetByName(string name);
    }
}