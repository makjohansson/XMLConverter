using XMLConverter.Models;

namespace XMLConverter.Helpers
{
	internal class PersonGenerator
	{
		private readonly string[] people;
		public PersonGenerator(string[] people) 
		{
			this.people = people;
		}

		public GeneratedResult Generate()
		{
			var index = 0;
			var personFound = false;
			var Person = new Person { Family = new List<Family>() };
			var skipLineUsedForFamily = 0;
			foreach (var line in people)
			{ 
				if (skipLineUsedForFamily > 0)
				{
					skipLineUsedForFamily--;
					index++;
					continue;
				}
				var splitLine = line.Split('|');
				if (index == 0 && splitLine[0] != "P")
					{
						throw new XMLConveterException("First line in file need to start with a \"P\"");
					}
				
				switch (splitLine.ElementAtOrDefault(0))
				{
					case "P":
						if (!personFound)
						{
							personFound = true;
							Person.Fistname = splitLine.ElementAtOrDefault(1);
							Person.Lastname = splitLine.ElementAtOrDefault(2);
						}
						else
						{
							return new GeneratedResult 
							{ 
								People = people[index..(people.Length - 1)], 
								Person = Person 
							};
						}
						break;
					case "T":
						if (!personFound)
						{
							throw new XMLConveterException($"\"{line}\" does not belong to a Person");
						}
						Person.Phone = new Phone { 
							Mobil = splitLine.ElementAtOrDefault(1), 
							Landline = splitLine.ElementAtOrDefault(2) };
						break;
					case "A":
						if (!personFound)
						{
							throw new XMLConveterException($"\"{line}\" does not belong to a Person");
						}
						Person.Address = new Address { 
							Street = splitLine.ElementAtOrDefault(1), 
							City = splitLine.ElementAtOrDefault(2), 
							PostalCode = splitLine.ElementAtOrDefault(3)
						};
						break;
					case "F":
						if (!personFound)
						{
							throw new XMLConveterException($"\"{line}\" does not belong to a Person");
						}
						var result = GenerateFamily(people[index..(people.Length - 1)]);
						Person.Family.Add(result.Family);
						skipLineUsedForFamily = result.Skip;
						break;
					default:
						throw new XMLConveterException($"Line must start with: \"P\", \"T\", \"A\" or \"F\" and \"{line}\" does not");
				}
				index++;
			}
			return new GeneratedResult
			{
				People = people[index..people.Length],
				Person = Person
			};	
		}

		private static GeneratedFamilyResult GenerateFamily(string[] people)
		{
			
			var family = new Family();
			var index = 0;
			var familyFound = false;
			foreach (var line in people)
			{
				var splitLine = line.Split("|");
				switch (splitLine.ElementAtOrDefault(0))
				{
					case "F":
						if (!familyFound)
						{
							family.Name = splitLine.ElementAtOrDefault(1);
							family.YearOfBirth = splitLine.ElementAtOrDefault(3) != null ? Int32.Parse(splitLine[3]) : null;
							familyFound = true;
						}
						else
						{
							return new GeneratedFamilyResult
							{
								Family = family,
								Skip = index
							};
						}
						break;
					case "A":
						if (!familyFound)
						{
							throw new XMLConveterException($"\"{line}\" does not belong too a family member");
						}
						family.Address = new Address { 
							Street = splitLine.ElementAtOrDefault(1), 
							City = splitLine.ElementAtOrDefault(2), 
							PostalCode = splitLine.ElementAtOrDefault(3)
						};
						index++;
						break;
					case "T":
						if (!familyFound)
						{
							throw new XMLConveterException($"\"{line}\" does not belong too a family member");
						}
						family.Phone = new Phone { 
							Mobil = splitLine.ElementAtOrDefault(1), 
							Landline = splitLine.ElementAtOrDefault(2)
						};
						index++;
						break;
					default:
						return new GeneratedFamilyResult
						{
							Family = family,
							Skip = index
						};
				}
			}
			return new GeneratedFamilyResult
			{
				Family = family,
				Skip = index
			};
		}
	}
}
