namespace UtgKata.Api.Models
{
    public class ErrorMessageViewModel
    {
        public string ErrorMessage { get; set; }

        public ErrorMessageViewModel(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }
    }
}
