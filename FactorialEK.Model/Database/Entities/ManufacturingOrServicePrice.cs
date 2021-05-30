using System;
using System.Globalization;

namespace FactorialEK.Model.Database.Entities
{
    public class ManufacturingOrServicePrice
    {
        public Guid Id { get; set; }
        
        public Guid ManufacturingOrServiceId { get; set; }
        
        public int? SecondPrice { get; set; }
        
        public int? LowerLimitPrice { get; set; }
        
        public int? UpperLimitPrice { get; set; }
        
        public bool IsUponRequest { get; set; }
        
        public bool IsIndividualCalculation { get; set; }
        
        public ManufacturingOrService ManufacturingOrService { get; set; }

        public string GetFormattedPrice()
        {
            if (IsUponRequest)
            {
                return "По запросу";
            }

            if (IsIndividualCalculation)
            {
                return "Индивидуальный расчёт";
            }

            if(LowerLimitPrice != null && UpperLimitPrice != null)
            {
                return $"от {((int)LowerLimitPrice).ToString("C", CultureInfo.CurrentCulture)} до {((int)UpperLimitPrice).ToString("C", CultureInfo.CurrentCulture)}";
            }
            else
            {
                if(LowerLimitPrice != null)
                {
                    if(SecondPrice != null)
                    {
                        return $"от {((int)LowerLimitPrice).ToString("C", CultureInfo.CurrentCulture)}/{((int)SecondPrice).ToString("C", CultureInfo.CurrentCulture)}";
                    }

                    return $"от {((int)LowerLimitPrice).ToString("C", CultureInfo.CurrentCulture)}";
                }

                if (UpperLimitPrice != null)
                {
                    return $"до {((int)UpperLimitPrice).ToString("C", CultureInfo.CurrentCulture)}";
                }
            }

            return "Неизвестно";
        }
    }
}