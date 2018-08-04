using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace ServiceLayer
{
    public class UserService
    {
        UnitOfWork _unitOfWork;

        public UserService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public IEnumerable<user> GetAll()
        {
            return _unitOfWork.UserRepository.Get();
        }

        public user FindById(int id)
        {
            return _unitOfWork.UserRepository.GetByID(id);
        }

        public void Create(user user)
        {
            _unitOfWork.UserRepository.Insert(user);
            _unitOfWork.Save();
        }

        public void Update(user user)
        {
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Save();
        }


        public void Delete(user user)
        {
            _unitOfWork.UserRepository.Delete(user);
            _unitOfWork.Save();
        }

        public user Login(user user)
        {
            return _unitOfWork.UserRepository.Get(x => x.full_name == user.full_name && x.password == user.password).FirstOrDefault();
        }

        public void Register(user user)
        {
            _unitOfWork.UserRepository.Insert(user);
            _unitOfWork.Save();
        }
    }
}
