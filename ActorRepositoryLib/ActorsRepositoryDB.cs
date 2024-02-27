using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ActorRepositoryLib
{
    public class ActorsRepositoryDB : IActorsRepository
    {
        private readonly ActorsDbContext _context;

        public ActorsRepositoryDB(ActorsDbContext context)
        {
            _context = context;
        }

        public Actor AddActor(Actor actor)
        {
            actor.Validate();
            actor.Id = 0;
            _context.Actors.Add(actor);
            _context.SaveChanges();
            return actor;
        }

        public Actor? DeleteActor(int id)
        {
            Actor? actor = GetActorById(id);
            if (actor is null)
            {
                return null;
            }
            _context.Actors.Remove(actor);
            _context.SaveChanges();
            return actor;
        }

        public IEnumerable<Actor> GetActor(string? nameStart = null, string? sortby = null)
        {
            IQueryable<Actor> query = _context.Actors.AsQueryable();
            if (nameStart != null)
            {
                query = query.Where(a => a.Name.Contains(nameStart));
            }
            if (sortby != null)
            {
                sortby = sortby.ToLower();
                switch (sortby)
                {
                    case "name":
                        query = query.OrderBy(a => a.Name);
                        break;
                    case "name_desc":
                        query = query.OrderByDescending(a => a.Name);
                        break;
                    case "birthyear":
                        query = query.OrderBy(a => a.BirthYear);
                        break;
                    case "birthyear_desc":
                        query = query.OrderByDescending(a => a.BirthYear);
                        break;
                    default:
                        break;
                        //throw new ArgumentException("Invalid sortby value");
                }
                }
                return query;

            

            }

            public Actor? GetActorById(int id)
            {
                return _context.Actors.FirstOrDefault(a => a.Id == id);
            }

            public Actor? UpdateActor(int id, Actor data)
            {
                data.Validate();
                Actor? actorToUpdate = GetActorById(id);
                if (actorToUpdate == null) return null;
                actorToUpdate.Name = data.Name;
                actorToUpdate.BirthYear = data.BirthYear;
                _context.SaveChanges();
                return actorToUpdate;
            }

        }
}
