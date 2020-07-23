using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAppliancesStore.Models
{
    public class Order
    {
        [BindNever]
        public int id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Недопустимая длина имени")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Недопустимая длина имени")]
        public string LastName { get; set; }

        [Display(Name = "Номер телефона")]
        [Phone(ErrorMessage = "Не указан номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Город")]
        [Required(ErrorMessage = "Не указан город доставки")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Недопустимая длина названия города")]
        public string City { get; set; }

        [Display(Name = "Улица")]
        [Required(ErrorMessage = "Не указана улица доставки")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Недопустимая длина названия улицы")]
        public string Street { get; set; }

        [Display(Name = "Квартира")]
        [Required(ErrorMessage = "Недопустимое значение")]
        public int Flat { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public List<OrderDetail> Details { get; set; }
    }
}
