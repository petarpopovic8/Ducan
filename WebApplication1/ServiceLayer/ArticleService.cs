using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ArticleService
    {
        UnitOfWork _unitOfWork;

        public ArticleService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public IEnumerable<article> GetAll()
        {
            return _unitOfWork.ArticleRepository.Get(filter: null, orderBy: null, includeProperties: "firm");
        }

        public article FindById(int id)
        {
            return _unitOfWork.ArticleRepository.GetByID(id);
        }

        public void Create(article article)
        {
            article.sumvalue = article.price * article.amount * (1 + article.tax);
            _unitOfWork.ArticleRepository.Insert(article);
            _unitOfWork.Save();
        }

        public void Update(article article)
        {
            article.sumvalue = article.price * article.amount * (1 + article.tax);
            _unitOfWork.ArticleRepository.Update(article);
            _unitOfWork.Save();
        }


        public void Delete(article article)
        {
            _unitOfWork.ArticleRepository.Delete(article);
            _unitOfWork.Save();
        }

        public IEnumerable<article> GetSorted(string sort)
        {
            switch (sort)
            {
                case "name":
                    return _unitOfWork.ArticleRepository.Get(orderBy: q => q.OrderBy(d => d.name));
                case "name_desc":
                    return _unitOfWork.ArticleRepository.Get(orderBy: q => q.OrderByDescending(d => d.name));
                case "firm":
                    return _unitOfWork.ArticleRepository.Get(orderBy: q => q.OrderBy(d => d.firm.name));
                case "firm_desc":
                    return _unitOfWork.ArticleRepository.Get(orderBy: q => q.OrderByDescending(d => d.firm.name));
                case "amount":
                    return _unitOfWork.ArticleRepository.Get(orderBy: q => q.OrderBy(d => d.amount));
                case "amount_desc":
                    return _unitOfWork.ArticleRepository.Get(orderBy: q => q.OrderByDescending(d => d.amount));
                case "price":
                    return _unitOfWork.ArticleRepository.Get(orderBy: q => q.OrderBy(d => d.price));
                case "price_desc":
                    return _unitOfWork.ArticleRepository.Get(orderBy: q => q.OrderByDescending(d => d.price));
                default:
                    return _unitOfWork.ArticleRepository.Get(orderBy: q => q.OrderBy(d => d.name));

            }
            return null;
        }
    }
}

