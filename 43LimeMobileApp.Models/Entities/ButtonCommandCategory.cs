/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/
namespace _43LimeMobileApp.Models.Entities
{

    public interface IButtonCommandCategory
    {
        string Category { get; set; }
        string Description { get; set; }
    }

    public class ButtonCommandCategory : IEntity<ButtonCommandCategory>, IButtonCommandCategory
    {
        public ButtonCommandCategory()
        {

        }

        public ButtonCommandCategory(int id, string category, string description)
        {
            Id = id;
            Category = category;
            Description = description;
        }

        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

    }
}
