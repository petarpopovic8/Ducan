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
        public double sum { get; set; }

        public BillViewModel(bill bill)
        {
            id = bill.id;
            num = bill.num;
            user = bill.user;
            sum = 0;
            foreach (var art in bill.bill_article)
            {
                sum += art.article.price + art.amount;
            }
        }
    }
}
