namespace CSE_website.Services;

public class BaseService
{
    protected readonly IWebHostEnvironment Environment;
    
    public BaseService(IWebHostEnvironment environment)
    {
        Environment = environment;
    }
}