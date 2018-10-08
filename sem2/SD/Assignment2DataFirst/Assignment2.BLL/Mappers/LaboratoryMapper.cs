using System;
using System.Collections.Generic;
using System.Text;
using Assignment2.BLL.Models;
using Assignment2.DAL;

namespace Assignment2.BLL.Mappers
{
    class LaboratoryMapper
    {
        public Laboratory map(LaboratoryModel laboratory)
        {
            if (laboratory == null)
                return null;
            var newLaboratory = new Laboratory() { ID = laboratory.ID, Curricula = laboratory.Curricula, Description = laboratory.Description, LabDate = laboratory.LabDate, Number = laboratory.Number, Title = laboratory.Title };
            return newLaboratory;
        }
        public LaboratoryModel map(Laboratory laboratory)
        {
            if (laboratory == null)
                return null;
            var newLaboratory = new LaboratoryModel() { ID = laboratory.ID, Curricula = laboratory.Curricula, Description = laboratory.Description, LabDate = laboratory.LabDate, Number = laboratory.Number, Title = laboratory.Title };
            return newLaboratory;
        }
    }
}
