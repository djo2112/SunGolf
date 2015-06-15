//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace SundayGolf.Models
{
    public partial class Weekly
    {
        public int ID { get; set; }
        public System.DateTime Date { get; set; }
        public int GolferID { get; set; }
        public int CourseID { get; set; }
        public int Score { get; set; }
        public decimal Handicap { get; set; }
        public Nullable<int> Net { get; set; }
        public Nullable<int> Skins { get; set; }
        public Nullable<int> Pins { get; set; }
        public Nullable<decimal> PinsAmt { get; set; }
        public Nullable<decimal> SkinsAmt { get; set; }
        public Nullable<decimal> NetAmt { get; set; }
        public Nullable<decimal> Total { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Golfer Golfer { get; set; }
    }
    
}