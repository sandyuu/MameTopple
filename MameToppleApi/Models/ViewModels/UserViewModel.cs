namespace MameToppleApi.Models.ViewModels
{
    public class UserViewModel
    {
        public string Account { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
        public int? Win { get; set; }
        public int? Lose { get; set; }
    }
}