using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace newExam.Models
{
    public class Tour
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Страна")]
        [Required(ErrorMessage = "Введите название страны")]
        public string Country { get; set; }
        [Display(Name = "Город")]
        [Required(ErrorMessage = "Введите название города")]
        public string City { get; set; }
        [Display(Name = "Вид Тура")]
        [Required(ErrorMessage = "Введите название вида тура")]
        public string Type { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Введите описание тура")]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Введите значение, большее нуля для цены")]
        [Display(Name = "Цена (руб)")]
        public decimal Price { get; set; }
    }
}
