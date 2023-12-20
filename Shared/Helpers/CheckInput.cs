namespace Shared.Helpers
{
    public static class CheckInput
    {
        public static string? ErrorMessage {set; get;}
        public static bool IsValid(string input)
        {
            IEnumerable<string> banneds = [";","-","/","|","or","*","%","+","=","'","and","#"];

            foreach (var banned in banneds)
            {
                if(input.Contains(banned))
                {
                    ErrorMessage = $"Kullanılamaz karakter = {banned}";
                    return false;
                }
            }
            return true;
        }
    }
}