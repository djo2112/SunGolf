﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SundayGolf.Models
{
    public partial class SUNGOLFDB : DbContext
    {
        public SUNGOLFDB()
            : base("name=SUNGOLFDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Course> Courses { get; set; }
        public DbSet<Golfer> Golfers { get; set; }
        public DbSet<Weekly> Weeklies { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
    }
}
