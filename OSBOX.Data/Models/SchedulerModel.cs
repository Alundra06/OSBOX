using System;

namespace OSBOX.Data.Models
{
    public class CalendarEvent
    {
        //id, text, start_date and end_date properties are mandatory
        public int id { get; set; }
        public string text { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public CalendarEvent(int id, string text, DateTime start_date, DateTime end_date)
        {
            this.id = id;
            this.start_date = start_date;
            this.end_date = end_date;
            this.text = text;
        }
        public CalendarEvent() { }
    }
}