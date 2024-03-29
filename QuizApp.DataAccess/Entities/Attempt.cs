﻿using System;
using System.Collections.Generic;

namespace QuizApp.DataAccess.Entities
{
    public class Attempt : BaseEntity
    {
        
        public string StudentId { get; set; }
        public string TopicId { get; set; }
        public int Score { get; set; }
        public DateTime DateTime { get; set; }
        
        public User Student { get; set; }
        public Topic Topic { get; set; }
    }
}