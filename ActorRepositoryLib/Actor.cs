using System.Net.Http.Headers;

namespace ActorRepositoryLib
{
    public class Actor
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int BirthYear { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name} {BirthYear}";
        }

        public void ValidateName()
        {
            if (Name == null)
            {
                throw new ArgumentNullException("Name is null");
            }
            if (Name.Length < 4)
            {
                throw new ArgumentException("Name must be atleast 4 character" + Name);
            }
        }

        public void ValidateBirthYear()
        {
            if (BirthYear <= 1819)
            {
                throw new ArgumentException("Birth year must be greater than 1819");
            }
        }

        public void Validate()
        {
            ValidateName();
            ValidateBirthYear();
        }



    }
}
