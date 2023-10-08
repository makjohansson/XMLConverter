using System.Collections.Generic;
namespace XMLConverter.Models
{
	internal class Person
	{
        public string  Fistname { get; set; }
        public string Lastname { get; set; }
        public Address Address { get; set; }
        public Phone Phone { get; set; }
        public List<Family> Family { get; set; }

    }
}
