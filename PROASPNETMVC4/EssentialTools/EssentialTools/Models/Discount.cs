using System;

namespace EssentialTools.Models
{
    public interface IDiscountHelper
    {
        decimal ApplyDiscount(decimal totalParam);
    }

    //public class DefaultDiscountHelper : IDiscountHelper
    //{
    //    //sonradan eklendi
    //    public decimal DiscountSize { get; set; }

    //    public decimal ApplyDiscount(decimal totalParam)
    //    {
    //        return (totalParam - (10m / 100m * totalParam));
    //    }
    //}

    public class DefaultDiscountHelper : IDiscountHelper
    {
        public decimal discountSize;
        public DefaultDiscountHelper(decimal discountParam)
        {
            discountSize = discountParam;
        }
        public decimal ApplyDiscount(decimal totalParam)
        {
            return (totalParam - (discountSize / 100m * totalParam));
        }
    }




    public class FlexibleDiscountHelper : IDiscountHelper
    {
        public decimal ApplyDiscount(decimal totalParam)
        {
            decimal discount = totalParam > 100 ? 70 : 25;
            return (totalParam - (discount / 100m * totalParam));
        }
    }


    //To demonstrate the Visual Studio unit-test support
    public class MinimumDiscountHelper : IDiscountHelper
    {
        public decimal ApplyDiscount(decimal totalParam)
        {
            if (totalParam < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (totalParam > 100)
            {
                return totalParam * 0.9M;
            }
            else if (totalParam >= 10 && totalParam <= 100)
            {
                return totalParam - 5;
            }
            else
            {
                return totalParam;
            }
        }
    }


}