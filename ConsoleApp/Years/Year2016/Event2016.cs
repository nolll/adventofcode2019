﻿using System.Collections.Generic;

namespace ConsoleApp.Years.Year2016
{
    public class Event2016 : Event
    {
        public Event2016() : base(2016)
        {
        }

        protected override IList<Day> Days => new List<Day>
        {
            new Day01(),
            new Day02(),
            new Day03(),
            new Day04(),
            new Day05(),
            new Day06()
        };
    }
}