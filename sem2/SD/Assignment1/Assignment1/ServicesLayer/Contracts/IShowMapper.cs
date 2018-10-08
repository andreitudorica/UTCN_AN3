using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.ServicesLayer.Contracts
{
    interface IShowMapper
    {
        DataLayer.Models.ShowDTO map(ServicesLayer.Models.ShowModel model);
        ServicesLayer.Models.ShowModel map(DataLayer.Models.ShowDTO dto);
    }
}
