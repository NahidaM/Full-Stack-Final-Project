using FinalProjectBackend.Migrations;
using FinalProjectBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cover = FinalProjectBackend.Models.Cover;
using HowWeAre = FinalProjectBackend.Models.HowWeAre;

namespace FinalProjectBackend.ViewModel
{
    public class HowWeAreVM
    {
       public List<Cover>  Cover { get; set; }
       public HowWeAre HowWeAre { get; set; }  
       public PurposeAssoc PurposeAssoc { get; set; }
      
    }
}
