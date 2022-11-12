using System.ComponentModel.DataAnnotations;

namespace Silky.Stock.Application.Contracts.Products.Dtos
{
    public abstract class ProductDtoBase
    {
        /// <summary>
        /// product name
        /// </summary>
        [Required(ErrorMessage = "product name不允许为空")]
        [MaxLength(100,ErrorMessage = "product name不允许超过100characters")]
        [MinLength(2, ErrorMessage = "product name不允许小于2characters")]
        public string Name { get; set; }

        /// <summary>
        /// unit price
        /// </summary>
        [Required(ErrorMessage = "unit price不允许为空")]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// in stock
        /// </summary>
        [Required(ErrorMessage = "in stock数量不允许为空")]
        public int Stock { get; set; } = 0;
    }
}