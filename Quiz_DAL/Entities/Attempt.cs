﻿using System;

namespace Quiz_DAL.Entities
{
    public class Attempt : BaseEntity
    {
        public int StudentId { get; set; }
        public int TopicId { get; set; }
        public int Score { get; set; }
        public DateTime DateTime { get; set; }
        public User Student { get; set; }
        public Topic Topic { get; set; }
    }
}