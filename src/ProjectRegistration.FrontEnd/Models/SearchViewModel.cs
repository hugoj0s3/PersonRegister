using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonRegistration.Application.ViewModels;

namespace ProjectRegistration.FrontEnd.Models
{
    public class SearchPersonViewModel
    {
        public string Text { get; set; }

        public IList<PersonViewModel> Persons { get; set; }
    }
}
