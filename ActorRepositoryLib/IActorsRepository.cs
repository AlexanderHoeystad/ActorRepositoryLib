
namespace ActorRepositoryLib
{
    public interface IActorsRepository
    {
        Actor AddActor(Actor actor);
        Actor? DeleteActor(int id);
        //List<Actor> GetActor(string? nameStart = null, string? sortby = null);
        IEnumerable<Actor> GetActor(string? nameStart = null, string? sortby = null);
        Actor? GetActorById(int id);
        Actor? UpdateActor(int id, Actor data);

        
    }
}