﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Requests.ServiceRequestsValidation
{
    public class AddServiceDto
    {
        [Required(ErrorMessage = "Service Name is Required ")]
        public string ServiceName { get; set; }

        [Required(ErrorMessage = "Service Description is Required ")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Service Criteria is required")]
        public string CriteriaName { get; set; }

        [Required(ErrorMessage = "Service Price is Required ")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Service Availability status is Required ")]
        public string AvailabilityStatus { get; set; }
        

        public string? ParentServiceID { get; set; }


    }
}
