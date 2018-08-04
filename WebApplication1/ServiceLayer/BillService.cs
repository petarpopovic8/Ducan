using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class BillService
    {
        UnitOfWork _unitOfWork;

        public BillService()
        {
            _unitOfWork = new UnitOfWork();
        }


        public IEnumerable<bill> GetAll()
        {
            return _unitOfWork.BillRepository.Get( filter : null, orderBy: null, includeProperties: "article,user");
        }

        public IEnumerable<bill> GetByNum(int num)
        {
            return _unitOfWork.BillRepository.Get(x => x.num == num);
        }

        public bill FindById(int id)
        {
            return _unitOfWork.BillRepository.GetByID(id);
        }

        public void Create(bill bill)
        {
            _unitOfWork.BillRepository.Insert(bill);
            _unitOfWork.Save();
        }

        public void Delete(bill bill)
        {
            _unitOfWork.BillRepository.Delete(bill);
            _unitOfWork.Save();
        }
    }
}

