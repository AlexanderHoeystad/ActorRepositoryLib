using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorRepositoryLib
{
    public class ActorsRepositoryList : IActorsRepository
    {
        private int nextId = 1;
        private readonly List<Actor> _Actors = new();
        //{
        //   new Actor { Id = 1, Name = "Anthony Hopkins", BirthYear = 1937 },
        //   new Actor { Id = 2, Name = "Angelina Jolie", BirthYear = 1975 },
        //   new Actor { Id = 3, Name = "Matt Damon", BirthYear = 1970 },
        //   new Actor { Id = 4, Name = "Brad Pitt", BirthYear = 1963 },
        //   new Actor { Id = 5, Name = "Jhonny Depp", BirthYear = 1963 }
        //};

        public IEnumerable<Actor> GetActor(string? nameStart = null, string? sortby = null)
        {
            List<Actor> result = new List<Actor>(_Actors);
            if (nameStart != null)
            {
                result = result.FindAll(a => a.Name.Contains(nameStart));
            }

            if (sortby != null)

                switch (sortby)
                {
                    case "Name":
                        result.Sort((a1, a2) => a1.Name.CompareTo(a2.Name));
                        break;
                    case "Name_desc":
                        result.Sort((a1, a2) => a2.Name.CompareTo(a1.Name));
                        break;
                    case "BirthYear":
                        result.Sort((a1, a2) => a1.BirthYear.CompareTo(a2.BirthYear));
                        break;
                    case "BirthYear_desc":
                        result.Sort((a1, a2) => a1.BirthYear.CompareTo(a2.BirthYear));
                        break;

                }
            return result;
        }

        public Actor AddActor(Actor actor)
        {
            actor.Id = nextId++;
            //actor.Validate();
            _Actors.Add(actor);
            return actor;
        }

        public Actor? GetActorById(int id)
        {
            return _Actors.Find(a => a.Id == id);
        }

        public Actor? DeleteActor(int id)
        {
            Actor? actor = _Actors.Find(a => a.Id == id);
            if (actor != null)
            {
                _Actors.Remove(actor);
            }
            return actor;
        }

        public Actor? UpdateActor(int id, Actor data)
        {
            Actor? ActorToUpdate = _Actors.Find(a => a.Id == id);
            if (ActorToUpdate != null)
            {
                ActorToUpdate.Name = data.Name;
                ActorToUpdate.BirthYear = data.BirthYear;
            }
            return ActorToUpdate;
        }

    }
}
