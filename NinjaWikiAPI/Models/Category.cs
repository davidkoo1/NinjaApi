using System.ComponentModel.DataAnnotations;

namespace NinjaWikiAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public virtual IEnumerable<Skill> Skills { get; set; }
    }
}
//Автоматическая битва
//Берется 2 лучших ниндзя(силе/рейтингу/кол-во битв ранее) их имена+кланы=название битвы

//Битвы
//Битва
//СписокНиндзяПоБитве
//Список битв по ниндзя

//Ниндзя команды из н человек
//Команда против команды