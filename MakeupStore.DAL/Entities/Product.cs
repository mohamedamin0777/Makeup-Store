﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MakeupStore.DAL.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        public ProductCategory ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }

        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
    }
}
