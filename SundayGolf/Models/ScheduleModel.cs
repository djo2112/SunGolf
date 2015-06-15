using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SundayGolf.Models
{
    /// <summary>
    /// Help faciliate a partial view (the old .ascx)
    /// </summary>
    public partial class ScheduleModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int GolferID { get; set; }

        [Required]
        public int CourseID { get; set; }

        [Display(Name = "Golfer")]
        public string GolferName { get; set; }

        [Display(Name = "Golfing?")]
        public bool Golfing { get; set; }

        [Display(Name = "NotGolfing?")]
        public bool NotGolfing { get; set; }

        [Display(Name = "Maybe?")]
        public bool Maybe { get; set; }
    }

    public partial class ScheduleModel
    {
        public List<ScheduleModel> scheduleModel { get; set; }
    }
}