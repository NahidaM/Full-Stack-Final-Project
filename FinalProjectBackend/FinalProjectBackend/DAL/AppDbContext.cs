using FinalProjectBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;

namespace FinalProjectBackend.DAL
{
    public class AppDbContext :IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Slider> Sliders { get; set; } 
        public DbSet<AboutSlider> AboutSliders { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Workshop> Workshops { get; set; } 
        public DbSet<Event> Events { get; set; } 
        public DbSet<CategoryEvent> CategoryEvents { get; set; } 
        public DbSet<OurPurpose> OurPurposes { get; set; } 
        public DbSet<Services> Services { get; set; }
        public DbSet<Cover> Covers { get; set; } 
        public DbSet<HowWeAre> HowWeAres { get; set; }
        public DbSet<PurposeAssoc> PurposeAssocs { get; set; } 
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Bio> Bios { get; set; }
        public DbSet<SubscribedEmail> SubscribedEmails { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<WeAre> WeAres { get; set; } 
    }
}
