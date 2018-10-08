using System;
using System.Collections.Generic;
using System.Text;
using Assignment2.BLL.Contracts;
using Assignment2.BLL.Models;
using Assignment2.BLL.Mappers;
using Assignment2.DAL;
using Assignment2.DAL.Contracts;
using Assignment2.DAL.Repositories;

namespace Assignment2.BLL.Services
{
    public class LaboratoryService :ILaboratoryService
    {
        private ILaboratoryRepository laboratoryRepository;
        private LaboratoryMapper mapper = new LaboratoryMapper();
        public LaboratoryService(ILaboratoryRepository laboratoryRepository)
        {
            this.laboratoryRepository = laboratoryRepository;
        }

        //public AttendanceService()
        //{
        //    attendanceRepository = new AttendanceRepository();
        //}

        public void Add(LaboratoryModel laboratoryModel)
        {
            var newLaboratory = mapper.map(laboratoryModel);
            laboratoryRepository.Add(newLaboratory);
        }

        public void DeleteLaboratory(LaboratoryModel laboratoryModel)
        {
            laboratoryRepository.Delete(laboratoryModel.ID);
        }

        public List<LaboratoryModel> GetAll()
        {
            var res = laboratoryRepository.GetAll();
            List<LaboratoryModel> laboratoryModels = new List<LaboratoryModel>();
            foreach (Laboratory l in res)
                laboratoryModels.Add(mapper.map(l));
            return laboratoryModels;
        }

        public LaboratoryModel GetById(int id)
        {
            return mapper.map(laboratoryRepository.GetById(id));
        }

        public void UpdateLaboratory(LaboratoryModel laboratoryModel)
        {
            laboratoryRepository.Update(mapper.map(laboratoryModel));
        }
    }
}
