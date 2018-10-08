using System;
using System.Collections.Generic;
using System.Text;
using Assignment2.BLL.Models;
using Assignment2.DAL;

namespace Assignment2.BLL.Contracts
{
    public interface ILaboratoryService
    {
        void Add(LaboratoryModel laboratoryModel);

        void DeleteLaboratory(LaboratoryModel laboratoryModel);

        List<LaboratoryModel> GetAll();

        LaboratoryModel GetById(int id);

        void UpdateLaboratory(LaboratoryModel laboratoryModel);
    }
}
