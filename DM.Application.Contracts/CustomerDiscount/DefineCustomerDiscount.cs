﻿using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Contracts.Product;
using System.ComponentModel.DataAnnotations;

namespace DM.Application.Contracts.CustomerDiscount
{
    public class DefineCustomerDiscount
    {
        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get;  set; }

        [Range(1, 99, ErrorMessage = ValidationMessages.IsRequired)]
        public int DiscountRate { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string StartDate { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string EndDate { get;  set; }
        public string Reason { get;  set; }
        //public SelectList Products { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
