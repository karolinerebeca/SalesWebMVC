using SalesWebMVC.Data;

internal class ServiceSeeding
{
    private SalesWebMVCContext context;

    public ServiceSeeding(SalesWebMVCContext context)
    {
        this.context = context;
    }
}