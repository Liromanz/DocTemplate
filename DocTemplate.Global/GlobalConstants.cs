namespace DocTemplate.Global
{
    public static class GlobalConstants
    {
        //скрытый символ для работы с полями - \u2063, но в ртф он \u8291?

        //public const string UrlBase = "https://localhost:44337/api";
        public const string UrlBase = "https://doctemplateapi.azurewebsites.net/api";

        public const string SuccessMessage = "Выполнено";
        public const string NotFoundMessage = "Такого пользователя нет";
        public const string ConflictMessage = "Такой пользователь уже существует";


        public const string ErrorMessage = "Произошла ошибка. Сообщение: ";
        public const string UnsetErrorMessage = "Произошла внутренняя ошибка.";
    }
}
