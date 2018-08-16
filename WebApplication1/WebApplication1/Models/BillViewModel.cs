using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class BillViewModel
    {
        public int id { get; set; }
        public int num { get; set; }
        public user user { get; set; }
        public string sum { get; set; }
        public IEnumerable<bill_article> bill_article { get; set; }
        //public IEnumerable<article> articles { get; set; }

        public BillViewModel(bill bill)
        {
            id = bill.id;
            num = bill.num;
            user = bill.user;
            double suma = 0;
            bill_article = bill.bill_article;
            foreach (var art in bill.bill_article)
            {
                suma += art.article.price * art.amount * (1 + art.article.tax);
            }
            sum = suma.ToString("0.00");
        }
    }
}
