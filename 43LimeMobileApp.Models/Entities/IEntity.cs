/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/

namespace _43LimeMobileApp.Models.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
    }

    public interface IEntity<T> : IEntity where T : class
    {
    }

    public abstract class Entity : IEntity<Entity>
    {
        public int Id { get; set; }

        public Entity()
        {

        }
    }
}
