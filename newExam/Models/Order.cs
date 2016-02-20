using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace newExam.Models
{
    public class Order
    {
        public Order()
        {
            NPersons = 1;
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public int? TourId { get; set; }
        public Tour Tour { get; set; }

        public string ApplicationUserId { get; set; }
        public string UserName { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }

        public DateTime Date { get; set; }
        public int NPersons { get; set; }
    }
}
