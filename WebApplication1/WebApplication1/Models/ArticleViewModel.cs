using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ArticleViewModel
    {
        public int id { get; set; }
        public int firm_id { get; set; }
        public string name { get; set; }
        public string amount { get; set; }
        public string price { get; set; }
        public string sumvalue { get; set; }
        public string tax { get; set; }
        public firm firm { get; set; }

        public ArticleViewModel(article article)
        {
            id = article.id;
            firm_id = article.firm_id;
            name = article.name;
            amount = article.amount.ToString();
            price = article.price.ToString("0.00");
            sumvalue = article.sumvalue.ToString("0.00");
            tax = article.tax.ToString("0.00");
            firm = article.firm;
        }

        public article ToArticle()
        {
            article article = new article();
            article.id = id;
            article.firm_id = firm_id;
            article.name = name;
            article.amount = double.Parse(amount);
            article.price = double.Parse(price);
            article.sumvalue = double.Parse(sumvalue);
            article.tax = double.Parse(tax);
            article.firm = firm;
            return article;
        }
    }
}