﻿namespace AgendaWebApp.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public int ActiveTaskCount { get; set; }
        public int ActiveMinorTasks { get; set; }
        public int ActiveRelevantTasks { get; set; }
        public int ActiveImportantTasks { get; set; }
        public int FinishedTaskCount { get; set; }
    }
}
