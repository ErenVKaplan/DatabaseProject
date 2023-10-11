namespace DatabaseProject.Services
{
    public class ServiceResponse<T>
    {
        public bool IsError { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }=new List<string>();
        public List<string> SuccessMessages { get; set; } = new List<string>();
        public T Data { get; set; }
        public void AddError(string errorMessage)
        {
            IsError= true; 
            Errors.Add(errorMessage);
        }
        public void AddSuccessMessage(string successMessage)
        {
            IsSuccess = true;
            SuccessMessages.Add(successMessage);
        }
    }
}
