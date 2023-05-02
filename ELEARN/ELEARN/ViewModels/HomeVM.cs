﻿using ELEARN.Models;

namespace ELEARN.ViewModels
{
    public class HomeVM
    {
       public IEnumerable<Slider> Sliders { get; set; }
       public IEnumerable<Course> Courses { get; set; }
       public IEnumerable<Author> Authors { get; set; }
       public IEnumerable<Event> Events { get; set; }
        public IEnumerable<News> News { get; set; } 

    }
}
