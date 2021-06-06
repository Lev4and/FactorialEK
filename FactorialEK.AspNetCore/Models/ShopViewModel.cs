using FactorialEK.Model.Database.Entities;
using FactorialEK.Model.Formatters.String;
using System;
using System.Collections.Generic;

namespace FactorialEK.AspNetCore.Models
{
    public class ShopViewModel
    {
        public int NumberPage { get; set; }

        public int CountItems { get; set; }

        public int ItemsPerPage { get; set; }

        public Guid CategoryId { get; set; }

        public List<int> Pagination { get; set; }

        public List<ManufacturingOrService> ManufacturingOrServices { get; set; }

        public string GetFormattedStringRange()
        {
            var from = 0;
            var to = 0;

            if(NumberPage > 1)
            {
                from = (NumberPage - 1) * ItemsPerPage;
                to = NumberPage * ItemsPerPage <= CountItems ? NumberPage * ItemsPerPage : CountItems;
            }
            else
            {
                if(CountItems > 0)
                {
                    from = 1;
                    to = NumberPage * ItemsPerPage <= CountItems ? NumberPage * ItemsPerPage : CountItems;
                }
                else
                {
                    from = 0;
                    to = 0;
                }
            }

            return $"Показано {from}-{to} из {CountItems} {Declension.DeclensionByNumeral(CountItems, new string[3] { "результата", "результатов", "результатов" }, false)}";
        }

        public void GeneratePagination()
        {
            Pagination = new List<int>();

            var pages = CountItems % ItemsPerPage != 0 ? (CountItems / ItemsPerPage) + 1 : CountItems / ItemsPerPage;
            var from = 0;
            var to = 0;

            if (NumberPage > 1 && NumberPage < pages)
            {
                from = NumberPage - 1;
                to = NumberPage + 1;
            }
            else
            {
                if(NumberPage == 1)
                {
                    from = 1;
                    to = pages > 3 ? 3 : pages;
                }
                else 
                {
                    from = pages - 3 > 0 ? pages - 3 : 1;
                    to = pages;
                }
            }

            for(int i = from; i <= to; i++)
            {
                Pagination.Add(i);
            }
        }
    }
}
