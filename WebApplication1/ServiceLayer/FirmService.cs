using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class FirmService
    {
        UnitOfWork _unitOfWork;

        public FirmService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public IEnumerable<firm> GetAll()
        {
            return _unitOfWork.FirmRepository.Get();
        }

        public firm FindById(int id)
        {
            return _unitOfWork.FirmRepository.GetByID(id);
        }

        public void Create(firm firm)
        {
            _unitOfWork.FirmRepository.Insert(firm);
            _unitOfWork.Save();
        }

        public void Update(firm firm)
        {
            _unitOfWork.FirmRepository.Update(firm);
            _unitOfWork.Save();
        }


        public void Delete(firm firm)
        {
            _unitOfWork.FirmRepository.Delete(firm);
            _unitOfWork.Save();
        }
    }
}

