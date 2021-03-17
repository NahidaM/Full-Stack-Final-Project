using FinalProjectBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.ViewModel
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public AboutSlider AboutSlider { get; set; }
        public IEnumerable<Volunteer> Volunteers { get; set; }
        public Workshop Workshop { get; set; }
        public List <Event> Events{ get; set; }
        public OurPurpose OurPurpose { get; set; }
        public List<Services> Services { get; set; }
    }
}
