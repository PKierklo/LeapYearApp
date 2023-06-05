using LeapYearApp.Forms;
using Microsoft.EntityFrameworkCore;
using System;

namespace LeapYearApp.Data
{
    public class helper : DbContext
    {
        public helper(DbContextOptions options) : base(options) { }

        public DbSet<LeapYear> LeapData { get; set; }

    }
}
