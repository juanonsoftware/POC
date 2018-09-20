using System;
using System.Globalization;

namespace Models
{
    public class Employee : DynamicPropertyBase
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime? Birthday
        {
            get
            {
                var str = Properties.GetOrDefault("Birthday");
                return string.IsNullOrEmpty(str) ? null : (DateTime?)DateTime.Parse(str);
            }
            set
            {
                if (value.HasValue)
                {
                    Properties.AddOrSet("Birthday", value.Value.ToString(CultureInfo.InvariantCulture));
                }
            }
        }

        public string Email2
        {
            get
            {
                return Properties.GetOrDefault("Email2");
            }
            set
            {
                Properties.AddOrSet("Email2", value);
            }
        }
    }
}