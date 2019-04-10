using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using project_vyb.Models;

namespace project_vyb.ViewModel
{
    public class HomeView
    {
        public IEnumerable<given_vaccine> given_Vaccines { get; set; }
        public IEnumerable<Upcomming> upcommings { get; set; }
        public IEnumerable<Upcomming> due { get; set; }
    }
}